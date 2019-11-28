using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [SerializeField] private float m_InitialJumpForce = 1200f;
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    public int HP;
    public float yKnockback;
    public float xKnockback;

    const float k_GroundedRadius = .2f;
    private bool m_Grounded;
    public float fuel;
    public SpriteRenderer weaponRenderer;
    public BoxCollider2D weaponCollider;

    public GameObject hand;
    public Light fuelLight;
    public float maxFuel;
    const float k_CeilingRadius = .2f;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;

    public ParticleSystem particles;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
        particles.Pause();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
        if (m_Grounded)
        {
            particles.Clear();
            particles.Pause();
        }
    }


    public void Move(float move, bool crouch, bool jump, bool shoot, bool grab)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight && !grab)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight && !grab)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (fuel > 0 && jump)
        {
            if (m_Grounded) m_Rigidbody2D.AddForce(new Vector2(0f, m_InitialJumpForce * Time.deltaTime));
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * Time.deltaTime));
            fuel -= 1f * Time.deltaTime;
            particles.Play();
        }

        if(shoot)
        {
            weaponCollider.enabled = true;
            Color color = weaponRenderer.color;
            color.a = 1;
            weaponRenderer.color = color;
        }
        else
        {
            weaponCollider.enabled = false;
            Color color = weaponRenderer.color;
            color.a = 0;
            weaponRenderer.color = color;
        }
        if (grab)
        {
            hand.SetActive(true);
        }
        else
        {
            hand.GetComponent<FixedJoint2D>().connectedBody = null;
            hand.SetActive(false);
        }
        if(m_Grounded && fuel < maxFuel)
        {
            fuel += 2f * Time.deltaTime;
        }
    }

    public void TakeDamage(int damage, float enemyX)
    {
        HP -= damage;
        Color color = fuelLight.color;
        color.g -= damage * 0.25f;
        fuelLight.color = color;
        if (HP <= 0) GameOver();
        m_Rigidbody2D.velocity = new Vector2(0f, 0f);
        if (enemyX > transform.position.x) m_Rigidbody2D.AddForce(new Vector2(xKnockback, yKnockback));
        else m_Rigidbody2D.AddForce(new Vector2(-xKnockback, yKnockback));
    }

    void GameOver()
    {

    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

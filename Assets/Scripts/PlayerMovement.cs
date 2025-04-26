using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Fall Damage Settings")]
    [SerializeField] private float fallDamageThreshold = -10f;  // Yere bu hızın altında düşerse hasar alacak
    [SerializeField] private int fallDamageAmount = -10;         // Düşünce kaç can azalacak
    private float lastYVelocity;                                // Düşmeden önceki düşüş hızını tutar

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;               // Başlangıç canı
    private int currentHealth;

    [Header("Ladder Settings")]
    private bool isOnLadder = false;
    [SerializeField] private float climbSpeed = 5f; // Merdivende çıkış hızı

    [SerializeField] private float speed;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float crouchSpeedMultiplier = 0.5f;
    [SerializeField] private float sprintDuration = 2f;    // Sprint süresi (saniye)
    [SerializeField] private float sprintCooldown = 3f;    // Sprint bekleme süresi (saniye)

    private float defaultGravity;

    private Rigidbody2D body;
    private bool grounded;
    private bool crouching = false;

    private Vector3 originalSize;
    private Vector3 crouchSize;

    private bool isSprinting = false;
    private float sprintTimer = 0f;
    private float cooldownTimer = 0f;


    private void Awake()
    {
        //get references
        body = GetComponent<Rigidbody2D>();
        //beginning size
        originalSize = transform.localScale;
        //size down to half
        crouchSize = new Vector3(originalSize.x, originalSize.y * 0.5f, originalSize.z);
        currentHealth = maxHealth;
        defaultGravity = body.gravityScale;
    }

    private void Update()
    {
        //get current position
        float moveX = Input.GetAxisRaw("Horizontal");

        //sprint
        Sprint(moveX);

        //flip player when moving left
        if (moveX > 0.01f)
            transform.localScale = crouching ? new Vector3(crouchSize.x, crouchSize.y, crouchSize.z) : originalSize;
        else if (moveX < -0.01f)
            transform.localScale = crouching ? new Vector3(-crouchSize.x, crouchSize.y, crouchSize.z) : new Vector3(-originalSize.x, originalSize.y, originalSize.z);


        if (Input.GetKey(KeyCode.Space) && grounded && !crouching)
        {
            Jump();
        }

        //crounch
        if (Input.GetKeyDown(KeyCode.LeftControl) && grounded && !crouching)
        {
            Crouch();
        }
        //stand up
        else if (Input.GetKeyUp(KeyCode.LeftControl) && crouching)
        {
            StandUp();
        }

        if (isOnLadder)
        {
            float moveY = Input.GetAxisRaw("Vertical");
            body.linearVelocity = new Vector2(body.linearVelocity.x, moveY * climbSpeed);
            body.gravityScale = 0f; // Merdivendeyken yerçekimi kapalı
        }
        else if (body.gravityScale == 0f) // sadece yerçekimi kapalıysa tekrar aç
        {
            body.gravityScale = defaultGravity;
        }


    }

    void Sprint(float moveX)
    {
        float currentSpeed;

        // Shift basılırsa ve sprint hazırsa sprint başlat
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSprinting && cooldownTimer <= 0f)
        {
            isSprinting = true;
            sprintTimer = sprintDuration;
        }
        // Eğer sprint aktifse süreyi say ve biterse kapat
        if (isSprinting)
        {
            sprintTimer -= Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.LeftShift) || sprintTimer <= 0f)
            {
                isSprinting = false;
                cooldownTimer = sprintCooldown;
            }
        }
        else
        {
            // Sprint aktif değilken cooldown say
            if (cooldownTimer > 0f)
            {
                cooldownTimer -= Time.deltaTime;
            }
        }
        // Hız belirle (crouch kontrolü dahil)
        if (isSprinting)
        {
            currentSpeed = speed * sprintMultiplier;
        }
        else
        {
            currentSpeed = crouching ? speed * crouchSpeedMultiplier : speed;
        }

        body.linearVelocity = new Vector2(moveX * currentSpeed, body.linearVelocity.y);

        if (!isOnLadder)
        {
            body.linearVelocity = new Vector2(moveX * currentSpeed, body.linearVelocity.y);
        }

    }


    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        grounded = false;
    }

    private void Crouch()
    {
        transform.localScale = new Vector3(transform.localScale.x, crouchSize.y, transform.localScale.z);
        crouching = true;
    }

    private void StandUp()
    {
        transform.localScale = originalSize;
        crouching = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            grounded = true;

        if (lastYVelocity < fallDamageThreshold)
        {
            // Hız ne kadar düşükse (yani yere ne kadar hızlı çakılmışsa) o kadar fazla hasar
            float fallSpeed = Mathf.Abs(lastYVelocity); // pozitif değer
            int damage = Mathf.RoundToInt((fallSpeed - Mathf.Abs(fallDamageThreshold)) * 2f); // çarpanı artır/azalt
            TakeDamage(damage);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            grounded = false;
    }

    private void FixedUpdate()
    {
        lastYVelocity = body.linearVelocity.y;
    }

    private void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // Ölünce yapılacak şeyler (örneğin sahneyi resetlemek)
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = false;
            // Hızı koru, yerçekimi zaten Update içinde normale dönecek
        }
    }

}

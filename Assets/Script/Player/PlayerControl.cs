using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Vector2 velocity;
    //public float acceleration = 10f;
    //public float deceleration = 8f;
    //public float maxSpeed = 5f;

    //void Update()
    //{
    //    float h = Input.GetAxisRaw("Horizontal");
    //    float v = Input.GetAxisRaw("Vertical");
    //    Vector2 input = new Vector2(h, v).normalized;

    //    if (input.magnitude > 0)
    //    {
    //        velocity = Vector2.MoveTowards(velocity, input * maxSpeed, acceleration * Time.deltaTime);
    //    }
    //    else
    //    {
    //        velocity = Vector2.MoveTowards(velocity, Vector2.zero, deceleration * Time.deltaTime);
    //    }

    //    transform.Translate(velocity * Time.deltaTime);


    //}
    public float acceleration = 10f;
    public float deceleration = 8f;
    public float maxSpeed = 6f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Vector2 velocity;
    private Vector2 input;
    private bool isDashing = false;
    private float dashTime = 0f;
    private float dashCooldownTimer = 0f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer ��������
    }

    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // ��� �Է� ó��
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0f && input != Vector2.zero)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashCooldownTimer = dashCooldown;
        }
        if (input.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (input.x < -0.01f)
            spriteRenderer.flipX = true;

        // �ִϸ��̼� �Ķ���� ����
        animator.SetFloat("Speed", velocity.magnitude);
    }

    void FixedUpdate()
    {
        // ��� ���̸� ���� �ð� ���� ���� �ӵ� �̵�
        if (isDashing)
        {
            velocity = input * dashSpeed;
            dashTime -= Time.fixedDeltaTime;
            if (dashTime <= 0f)
            {
                isDashing = false;
            }
        }
        else
        {
   
            if (input.magnitude > 0)
            {
                velocity = Vector2.MoveTowards(velocity, input * maxSpeed, acceleration * Time.fixedDeltaTime);
            }
            else
            {
                velocity = Vector2.MoveTowards(velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            }

            transform.Translate(velocity * Time.deltaTime);

            // ��� ��Ÿ�� ����
            if (dashCooldownTimer > 0f)
                dashCooldownTimer -= Time.fixedDeltaTime;
        }

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}

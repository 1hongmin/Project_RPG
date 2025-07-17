using UnityEngine;
using UnityEngine.Windows;

public class MovementController : MonoBehaviour
{

    [Header("이동")]
    public float moveSpeed = 10f;
    public float acceleration = 40f;
    public float deceleration = 40f;
    public float velocityPower = 0.9f;

    [Header("점프")]
    public float jumpForce = 15f;
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    [Header("상태")]
    public bool isFacingRight = true;
    private bool isGrounded;
    private float lastGroundedTime;
    private float lastJumpPressedTime;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float moveInput = PlayerInputHandler.instance.MoveInput.x;

        // 좌우 이동 처리
        HandleMovement(moveInput);

        // 방향 전환
        Flip();

        // 점프 입력 타이밍 저장
        if (PlayerInputHandler.instance.JumpPressed)
            lastJumpPressedTime = Time.time;

        // 점프 조건 확인
        if (Time.time - lastJumpPressedTime <= jumpBufferTime &&
            Time.time - lastGroundedTime <= coyoteTime)
        {
            Jump();
            lastJumpPressedTime = -999;
        }

        // 애니메이션 연동
    }

    private void FixedUpdate()
    {
        // Ground Check
        isGrounded = Physics2D.Raycast(transform.position + Vector3.down * 0.5f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        if (isGrounded)
            lastGroundedTime = Time.time;
    }

    private void HandleMovement(float input)
    {
        float targetSpeed = input * moveSpeed;
        float speedDiff = targetSpeed - rb.linearVelocity.x;
        float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velocityPower) * Mathf.Sign(speedDiff);
        rb.AddForce(Vector2.right * movement);
    }

    private void Jump()
    {
        rb.linearVelocity= new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void Flip()
    {
        if (PlayerInputHandler.instance.MoveInput.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (PlayerInputHandler.instance.MoveInput.x < -0.01f)
            spriteRenderer.flipX = true;
    }
}

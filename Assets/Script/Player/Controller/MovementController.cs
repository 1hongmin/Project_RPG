using UnityEngine;
using UnityEngine.Windows;

public class MovementController : MonoBehaviour
{
    float NomalSpeed = 2f;
    float maxSpeed = 6f;//캐릭터가 일반 이동 중에 낼 수 있는 최대 속도입니다.

    float jumpForce = 7f;
    float acceleration = 10f;//  가속도입니다.
    float deceleration = 8f;//감속도입니다. 

    float dashSpeed = 10f; //대시할 때 이동 속도입니다. 일반 속도보다 빠르게 설정합니다.
    float dashDuration = 0.2f; //대시가 유지되는 시간입니다. 초 단위로, 이 시간 동안 dashSpeed로 움직입니다
    float dashCooldown = 1f; //대시를 한 번 사용한 후 다시 사용할 수 있을 때까지 기다려야 하는 쿨다운 시간입니다

    private Vector2 velocity; //현재 캐릭터의 속도 벡터입니다. 가속도와 감속도를 적용하여 계산된 실제 이동값을 담습니다.
    private bool isDashing = false;
    private float dashTime = 0f; //현재 대시가 진행된 시간입니다. d
    private float dashCooldownTimer = 0f; //대시 쿨다운이 얼마나 지났는지를 추적하는 변수입니다. 이 값이 dashCooldown보다 작으면 아직 대시를 다시 할 수 없습니다.

    private Rigidbody2D rb;

    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInputHandler.instance.MoveInput.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (PlayerInputHandler.instance.MoveInput.x < -0.01f)
            spriteRenderer.flipX = true;


        speed = PlayerInputHandler.instance.ShiftPressed ? maxSpeed : NomalSpeed;

        // 애니메이션 파라미터 갱신
        PlayerAnimatorController.Instance.UpdateMove(speed);
        Debug.Log(speed);
    }
    float speed;
    private void FixedUpdate()
    {

        if (PlayerInputHandler.instance.MoveInput.magnitude > 0)
        {
            velocity = Vector2.MoveTowards(velocity, PlayerInputHandler.instance.MoveInput, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            speed = 0;
        }
        transform.Translate(velocity * Time.fixedDeltaTime * speed);


    }
}

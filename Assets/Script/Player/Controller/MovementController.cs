using UnityEngine;
using UnityEngine.Windows;

public class MovementController : MonoBehaviour
{
    float NomalSpeed = 2f;
    float maxSpeed = 6f;//ĳ���Ͱ� �Ϲ� �̵� �߿� �� �� �ִ� �ִ� �ӵ��Դϴ�.

    float jumpForce = 7f;
    float acceleration = 10f;//  ���ӵ��Դϴ�.
    float deceleration = 8f;//���ӵ��Դϴ�. 

    float dashSpeed = 10f; //����� �� �̵� �ӵ��Դϴ�. �Ϲ� �ӵ����� ������ �����մϴ�.
    float dashDuration = 0.2f; //��ð� �����Ǵ� �ð��Դϴ�. �� ������, �� �ð� ���� dashSpeed�� �����Դϴ�
    float dashCooldown = 1f; //��ø� �� �� ����� �� �ٽ� ����� �� ���� ������ ��ٷ��� �ϴ� ��ٿ� �ð��Դϴ�

    private Vector2 velocity; //���� ĳ������ �ӵ� �����Դϴ�. ���ӵ��� ���ӵ��� �����Ͽ� ���� ���� �̵����� ����ϴ�.
    private bool isDashing = false;
    private float dashTime = 0f; //���� ��ð� ����� �ð��Դϴ�. d
    private float dashCooldownTimer = 0f; //��� ��ٿ��� �󸶳� ���������� �����ϴ� �����Դϴ�. �� ���� dashCooldown���� ������ ���� ��ø� �ٽ� �� �� �����ϴ�.

    private Rigidbody2D rb;

    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer ��������
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInputHandler.instance.MoveInput.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (PlayerInputHandler.instance.MoveInput.x < -0.01f)
            spriteRenderer.flipX = true;


        speed = PlayerInputHandler.instance.ShiftPressed ? maxSpeed : NomalSpeed;

        // �ִϸ��̼� �Ķ���� ����
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

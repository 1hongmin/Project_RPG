using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public enum PlayerStateType
{
    None,Movement,Attack,Hurt,Death

}

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState currentState;


    public PlayerAnimatorController playerAnim;
    public PlayerInputHandler playerInput;


    private void Awake()
    {
        if (playerAnim == null)
            Debug.LogError("PlayerAnimatorController�� �Ҵ� ���ּ���");
        if (playerInput == null)
            Debug.LogError("PlayerInputHandler�� �Ҵ� ���ּ���");

    }
    void Start()
    {
       // ChangeState(new IdleState(this));
    }

    void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(IPlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}

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
            Debug.LogError("PlayerAnimatorController을 할당 해주세요");
        if (playerInput == null)
            Debug.LogError("PlayerInputHandler을 할당 해주세요");

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

using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState currentState;

    [HideInInspector]
    public PlayerAnimatorController playerAnim;

    [HideInInspector]
    public PlayerInputHandler playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInputHandler>();
        playerAnim = GetComponent<PlayerAnimatorController>();
    }

    void Start()
    {
        ChangeState(new IdleState(this));
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

using UnityEngine;

public class IdleState : IPlayerState
{
    private PlayerStateMachine machine;

    public IdleState(PlayerStateMachine machine)
    {
        this.machine = machine;
    }

    public void Enter()
    {
        machine.playerAnim.Idle();
    }

    public void Update()
    {
        //if (machine.InputReader.MoveInput != Vector2.zero)
        //{
        //    machine.ChangeState(new MoveState(machine));
        //}
    }

    public void Exit() { }
}

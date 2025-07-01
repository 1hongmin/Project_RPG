using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public Vector2 MoveInput { get; private set; }

    public bool ShiftPressed { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool shieldPressed { get; private set; }


    void Update()
    {
       
        MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"),0).normalized;

        JumpPressed = Input.GetKeyDown(KeyCode.Space);
        ShiftPressed = Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift);
        AttackPressed = Input.GetMouseButtonDown(0);
        shieldPressed = Input.GetMouseButton(1);
    }
}

using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public static PlayerAnimatorController Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    Animator animator;

    int M_Speed = Animator.StringToHash("Speed");

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void UpdateMove(float Speed)
    {
        animator.SetFloat(M_Speed, Speed);
    }

}

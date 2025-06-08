using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void Idle()
    {

    }

}

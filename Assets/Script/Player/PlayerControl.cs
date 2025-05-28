using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Vector2 velocity;
    public float acceleration = 10f;
    public float deceleration = 8f;
    public float maxSpeed = 5f;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(h, v).normalized;

        if (input.magnitude > 0)
        {
            velocity = Vector2.MoveTowards(velocity, input * maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, deceleration * Time.deltaTime);
        }

        transform.Translate(velocity * Time.deltaTime);
    }
}

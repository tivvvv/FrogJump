using UnityEngine;

public class MoveForwardController : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

}

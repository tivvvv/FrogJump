using UnityEngine;

public class MoveForwardController : MonoBehaviour
{
    public float speed;

    public float maxLength = 25;

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        Move();
        if (Mathf.Abs(transform.position.x - startPos.x) > maxLength)
        {
            Destroy(this.gameObject);
        }
    }

    private void Move()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

}

using UnityEngine;

public class MoveForwardController : MonoBehaviour
{
    // 汽车行驶方向
    public int dir = 1;

    public float speed;

    public float maxLength = 25;

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
        transform.localScale = new Vector3(dir, 1, 1);
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
        transform.position += transform.right * speed * Time.deltaTime * dir;
    }

}

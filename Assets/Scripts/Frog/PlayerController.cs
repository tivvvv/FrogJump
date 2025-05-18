using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 预设跳跃距离
    public float jumpDistance;

    // 实际移动距离
    private float moveDistance;

    // 是否长按
    private bool buttonHeld;

    // 跳跃目标点
    private Vector2 destination;

    // 是否在跳跃中
    private bool isJump;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (destination.y - transform.position.y < 0.1f)
        {
            isJump = false;
        }
    }

    private void FixedUpdate()
    {
        if (isJump)
        {
            rb.position = Vector2.Lerp(transform.position, destination, 0.134f);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJump)
        {
            moveDistance = jumpDistance;
            destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
            isJump = true;
            Debug.Log("jump");
        }

    }

    public void LongJump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJump)
        {
            moveDistance = 2 * jumpDistance;
            buttonHeld = true;
            destination = new Vector2(transform.position.x, transform.position.y + moveDistance);

        }

        if (context.canceled && buttonHeld && !isJump)
        {
            Debug.Log("LongJump");
            buttonHeld = false;
            isJump = true;
        }

    }

    public void GetTouchPosition(InputAction.CallbackContext context)
    {
        Debug.Log("GetTouchPosition");
    }

}

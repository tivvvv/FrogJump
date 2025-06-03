using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 方向枚举
    private enum Direction
    {
        Up, Left, Right
    }

    // 点击位置
    private Vector2 touchPosition;

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

    // 跳跃方向
    private Direction dir;

    // 是否可以跳跃
    private bool canJump;

    private Rigidbody2D rb;

    private Animator anim;

    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canJump)
        {
            TriggerJump();
        }
    }

    private void FixedUpdate()
    {
        if (isJump)
        {
            rb.position = Vector2.Lerp(transform.position, destination, 0.134f);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Border") || other.CompareTag("Car"))
        {
            Debug.Log("gg");
        }

        if (!isJump && other.CompareTag("Obstacle"))
        {
            Debug.Log("gg");
        }
    }

    #region INPUT 输入回调函数
    /// <summary>
    /// 小跳
    /// </summary>
    /// <param name="context"></param>
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJump)
        {
            Debug.Log("jump");
            moveDistance = jumpDistance;
            canJump = true;
        }
    }

    /// <summary>
    /// 大跳
    /// </summary>
    /// <param name="context"></param>
    public void LongJump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJump)
        {
            moveDistance = 2 * jumpDistance;
            buttonHeld = true;
        }

        if (context.canceled && buttonHeld && !isJump)
        {
            Debug.Log("LongJump");
            canJump = true;
            buttonHeld = false;
        }
    }

    public void GetTouchPosition(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
            Debug.Log("GetTouchPosition: " + touchPosition);
            var offset = ((Vector3)touchPosition - transform.position).normalized;
            if (Mathf.Abs(offset.x) <= 0.7f)
            {
                dir = Direction.Up;
            }
            else if (offset.x < 0)
            {
                dir = Direction.Left;
            }
            else
            {
                dir = Direction.Right;
            }
        }
    }
    #endregion

    /// <summary>
    /// 触发执行跳跃动画
    /// </summary>
    private void TriggerJump()
    {
        anim.SetTrigger("Jump");
        canJump = false;
        switch (dir)
        {
            case Direction.Up:
                anim.SetBool("isSide", false);
                destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
                transform.localScale = Vector3.one;
                break;
            case Direction.Left:
                anim.SetBool("isSide", true);
                destination = new Vector2(transform.position.x - moveDistance, transform.position.y);
                transform.localScale = Vector3.one;
                break;
            case Direction.Right:
                anim.SetBool("isSide", true);
                destination = new Vector2(transform.position.x + moveDistance, transform.position.y);
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
    }

    #region Animation Event 动画事件
    /// <summary>
    /// 开始播放跳跃动画
    /// </summary>
    public void StartJumpAnimationEvent()
    {
        isJump = true;
        // 跳跃过程中修改排序图层
        sr.sortingLayerName = "Front";
    }

    /// <summary>
    /// 完成播放跳跃动画
    /// </summary>
    public void FinishJumpAnimationEvent()
    {
        isJump = false;
        sr.sortingLayerName = "Mid";
    }
    #endregion

}

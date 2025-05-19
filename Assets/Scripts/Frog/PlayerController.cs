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

    // 是否可以跳跃
    private bool canJump;

    private Rigidbody2D rb;

    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
            destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
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
            destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
            canJump = true;
            buttonHeld = false;
        }
    }

    public void GetTouchPosition(InputAction.CallbackContext context)
    {
        Debug.Log("GetTouchPosition");
    }
    #endregion

    /// <summary>
    /// 触发执行跳跃动画
    /// </summary>
    private void TriggerJump()
    {
        anim.SetTrigger("Jump");
        canJump = false;
    }

    #region Animation Event 动画事件
    /// <summary>
    /// 开始播放跳跃动画
    /// </summary>
    public void StartJumpAnimationEvent()
    {
        isJump = true;
    }

    /// <summary>
    /// 完成播放跳跃动画
    /// </summary>
    public void FinishJumpAnimationEvent()
    {
        isJump = false;
    }
    #endregion

}

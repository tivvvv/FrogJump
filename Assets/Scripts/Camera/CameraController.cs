using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform frog;

    public float offsetY;

    // 屏幕比例
    private float ratio;

    // 缩放系数
    public float zoomBase;

    private void Start()
    {
        ratio = (float)Screen.height / (float)Screen.width;
        Camera.main.orthographicSize = zoomBase * ratio * 0.462f;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, frog.transform.position.y + offsetY, transform.position.z);
    }


}

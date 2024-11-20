using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // プレイヤーのTransform
    public Vector3 offset = new Vector3(0, 0, -10);  // カメラのオフセット（Z軸を後ろに下げる）
    public float fixedY = 4;  // カメラの固定するY座標

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x + offset.x, fixedY + offset.y, offset.z);
        }
    }
}

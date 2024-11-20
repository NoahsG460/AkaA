using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // �v���C���[��Transform
    public Vector3 offset = new Vector3(0, 0, -10);  // �J�����̃I�t�Z�b�g�iZ�������ɉ�����j
    public float fixedY = 4;  // �J�����̌Œ肷��Y���W

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x + offset.x, fixedY + offset.y, offset.z);
        }
    }
}

using UnityEngine;

public class LoopingMovement : MonoBehaviour
{
    // ���[�v����͈�
    public float minX = -10f; // ���[��x���W
    public float maxX = 10f;  // �E�[��x���W

    void Update()
    {
        // ���݂̈ʒu���擾
        Vector3 position = transform.position;

        // �E�[�𒴂����ꍇ�A���[�Ɉړ�
        if (position.x > maxX)
        {
            position.x = minX;
        }
        // ���[�𒴂����ꍇ�A�E�[�Ɉړ�
        else if (position.x < minX)
        {
            position.x = maxX;
        }

        // �V�����ʒu��ݒ�
        transform.position = position;
    }
}

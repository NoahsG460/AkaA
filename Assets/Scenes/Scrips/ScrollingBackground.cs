using UnityEngine;

public class PlayerLoop : MonoBehaviour
{
    public float leftBoundary = -10f;  // ���[�̈ʒu
    public float rightBoundary = 10f; // �E�[�̈ʒu

    void Update()
    {
        // �L�����N�^�[�̌��݂̈ʒu
        Vector3 position = transform.position;

        // �E�[�𒴂����ꍇ�A���[�Ƀ��[�v
        if (position.x > rightBoundary)
        {
            position.x = leftBoundary;
        }
        // ���[�𒴂����ꍇ�A�E�[�Ƀ��[�v
        else if (position.x < leftBoundary)
        {
            position.x = rightBoundary;
        }

        // ���[�v�����ʒu��K�p
        transform.position = position;
    }
}

using UnityEngine;

public class SpriteScaler : MonoBehaviour
{
    [SerializeField] private Vector3 fixedScale = new Vector3(0.1f, 0.1f, 1f); // �Œ肷��X�P�[��

    void LateUpdate()
    {
        // �X�P�[������ɌŒ�i�A�j���[�V�����̉e�����󂯂���ɒ����j
        if (transform.localScale != fixedScale)
        {
            transform.localScale = fixedScale;
        }
    }
}


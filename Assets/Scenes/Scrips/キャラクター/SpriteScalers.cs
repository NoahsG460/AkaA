using UnityEngine;

public class SpriteScaler : MonoBehaviour
{
    [SerializeField] private Vector3 fixedScale = new Vector3(0.1f, 0.1f, 1f); // �Œ肷��X�P�[��

    void Start()
    {
        // �����X�P�[����ݒ�
        transform.localScale = fixedScale;
    }

    void Update()
    {
        // �X�P�[������ɌŒ�
        if (transform.localScale != fixedScale)
        {
            transform.localScale = fixedScale;
        }
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;         // ��ѓ���̃_���[�W
    public float lifetime = 100f;   // ��ѓ�������I�ɏ�����܂ł̎���

    void Start()
    {
        // ��莞�Ԍ�ɍ폜
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �G�ɓ���������_���[�W��^����
        EnemyManager enemy = collision.gameObject.GetComponent<EnemyManager>();
        if (enemy != null)
        {
            enemy.OnDamage(damage); // �G�Ƀ_���[�W������K�p
        }

        // ���g���폜
        Destroy(gameObject);
    }
}


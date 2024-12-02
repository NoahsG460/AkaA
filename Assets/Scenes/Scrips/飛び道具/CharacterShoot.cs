using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // ��ѓ����Prefab
    public Transform shootPoint;       // ��ѓ���𔭎˂���ʒu
    public float projectileSpeed = 10f; // ��ѓ���̃X�s�[�h
    private bool facingRight = true;   // �L�����N�^�[���E�������ǂ���

    void Update()
    {
        // A, D�L�[�ňړ����͂��m�F���Č������X�V
        if (Input.GetKey(KeyCode.A))
        {
            if (facingRight) Flip();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!facingRight) Flip();
        }

        // ���ˏ���
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // ��ѓ���𐶐�
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Rigidbody2D�Ŕ�΂�
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // �L�����N�^�[�̌����ɉ��������x��ݒ�
            Vector2 shootDirection = facingRight ? Vector2.right : Vector2.left;
            rb.velocity = shootDirection * projectileSpeed;
        }

        // �����Ƃ̏Փ˂𖳎�
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D projectileCollider = projectile.GetComponent<Collider2D>();
        if (playerCollider != null && projectileCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, projectileCollider);
        }
    }

    void Flip()
    {
        // �L�����N�^�[�̌����𔽓]
        facingRight = !facingRight;

        // �L�����N�^�[�̃X�v���C�g�̌����𔽓]������
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // X���𔽓]
        transform.localScale = localScale;
    }
}

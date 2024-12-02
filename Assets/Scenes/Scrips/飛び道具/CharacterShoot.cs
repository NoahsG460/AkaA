using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // ��ѓ����Prefab
    public Transform shootPoint;       // ���˂���ʒu
    public float projectileSpeed = 10f; // ��ѓ���̃X�s�[�h
    private bool facingRight = true;   // �L�����N�^�[�̌��� (�E�������f�t�H���g)

    void Update()
    {
        // ���ˏ���
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }

        // �L�����N�^�[�̌�����؂�ւ���i��: ���E�ړ��ɍ��킹�āj
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            facingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            facingRight = true;
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
    }
}

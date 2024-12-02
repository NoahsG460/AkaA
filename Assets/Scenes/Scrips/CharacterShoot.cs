using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // ��ѓ����Prefab
    public Transform shootPoint;       // ��ѓ���𔭎˂���ʒu
    public float projectileSpeed = 10f; // ��ѓ���̃X�s�[�h

    void Update()
    {
        // K�L�[�Ŕ���
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
            rb.velocity = transform.right * projectileSpeed; // �L�����N�^�[�̌����ɉ����Ĕ�΂�
        }
    }
}

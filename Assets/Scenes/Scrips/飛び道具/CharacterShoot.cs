using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // ��ѓ����Prefab
    public Transform shootPoint;       // ���˃|�C���g��Transform
    public float projectileSpeed = 10f; // ��ѓ���̃X�s�[�h

    private float lastDirection = 1f; // �Ō�ɉ����ꂽ�L�[�̕����i1:�E, -1:���j

    void Update()
    {
        // A�L�[�ō��AD�L�[�ŉE�̕������L�^
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastDirection = -1f;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastDirection = 1f;
        }

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
            rb.velocity = new Vector2(lastDirection * projectileSpeed, 0f); // �Ō�̕����ɔ�΂�
        }

        // �f�o�b�O���O�Ŋm�F
        Debug.Log("Projectile Direction: " + (lastDirection > 0 ? "Right" : "Left"));
    }
}

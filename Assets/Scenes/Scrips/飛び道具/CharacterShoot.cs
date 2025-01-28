using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // ��ѓ����Prefab
    public Transform shootPoint;       // ���˃|�C���g��Transform
    public float projectileSpeed = 10f; // ��ѓ���̃X�s�[�h
    public SpriteRenderer characterSprite; // �L�����N�^�[�̃X�v���C�g

    private float lastDirection = 1f; // �Ō�ɉ����ꂽ�L�[�̕����i1:�E, -1:���j

    void Update()
    {
        // A�L�[�ō��AD�L�[�ŉE�̕������L�^
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastDirection = -1f;
            UpdateCharacterDirection(); // �L�����N�^�[�̌������X�V
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastDirection = 1f;
            UpdateCharacterDirection(); // �L�����N�^�[�̌������X�V
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

    void UpdateCharacterDirection()
    {
        // �L�����N�^�[�̃X�v���C�g�𔽓]������
        if (characterSprite != null)
        {
            characterSprite.flipX = lastDirection < 0; // ���������Ƃ��X�v���C�g�𔽓]
        }

        // ���˃|�C���g�̈ʒu���X�V�i���E���]�j
        if (shootPoint != null)
        {
            Vector3 localPosition = shootPoint.localPosition;
            localPosition.x = Mathf.Abs(localPosition.x) * lastDirection; // x���𔽓]
            shootPoint.localPosition = localPosition;
        }
    }
}

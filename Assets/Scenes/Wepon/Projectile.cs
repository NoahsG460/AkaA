using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float maxDistance = 20f; // �e����ԍő勗��
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position; // ���ˈʒu���L�^
    }

    void Update()
    {
        // ��苗���𒴂�����e��j��
        if (Vector2.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // �G�ɓ��������ꍇ�ɒe��j��
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}

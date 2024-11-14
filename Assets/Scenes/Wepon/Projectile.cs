using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float maxDistance = 20f; // ’e‚ª”ò‚ÔÅ‘å‹——£
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position; // ”­ËˆÊ’u‚ğ‹L˜^
    }

    void Update()
    {
        // ˆê’è‹——£‚ğ’´‚¦‚½‚ç’e‚ğ”j‰ó
        if (Vector2.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // “G‚É“–‚½‚Á‚½ê‡‚É’e‚ğ”j‰ó
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}

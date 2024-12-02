using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // 飛び道具のPrefab
    public Transform shootPoint;       // 飛び道具を発射する位置
    public float projectileSpeed = 10f; // 飛び道具のスピード

    void Update()
    {
        // Kキーで発射
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 飛び道具を生成
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Rigidbody2Dで飛ばす
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.right * projectileSpeed; // キャラクターの向きに応じて飛ばす
        }
    }
}

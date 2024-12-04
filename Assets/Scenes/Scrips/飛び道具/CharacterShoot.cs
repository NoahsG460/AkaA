using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // 飛び道具のPrefab
    public Transform shootPoint;       // 発射ポイントのTransform
    public float projectileSpeed = 10f; // 飛び道具のスピード

    private float lastDirection = 1f; // 最後に押されたキーの方向（1:右, -1:左）

    void Update()
    {
        // Aキーで左、Dキーで右の方向を記録
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastDirection = -1f;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastDirection = 1f;
        }

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
            rb.velocity = new Vector2(lastDirection * projectileSpeed, 0f); // 最後の方向に飛ばす
        }

        // デバッグログで確認
        Debug.Log("Projectile Direction: " + (lastDirection > 0 ? "Right" : "Left"));
    }
}

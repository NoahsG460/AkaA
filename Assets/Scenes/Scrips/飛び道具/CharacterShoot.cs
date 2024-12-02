using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // 飛び道具のPrefab
    public Transform shootPoint;       // 発射する位置
    public float projectileSpeed = 10f; // 飛び道具のスピード
    private bool facingRight = true;   // キャラクターの向き (右向きがデフォルト)

    void Update()
    {
        // 発射処理
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }

        // キャラクターの向きを切り替える（例: 左右移動に合わせて）
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
        // 飛び道具を生成
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Rigidbody2Dで飛ばす
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // キャラクターの向きに応じた速度を設定
            Vector2 shootDirection = facingRight ? Vector2.right : Vector2.left;
            rb.velocity = shootDirection * projectileSpeed;
        }
    }
}

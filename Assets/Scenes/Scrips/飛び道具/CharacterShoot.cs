using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // 飛び道具のPrefab
    public Transform shootPoint;       // 飛び道具を発射する位置
    public float projectileSpeed = 10f; // 飛び道具のスピード
    private bool facingRight = true;   // キャラクターが右向きかどうか

    void Update()
    {
        // A, Dキーで移動入力を確認して向きを更新
        if (Input.GetKey(KeyCode.A))
        {
            if (facingRight) Flip();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!facingRight) Flip();
        }

        // 発射処理
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
            // キャラクターの向きに応じた速度を設定
            Vector2 shootDirection = facingRight ? Vector2.right : Vector2.left;
            rb.velocity = shootDirection * projectileSpeed;
        }

        // 自分との衝突を無視
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D projectileCollider = projectile.GetComponent<Collider2D>();
        if (playerCollider != null && projectileCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, projectileCollider);
        }
    }

    void Flip()
    {
        // キャラクターの向きを反転
        facingRight = !facingRight;

        // キャラクターのスプライトの向きを反転させる
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // X軸を反転
        transform.localScale = localScale;
    }
}

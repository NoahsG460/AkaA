using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // 飛び道具のPrefab
    public Transform shootPoint;       // 発射ポイントのTransform
    public float projectileSpeed = 10f; // 飛び道具のスピード
    public SpriteRenderer characterSprite; // キャラクターのスプライト

    private float lastDirection = 1f; // 最後に押されたキーの方向（1:右, -1:左）

    void Update()
    {
        // Aキーで左、Dキーで右の方向を記録
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastDirection = -1f;
            UpdateCharacterDirection(); // キャラクターの向きを更新
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastDirection = 1f;
            UpdateCharacterDirection(); // キャラクターの向きを更新
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

    void UpdateCharacterDirection()
    {
        // キャラクターのスプライトを反転させる
        if (characterSprite != null)
        {
            characterSprite.flipX = lastDirection < 0; // 左を向くときスプライトを反転
        }

        // 発射ポイントの位置を更新（左右反転）
        if (shootPoint != null)
        {
            Vector3 localPosition = shootPoint.localPosition;
            localPosition.x = Mathf.Abs(localPosition.x) * lastDirection; // x軸を反転
            shootPoint.localPosition = localPosition;
        }
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;         // 飛び道具のダメージ
    public float lifetime = 100f;   // 飛び道具が自動的に消えるまでの時間

    void Start()
    {
        // 一定時間後に削除
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 敵に当たったらダメージを与える
        EnemyManager enemy = collision.gameObject.GetComponent<EnemyManager>();
        if (enemy != null)
        {
            enemy.OnDamage(damage); // 敵にダメージ処理を適用
        }

        // 自身を削除
        Destroy(gameObject);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public int maxHp = 3; // 敵の最大HP
    public int hp; // 現在のHP
    public float moveSpeed = 3f; // 敵の移動速度
    public Transform attackPoint; // 攻撃の発生位置
    public float attackRadius; // 攻撃範囲
    public LayerMask enemyLayer; // 攻撃対象のレイヤー
    public Image EnemyHP; // HPバーのImage（UI要素）

    private Rigidbody2D rb;
    private Animator animator;
    private int attackPower = 1; // 攻撃力

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // 現在のHPを最大値に初期化
        hp = maxHp;

        // HPバーの初期設定
        if (EnemyHP != null)
        {
            EnemyHP.fillAmount = 1f; // 初期状態では最大値
        }
        else
        {
            Debug.LogWarning("EnemyHP Image がアサインされていません。");
        }
    }

    void Update()
    {
        // 攻撃処理
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("攻撃");
            Attack();
        }

        // 敵の移動処理
        EnemyMovement();
    }

    void EnemyMovement()
    {
        float x = Input.GetAxisRaw("HorizontalEnemy"); // 横方向の入力 (A/Dキーや矢印キー)
        if (x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // 右向きにスプライトを反転
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // 左向きにスプライトを反転
        }

        animator.SetFloat("Speed", Mathf.Abs(x)); // アニメーションのスピード設定
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y); // 移動を適用
    }

    void Attack()
    {
        if (attackPoint == null)
        {
            Debug.LogWarning("attackPointがアサインされていません。攻撃できません。");
            return;
        }

        animator.SetTrigger("IsAttack");
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        foreach (Collider2D hitTarget in hitTargets)
        {
            PlayerManager player = hitTarget.GetComponent<PlayerManager>();
            if (player != null)
            {
                Debug.Log(hitTarget.gameObject.name + "に攻撃");
                player.OnDamage(attackPower); // プレイヤーにダメージを与える
            }
            else
            {
                Debug.LogWarning(hitTarget.gameObject.name + "にはPlayerManagerがありません");
            }
        }
    }

    public void OnDamage(int damage)
    {
        hp -= damage;

        if (hp < 0)
        {
            hp = 0; // HPが負の値にならないよう制限
        }

        // HPバーを更新
        if (EnemyHP != null)
        {
            EnemyHP.fillAmount = (float)hp / maxHp; // 最大HPに基づいて割合を設定
        }
        else
        {
            Debug.LogWarning("EnemyHP Image がアサインされていません。");
        }

        animator.SetTrigger("IsHurt");

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        Debug.Log("敵が死亡しました");

        // 必要なら敵を削除する処理を追加
        Destroy(gameObject, 2f); // 2秒後に敵を削除
    }

    // 攻撃範囲をギズモで表示
    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
        else
        {
            Debug.LogWarning("attackPointが設定されていません。Gizmosは描画されません。");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Me"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;
    Rigidbody2D rb;
    Animator animator;
    public int hp = 5; // プレイヤーのHPを設定
    int attackPower = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // スペースキーで攻撃
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        // プレイヤーの移動
        Movement();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal"); // 横方向の入力 (A/Dキーや矢印キー)
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
        animator.SetTrigger("IsAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        foreach (Collider2D hitEnemy in hitEnemies)
        {
            Debug.Log(hitEnemy.gameObject.name + "に攻撃");
            hitEnemy.GetComponent<EnemyManager>().OnDamage(attackPower); // 敵にダメージを与える
        }
    }

    // プレイヤーがダメージを受けたときの処理
    public void OnDamage(int damage)
    {
        hp -= damage;
        animator.SetTrigger("IsHurt");
        Debug.Log("プレイヤーが" + damage + "ダメージを受けた");
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        hp = 0;
        animator.SetTrigger("Die");
        Debug.Log("プレイヤーが死亡しました");
        // プレイヤーが死んだときの処理（例：リスポーンやゲームオーバー処理）
    }

    // 攻撃範囲をギズモで表示
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

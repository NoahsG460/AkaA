using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f; // ジャンプ力を設定
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;
    Rigidbody2D rb;
    Animator animator;
    public int hp = 5; // プレイヤーのHPを設定
    int attackPower = 1;
    private bool isGrounded; // 地面に接地しているかの判定
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Jキーで攻撃
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
        // Kキーで飛び道具を発射
        if (Input.GetKeyDown(KeyCode.K))
        {
            ShootProjectile();
        }

        // スペースキーでジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            Debug.Log("ジャンプがトリガーされました");
        }

        // プレイヤーの移動
        Movement();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal"); // 横方向の入力 (A/Dキーや矢印キー)

        if (x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // 右向き
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // 左向き
        }

        animator.SetFloat("Speed", Mathf.Abs(x)); // アニメーションのスピード設定
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y); // 移動を適用
    }

    void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        // プレイヤーの向きに応じて弾の方向を変更
        if (transform.localScale.x < 0) // 左向き
        {
            projectileRb.velocity = Vector2.left * 10f; // 左方向に発射
        }
        else // 右向き
        {
            projectileRb.velocity = Vector2.right * 10f; // 右方向に発射
        }
    }


    void Attack()
    {
        animator.SetTrigger("IsAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        foreach (Collider2D hitEnemy in hitEnemies)
        {
            Debug.Log(hitEnemy.gameObject.name + "に攻撃");

            // ダメージ処理を一時的にコメントアウト
            /*
            var damageable = hitEnemy.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.OnDamage(attackPower);
            }
            */
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // ジャンプを適用
        isGrounded = false; // ジャンプ中は地面にいないと判定
    }

    // 地面に接触しているかを判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // プレイヤーがダメージを受けたときの処理（コメントアウト）
    /*
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
    */

    // 攻撃範囲をギズモで表示
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

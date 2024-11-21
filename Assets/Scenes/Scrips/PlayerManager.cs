using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float boostedSpeed = 6f; // シフトキーで速くなる速度
    public float jumpForce = 5f; // ジャンプ力を設定
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;
    Rigidbody2D rb;
    Animator animator;
    public int hp = 5; // プレイヤーのHPを設定
    int attackPower = 1;
    private bool isGrounded; // 地面に接地しているかの判定
    private bool isBoosting; // スピードアップ中かを判定
    private float currentSpeed; // 現在の移動速度

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSpeed = moveSpeed; // 初期速度を設定
    }

    void Update()
    {
        // Jキーで攻撃
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }

        // スペースキーでジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            Debug.Log("ジャンプがトリガーされました");
        }

        // シフトキーでスピードアップ（地上にいるときのみ）
        if (isGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            isBoosting = true;
        }
        else if (isGrounded)
        {
            isBoosting = false;
        }

        // プレイヤーの移動
        Movement();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal"); // 横方向の入力 (A/Dキーや矢印キー)

        // 現在の速度を計算（地上でのみブースト適用）
        currentSpeed = isBoosting ? boostedSpeed : moveSpeed;

        // 向きの変更
        if (x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // 右向きにスプライトを反転
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // 左向きにスプライトを反転
        }

        animator.SetFloat("Speed", Mathf.Abs(x)); // アニメーションのスピード設定

        // 移動を適用
        rb.velocity = new Vector2(x * currentSpeed, rb.velocity.y);
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

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // ジャンプを適用
        isGrounded = false; // ジャンプ中は地面にいないと判定
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Me"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // 地面に接地
        }
    }

    // 攻撃範囲をギズモで表示
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}


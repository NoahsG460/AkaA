using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform attackPoint; // 攻撃の発生位置
    public float attackRadius; // 攻撃範囲
    public LayerMask enemyLayer; // 攻撃対象のレイヤー
    Rigidbody2D rb;
    Animator animator;
    public int hp = 3; // 敵のHP
    int attackPower = 1; // 攻撃力

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        
    }

    void Update()
    {
        // エンターキーで攻撃
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


    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal"); // 横方向の入力 (標準の入力を使用)
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
        animator.SetTrigger("IsHunt");
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

        // 一定時間待機してからシーン遷移
        StartCoroutine(GameOverTransition());
    }

    // ゲームオーバー画面への遷移
    IEnumerator GameOverTransition()
    {
        yield return new WaitForSeconds(2f); // アニメーションが終わるまで待機（調整可能）
        UnityEngine.SceneManagement.SceneManager.LoadScene("リザルト");
    }


    // 攻撃範囲をギズモで表示
    public void OnDrawGizmosSelected()
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

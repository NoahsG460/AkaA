using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform attackPoint; // 攻撃位置
    public float attackRadius;    // 攻撃の半径
    public LayerMask enemyLayer;
    public float attackDelay = 0.2f;  // 攻撃判定が出るまでの遅延
    public float attackDuration = 0.3f; // 攻撃判定の持続時間
    Animator animator;
    public int hp = 5; // プレイヤーのHPを設定
    int attackPower = 1;
    private Coroutine attackCoroutine;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (attackCoroutine == null) // 攻撃中でない場合のみ実行
                attackCoroutine = StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.Space) && GetComponent<PlayerStamina>().IsGrounded)
        {
            Jump();
        }
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("IsAttack");

        // 攻撃判定を遅延させる
        yield return new WaitForSeconds(attackDelay);

        // 攻撃判定を有効化
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

        foreach (Collider2D hitEnemy in hitEnemies)
        {
            Debug.Log(hitEnemy.gameObject.name + "に攻撃");
            hitEnemy.GetComponent<EnemyManager>().OnDamage(attackPower);
        }

        // 攻撃判定の持続時間
        yield return new WaitForSeconds(attackDuration);

        // 攻撃完了
        attackCoroutine = null;
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<PlayerStamina>().JumpForce);
        GetComponent<PlayerStamina>().IsGrounded = false;
    }

    public void OnDamage(int damage)
    {
        hp -= damage;
        animator.SetTrigger("IsHurt");
        Debug.Log("プレイヤーが" + damage + "ダメージを受けた");

        // HPバーを管理するやつです
        //GameObject director = GameObject.Find("HPDirector");
        //director.GetComponent<HPDirector>().DecreaseHP();

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


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Me"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<PlayerStamina>().IsGrounded = true; // 地面に接地
        }
    }

    // 攻撃範囲をギズモで表示
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float boostedSpeed = 6f;
    public float jumpForce = 5f;
    public Transform attackPoint;
    public BoxCollider2D attackCollider;
    public float attackRadius;
    public LayerMask enemyLayer;
    Rigidbody2D rb;
    Animator animator;
    public int hp = 5;
    int attackPower = 1;
    private bool isGrounded;
    private bool isBoosting;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (isGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            isBoosting = true;
        }
        else if (isGrounded)
        {
            isBoosting = false;
        }

        Movement();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        currentSpeed = isBoosting ? boostedSpeed : moveSpeed;

        if (x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        animator.SetFloat("Speed", Mathf.Abs(x));
        rb.velocity = new Vector2(x * currentSpeed, rb.velocity.y);
    }

    void Attack()
    {
        animator.SetTrigger("IsAttack");
        attackCollider.enabled = true;

        List<Collider2D> hitEnemies = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(enemyLayer);
        Physics2D.OverlapCollider(attackCollider, contactFilter, hitEnemies);

        foreach (Collider2D hitEnemy in hitEnemies)
        {
            Debug.Log(hitEnemy.gameObject.name + "に攻撃");
            hitEnemy.GetComponent<EnemyManager>().OnDamage(attackPower);
        }

        attackCollider.enabled = false;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
    }

    public void OnDamage(int damage)
    {
        hp -= damage;
        animator.SetTrigger("IsHurt");
        Debug.Log("プレイヤーが" + damage + "ダメージを受けた");

        GameObject director = GameObject.Find("HPDirector");
        director.GetComponent<HPDirector>().DecreaseHP();

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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Me"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (attackCollider != null)
        {
            // コライダーのワールド座標を計算
            Vector3 colliderPosition = attackCollider.transform.position + (Vector3)attackCollider.offset;
            Vector3 colliderSize = attackCollider.size;

            // ギズモでボックスを描画
            Gizmos.DrawWireCube(colliderPosition, colliderSize);
        }
    }
}

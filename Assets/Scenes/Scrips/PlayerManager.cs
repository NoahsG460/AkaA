using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPun
{
    public float moveSpeed = 3f;
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;
    private Rigidbody2D rb;
    private Animator animator;
    public int hp = 5;
    int attackPower = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (!photonView.IsMine)
        {
            enabled = false; // 他プレイヤーの操作を無効化
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Movement();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if (x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        animator.SetFloat("Speed", Mathf.Abs(x));
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
    }

    void Attack()
    {
        animator.SetTrigger("IsAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        foreach (Collider2D hitEnemy in hitEnemies)
        {
            hitEnemy.GetComponent<PlayerManager>().OnDamage(attackPower);
        }
    }

    public void OnDamage(int damage)
    {
        hp -= damage;
        animator.SetTrigger("IsHurt");
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("プレイヤーが死亡しました");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

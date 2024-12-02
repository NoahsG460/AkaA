using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform attackPoint; // UŒ‚ˆÊ’u
    public float attackRadius;    // UŒ‚‚Ì”¼Œa
    public LayerMask enemyLayer;
    public float attackDelay = 0.2f;  // UŒ‚”»’è‚ªo‚é‚Ü‚Å‚Ì’x‰„
    public float attackDuration = 0.3f; // UŒ‚”»’è‚Ì‘±ŠÔ
    Animator animator;
    public int hp = 5; // ƒvƒŒƒCƒ„[‚ÌHP‚ğİ’è
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
            if (attackCoroutine == null) // UŒ‚’†‚Å‚È‚¢ê‡‚Ì‚İÀs
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

        // UŒ‚”»’è‚ğ’x‰„‚³‚¹‚é
        yield return new WaitForSeconds(attackDelay);

        // UŒ‚”»’è‚ğ—LŒø‰»
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

        foreach (Collider2D hitEnemy in hitEnemies)
        {
            Debug.Log(hitEnemy.gameObject.name + "‚ÉUŒ‚");
            hitEnemy.GetComponent<EnemyManager>().OnDamage(attackPower);
        }

        // UŒ‚”»’è‚Ì‘±ŠÔ
        yield return new WaitForSeconds(attackDuration);

        // UŒ‚Š®—¹
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
        Debug.Log("ƒvƒŒƒCƒ„[‚ª" + damage + "ƒ_ƒ[ƒW‚ğó‚¯‚½");

        // HPƒo[‚ğŠÇ—‚·‚é‚â‚Â‚Å‚·
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
        Debug.Log("ƒvƒŒƒCƒ„[‚ª€–S‚µ‚Ü‚µ‚½");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Me"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<PlayerStamina>().IsGrounded = true; // ’n–Ê‚ÉÚ’n
        }
    }

    // UŒ‚”ÍˆÍ‚ğƒMƒYƒ‚‚Å•\¦
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}

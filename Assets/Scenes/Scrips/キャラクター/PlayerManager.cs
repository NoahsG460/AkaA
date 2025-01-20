using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform attackPoint; // �U���ʒu
    public float attackRadius;    // �U���̔��a
    public LayerMask enemyLayer;
    public float attackDelay = 0.2f;  // �U�����肪�o��܂ł̒x��
    public float attackDuration = 0.3f; // �U������̎�������
    Animator animator;
    public int hp = 5; // �v���C���[��HP��ݒ�
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
            if (attackCoroutine == null) // �U�����łȂ��ꍇ�̂ݎ��s
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

        // �U�������x��������
        yield return new WaitForSeconds(attackDelay);

        // �U�������L����
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

        foreach (Collider2D hitEnemy in hitEnemies)
        {
            Debug.Log(hitEnemy.gameObject.name + "�ɍU��");
            hitEnemy.GetComponent<EnemyManager>().OnDamage(attackPower);
        }

        // �U������̎�������
        yield return new WaitForSeconds(attackDuration);

        // �U������
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
        Debug.Log("�v���C���[��" + damage + "�_���[�W���󂯂�");

        // HP�o�[���Ǘ������ł�
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
        Debug.Log("�v���C���[�����S���܂���");

        // ��莞�ԑҋ@���Ă���V�[���J��
        StartCoroutine(GameOverTransition());
    }

    // �Q�[���I�[�o�[��ʂւ̑J��
    IEnumerator GameOverTransition()
    {
        yield return new WaitForSeconds(2f); // �A�j���[�V�������I���܂őҋ@�i�����\�j
        UnityEngine.SceneManagement.SceneManager.LoadScene("���U���g");
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Me"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<PlayerStamina>().IsGrounded = true; // �n�ʂɐڒn
        }
    }

    // �U���͈͂��M�Y���ŕ\��
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f; // �W�����v�͂�ݒ�
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;
    Rigidbody2D rb;
    Animator animator;
    public int hp = 5; // �v���C���[��HP��ݒ�
    int attackPower = 1;
    private bool isGrounded; // �n�ʂɐڒn���Ă��邩�̔���

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // J�L�[�ōU��
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }

        // �X�y�[�X�L�[�ŃW�����v
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            Debug.Log("�W�����v���g���K�[����܂���");
        }

        // �v���C���[�̈ړ�
        Movement();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal"); // �������̓��� (A/D�L�[����L�[)
        if (x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // �E�����ɃX�v���C�g�𔽓]
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // �������ɃX�v���C�g�𔽓]
        }

        animator.SetFloat("Speed", Mathf.Abs(x)); // �A�j���[�V�����̃X�s�[�h�ݒ�
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y); // �ړ���K�p
    }

    void Attack()
    {
        animator.SetTrigger("IsAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        foreach (Collider2D hitEnemy in hitEnemies)
        {
            Debug.Log(hitEnemy.gameObject.name + "�ɍU��");
            hitEnemy.GetComponent<EnemyManager>().OnDamage(attackPower); // �G�Ƀ_���[�W��^����
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // �W�����v��K�p
        isGrounded = false; // �W�����v���͒n�ʂɂ��Ȃ��Ɣ���
    }

    // �n�ʂɐڐG���Ă��邩�𔻒�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // �v���C���[���_���[�W���󂯂��Ƃ��̏���
    public void OnDamage(int damage)
    {
        hp -= damage;
        animator.SetTrigger("IsHurt");
        Debug.Log("�v���C���[��" + damage + "�_���[�W���󂯂�");
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
        // �v���C���[�����񂾂Ƃ��̏����i��F���X�|�[����Q�[���I�[�o�[�����j
    }

    // �U���͈͂��M�Y���ŕ\��
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

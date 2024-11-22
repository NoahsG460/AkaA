using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float boostedSpeed = 6f; // �V�t�g�L�[�ő����Ȃ鑬�x
    public float jumpForce = 5f; // �W�����v�͂�ݒ�
    public Transform attackPoint;
    public float attackRadius; // �U���̔��a
    public LayerMask enemyLayer;
    Rigidbody2D rb;
    Animator animator;
    public int hp = 5; // �v���C���[��HP��ݒ�
    int attackPower = 1;
    private bool isGrounded; // �n�ʂɐڒn���Ă��邩�̔���
    private bool isBoosting; // �X�s�[�h�A�b�v�����𔻒�
    private float currentSpeed; // ���݂̈ړ����x

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            Debug.Log("�W�����v���g���K�[����܂���");
        }

        // �V�t�g�L�[�ŃX�s�[�h�A�b�v�i�n��ɂ���Ƃ��̂݁j
        if (isGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            isBoosting = true;
        }
        else if (isGrounded)
        {
            isBoosting = false;
        }

        // �v���C���[�̈ړ�
        Movement();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal"); // �������̓��� (A/D�L�[����L�[)

        // ���݂̑��x���v�Z�i�n��ł̂݃u�[�X�g�K�p�j
        currentSpeed = isBoosting ? boostedSpeed : moveSpeed;

        // �����̕ύX
        if (x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        animator.SetFloat("Speed", Mathf.Abs(x)); // �A�j���[�V�����̃X�s�[�h�ݒ�

        // �ړ���K�p
        rb.velocity = new Vector2(x * currentSpeed, rb.velocity.y);
    }

    void Attack()
    {
        animator.SetTrigger("IsAttack");

        // �U���͈͓��̓G�����o
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

        foreach (Collider2D hitEnemy in hitEnemies)
        {
            Debug.Log(hitEnemy.gameObject.name + "�ɍU��");
            hitEnemy.GetComponent<EnemyManager>().OnDamage(attackPower);
        }
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
        Debug.Log("�v���C���[��" + damage + "�_���[�W���󂯂�");

        // HP�o�[���Ǘ������ł�
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
        Debug.Log("�v���C���[�����S���܂���");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Me"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // �n�ʂɐڒn
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

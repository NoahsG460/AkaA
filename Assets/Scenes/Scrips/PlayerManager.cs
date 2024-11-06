using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    [SerializeField] private PolygonCollider2D attackCollider; // �C���X�y�N�^�Őݒ�\�ɂ���
    public LayerMask enemyLayer;
    private Rigidbody2D rb;
    private Animator animator;
    public int hp = 5;
    private int attackPower = 1;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackCollider.enabled = false; // �U�����̂ݗL���ɂ��邽�߁A�ŏ��͖�����
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

        Movement();
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
        attackCollider.enabled = true; // �U���͈͂��ꎞ�I�ɗL����

        // �q�b�g�����G���i�[���郊�X�g��p��
        List<Collider2D> hitEnemies = new List<Collider2D>();

        // �I�[�o�[���b�v�����R���C�_�[���擾
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(enemyLayer);
        Physics2D.OverlapCollider(attackCollider, contactFilter, hitEnemies);

        foreach (Collider2D hitEnemy in hitEnemies)
        {
            Debug.Log(hitEnemy.gameObject.name + "�ɍU��");
            hitEnemy.GetComponent<EnemyManager>().OnDamage(attackPower);
        }

        attackCollider.enabled = false; // �U����ɖ�����
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

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
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (attackCollider != null)
        {
            Vector2[] points2D = attackCollider.points;
            Vector3[] points3D = new Vector3[points2D.Length];

            for (int i = 0; i < points2D.Length; i++)
            {
                points3D[i] = attackCollider.transform.TransformPoint(points2D[i]);
            }

            // �e���_�Ԃɐ��������ă|���S����`��
            for (int i = 0; i < points3D.Length; i++)
            {
                Vector3 start = points3D[i];
                Vector3 end = points3D[(i + 1) % points3D.Length];
                Gizmos.DrawLine(start, end);
            }
        }
    }
}

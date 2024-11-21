using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 3f;
<<<<<<< HEAD
    public float jumpForce = 5f;
    [SerializeField] private PolygonCollider2D attackCollider; // �C���X�y�N�^�Őݒ�\�ɂ���
    public LayerMask enemyLayer;
    private Rigidbody2D rb;
    private Animator animator;
    public int hp = 5;
    private int attackPower = 1;
    private bool isGrounded;
=======
    public float boostedSpeed = 6f; // �V�t�g�L�[�ő����Ȃ鑬�x
    public float jumpForce = 5f; // �W�����v�͂�ݒ�
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;
    Rigidbody2D rb;
    Animator animator;
    public int hp = 5; // �v���C���[��HP��ݒ�
    int attackPower = 1;
    private bool isGrounded; // �n�ʂɐڒn���Ă��邩�̔���
    private bool isBoosting; // �X�s�[�h�A�b�v�����𔻒�
    private float currentSpeed; // ���݂̈ړ����x
>>>>>>> 走りとプレイヤーマネージャー分ける

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
<<<<<<< HEAD
        attackCollider.enabled = false; // �U�����̂ݗL���ɂ��邽�߁A�ŏ��͖�����
=======
        currentSpeed = moveSpeed; // �������x��ݒ�
>>>>>>> 走りとプレイヤーマネージャー分ける
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

<<<<<<< HEAD
=======
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
>>>>>>> 走りとプレイヤーマネージャー分ける
        Movement();
    }

    void Movement()
    {
<<<<<<< HEAD
        float x = Input.GetAxisRaw("Horizontal");
=======
        float x = Input.GetAxisRaw("Horizontal"); // �������̓��� (A/D�L�[����L�[)

        // ���݂̑��x���v�Z�i�n��ł̂݃u�[�X�g�K�p�j
        currentSpeed = isBoosting ? boostedSpeed : moveSpeed;

        // �����̕ύX
>>>>>>> 走りとプレイヤーマネージャー分ける
        if (x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

<<<<<<< HEAD
        animator.SetFloat("Speed", Mathf.Abs(x));
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
=======
        animator.SetFloat("Speed", Mathf.Abs(x)); // �A�j���[�V�����̃X�s�[�h�ݒ�

        // �ړ���K�p
        rb.velocity = new Vector2(x * currentSpeed, rb.velocity.y);
>>>>>>> 走りとプレイヤーマネージャー分ける
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

<<<<<<< HEAD
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

=======
    // �v���C���[���_���[�W���󂯂��Ƃ��̏���
>>>>>>> 走りとプレイヤーマネージャー分ける
    public void OnDamage(int damage)
    {
        hp -= damage;
        animator.SetTrigger("IsHurt");
        Debug.Log("�v���C���[��" + damage + "�_���[�W���󂯂�");
        //HP�o�[�̊Ǘ������ł�
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

<<<<<<< HEAD
=======
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
>>>>>>> 走りとプレイヤーマネージャー分ける
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


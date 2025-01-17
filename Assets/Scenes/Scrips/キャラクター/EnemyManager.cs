using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform attackPoint; // �U���̔����ʒu
    public float attackRadius; // �U���͈�
    public LayerMask enemyLayer; // �U���Ώۂ̃��C���[
    Rigidbody2D rb;
    Animator animator;
    public int hp = 3; // �G��HP
    int attackPower = 1; // �U����

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        
    }

    void Update()
    {
        // �G���^�[�L�[�ōU��
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("�U��");
            Attack();
        }

        // �G�̈ړ�����
        EnemyMovement();

    }
    void EnemyMovement()
    {
        float x = Input.GetAxisRaw("HorizontalEnemy"); // �������̓��� (A/D�L�[����L�[)
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


    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal"); // �������̓��� (�W���̓��͂��g�p)
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
        if (attackPoint == null)
        {
            Debug.LogWarning("attackPoint���A�T�C������Ă��܂���B�U���ł��܂���B");
            return;
        }

        animator.SetTrigger("IsAttack");
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        foreach (Collider2D hitTarget in hitTargets)
        {
            PlayerManager player = hitTarget.GetComponent<PlayerManager>();
            if (player != null)
            {
                Debug.Log(hitTarget.gameObject.name + "�ɍU��");
                player.OnDamage(attackPower); // �v���C���[�Ƀ_���[�W��^����
            }
            else
            {
                Debug.LogWarning(hitTarget.gameObject.name + "�ɂ�PlayerManager������܂���");
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


    // �U���͈͂��M�Y���ŕ\��
    public void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
        else
        {
            Debug.LogWarning("attackPoint���ݒ肳��Ă��܂���BGizmos�͕`�悳��܂���B");
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

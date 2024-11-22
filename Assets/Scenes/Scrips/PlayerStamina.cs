using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float boostedSpeed = 6f; // �V�t�g�L�[�ő����Ȃ鑬�x
    public float jumpForce = 5f; // �W�����v�͂�ݒ�
    public bool IsGrounded { get; set; } // �n�ʂɐڒn���Ă��邩�̔���
    private bool isBoosting; // �X�s�[�h�A�b�v�����𔻒�
    private float currentSpeed; // ���݂̈ړ����x
    Rigidbody2D rb;
    Animator animator;

    public float JumpForce => jumpForce; // �W�����v�͂����J

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // �V�t�g�L�[�ŃX�s�[�h�A�b�v�i�n��ɂ���Ƃ��̂݁j
        if (IsGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            isBoosting = true;
        }
        else if (IsGrounded)
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public float maxStamina = 100f; // �X�^�~�i�̍ő�l
    private float currentStamina; // ���݂̃X�^�~�i�l
    public float staminaDrainRate = 10f; // �X�^�~�i����x�i���b�j
    public float staminaRecoveryRate = 5f; // �X�^�~�i�񕜑��x�i���b�j
    public Image StaminaGauge;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentStamina = maxStamina; // �X�^�~�i���ő�l�ɐݒ�
        if (StaminaGauge != null)
        {
            StaminaGauge.fillAmount = 1f;
        }
        else
        {
            Debug.LogWarning("StaminaGauge Image ���A�T�C������Ă��܂���B");
        }
    }

    void Update()
    {
        // �X�^�~�i�̉�
        if (!isBoosting && currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }

        // �V�t�g�L�[�ŃX�s�[�h�A�b�v�i�n��ɂ���Ƃ��A�X�^�~�i������ꍇ�̂݁j
        if (IsGrounded && Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            isBoosting = true;
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }
        else
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

        if (StaminaGauge != null)
        {
            StaminaGauge.fillAmount = (float)currentStamina / maxStamina;
        }
    }
}

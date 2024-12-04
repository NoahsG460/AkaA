using System.Collections;
using UnityEngine;

public class PlayerGuard : MonoBehaviour
{
    public int maxGuardStock = 3;        // �ő�X�g�b�N��
    public float guardRechargeTime = 5f; // �X�g�b�N��1�񕜂���܂ł̎���
    public float guardCooldown = 0.5f;   // �K�[�h������̌㌄�i�N�[���_�E���j
    private int currentGuardStock;      // ���݂̃K�[�h�X�g�b�N��
    private bool canGuard = true;       // �K�[�h�\���ǂ���
    private float currentCooldown = 0f; // �N�[���_�E���̃J�E���g

    private Animator animator;          // �A�j���[�V��������p

    void Start()
    {
        currentGuardStock = maxGuardStock; // �����X�g�b�N����ݒ�
        animator = GetComponent<Animator>();
        StartCoroutine(RechargeGuard());   // �X�g�b�N�񕜃R���[�`�����J�n

        Debug.Log("�K�[�h�@�\������������܂���: ���݂̃X�g�b�N�� = " + currentGuardStock);
    }

    void Update()
    {
        // �K�[�h���͂Ə���
        if (Input.GetKeyDown(KeyCode.L) && canGuard && currentGuardStock > 0)
        {
            Debug.Log("�K�[�h���͂��󂯎��܂���");
            StartCoroutine(PerformGuard());
        }
        else if (Input.GetKeyDown(KeyCode.L) && currentGuardStock == 0)
        {
            Debug.Log("�K�[�h���͂��󂯎��܂������A�X�g�b�N������܂���I");
        }

        // �㌄�̃N�[���_�E������
        if (!canGuard)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0f)
            {
                canGuard = true;
                Debug.Log("�㌄���I�����܂����B�K�[�h���Ăщ\�ł�");
            }
        }
    }

    IEnumerator PerformGuard()
    {
        // �K�[�h�����s
        currentGuardStock--;  // �X�g�b�N��1����
        canGuard = false;     // �㌄���������邽�߃K�[�h�s��
        currentCooldown = guardCooldown;

        if (animator != null)
        {
            animator.SetTrigger("IsGuard"); // �K�[�h�A�j���[�V�������Đ�
        }

        Debug.Log("�K�[�h�����I �c��X�g�b�N: " + currentGuardStock);

        // �K�[�h���̏����i�K�v�Ȃ�ҋ@���Ԃ�ݒ�\�j
        yield return new WaitForSeconds(0.3f);

        Debug.Log("�K�[�h���I�����܂����B�㌄���J�n���܂�");
    }

    IEnumerator RechargeGuard()
    {
        while (true)
        {
            if (currentGuardStock < maxGuardStock)
            {
                yield return new WaitForSeconds(guardRechargeTime);
                currentGuardStock++;
                Debug.Log("�K�[�h�X�g�b�N���񕜂��܂���: ���݂̃X�g�b�N�� = " + currentGuardStock);
            }
            else
            {
                yield return null; // �ő�X�g�b�N�Ȃ牽�����Ȃ�
            }
        }
    }

    public int GetCurrentGuardStock()
    {
        return currentGuardStock;
    }

    public int GetMaxGuardStock()
    {
        return maxGuardStock;
    }
}

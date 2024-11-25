using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // �ő�HP
    private float currentHealth; // ���݂�HP

    public Image hpBarFill; // HP�o�[�� `Fill` ����

    void Start()
    {
        currentHealth = maxHealth; // �J�n���͍ő�HP
        UpdateHealthBar();
    }

    // HP�����������郁�\�b�h
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // HP��͈͓��ɐ���
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead!");
            // �K�v�Ȃ玀�S������ǉ�
        }
    }

    // HP���񕜂����郁�\�b�h
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    // HP�o�[���X�V����
    void UpdateHealthBar()
    {
        if (hpBarFill != null)
        {
            hpBarFill.fillAmount = currentHealth / maxHealth; // HP�̊����ɉ����ăo�[���X�V
        }
    }
}

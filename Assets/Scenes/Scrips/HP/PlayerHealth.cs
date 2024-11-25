using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // 最大HP
    private float currentHealth; // 現在のHP

    public Image hpBarFill; // HPバーの `Fill` 部分

    void Start()
    {
        currentHealth = maxHealth; // 開始時は最大HP
        UpdateHealthBar();
    }

    // HPを減少させるメソッド
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // HPを範囲内に制限
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead!");
            // 必要なら死亡処理を追加
        }
    }

    // HPを回復させるメソッド
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    // HPバーを更新する
    void UpdateHealthBar()
    {
        if (hpBarFill != null)
        {
            hpBarFill.fillAmount = currentHealth / maxHealth; // HPの割合に応じてバーを更新
        }
    }
}

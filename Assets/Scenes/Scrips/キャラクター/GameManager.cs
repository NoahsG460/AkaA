using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // シングルトンのインスタンス

    public GameObject[] characterPrefabs; // キャラクタープレハブの配列
    public Transform spawnPoint;          // キャラクター生成位置
    private GameObject currentPlayer;     // 現在操作中のキャラクター

    void Awake()
    {
        // シングルトンインスタンスの設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいで破棄されない
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスがあれば削除
        }
    }

    public void OnCharacterSelect(int characterIndex)
    {
        // 既存のキャラクターを削除
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }

        // 新しいキャラクターを生成
        currentPlayer = Instantiate(characterPrefabs[characterIndex], spawnPoint.position, Quaternion.identity);

        // 操作可能に設定
        PlayerStamina playerStamina = currentPlayer.GetComponent<PlayerStamina>();
        if (playerStamina != null)
        {
            playerStamina.isControlled = true; // 新しいキャラクターを操作可能に
        }
        else
        {
            Debug.LogWarning("PlayerStaminaがアタッチされていません");
        }
    }
}

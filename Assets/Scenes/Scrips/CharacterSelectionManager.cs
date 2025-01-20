using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    public static CharacterSelectionManager Instance { get; private set; }

    void Awake()
    {
        // シングルトンインスタンスの初期化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン切り替えでもインスタンスを保持
        }
        else
        {
            Destroy(gameObject); // すでにインスタンスがある場合、このオブジェクトを破棄
        }
    }

    public int selectedCharacterIndex; // 選択されたキャラクターのインデックス
}

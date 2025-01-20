using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform spawnPoint; // キャラクターのスポーン位置
    public GameObject[] characterPrefabs; // キャラクターのプレハブ配列

    void Start()
    {
        // 選択されたキャラクターを取得
        int selectedIndex = CharacterSelectionManager.Instance.selectedCharacterIndex;

        if (selectedIndex >= 0 && selectedIndex < characterPrefabs.Length)
        {
            // 選択されたキャラクターをスポーン
            Instantiate(characterPrefabs[selectedIndex], spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("選択されたキャラクターのインデックスが無効です: " + selectedIndex);
        }
    }
}

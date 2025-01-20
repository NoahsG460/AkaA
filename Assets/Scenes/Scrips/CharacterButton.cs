using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterButton : MonoBehaviour
{
    public int characterIndex;  // キャラクター選択インデックス
    public string sceneName = "SampleScene";  // 遷移先のシーン名
    private bool isCharacterSelected = false;  // キャラクター選択フラグ

    public Button selectButton;  // キャラクター選択ボタン
    public Button confirmButton;  // 決定ボタン

    void Start()
    {
        // 決定ボタンはキャラクターが選ばれるまで無効化
        confirmButton.interactable = false;

        selectButton.onClick.AddListener(OnCharacterSelected);  // キャラクター選択ボタンのリスナーを設定
        confirmButton.onClick.AddListener(OnConfirmSelection);  // 決定ボタンのリスナーを設定
    }

    // キャラクター選択された時の処理
    void OnCharacterSelected()
    {
        // キャラクター選択フラグを立てて、決定ボタンを有効化
        isCharacterSelected = true;
        confirmButton.interactable = true;

        // 選択されたキャラクターを CharacterSelectionManager に保存
        CharacterSelectionManager.Instance.selectedCharacterIndex = characterIndex;

        Debug.Log("キャラクター " + characterIndex + " が選択されました");
    }


    // 決定ボタンが押された時の処理
    void OnConfirmSelection()
    {
        if (isCharacterSelected)
        {
            // シーン遷移
            SceneManager.LoadScene(sceneName);
            Debug.Log("シーン遷移: " + sceneName);
        }
    }
}


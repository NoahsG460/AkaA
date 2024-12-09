using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    void Start()
    {
        // シーンがロードされたときに呼ばれるイベントを登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // イベント登録を解除（メモリリークを防ぐため）
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 指定したシーンに移動したらキャラクターを削除
        if (scene.name == "リザルト") // リザルトシーンに移動したとき
        {
            Destroy(gameObject); // キャラクターを削除
        }
    }
}

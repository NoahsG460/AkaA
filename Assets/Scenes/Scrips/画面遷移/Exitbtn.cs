using UnityEngine;

public class Exitbtn: MonoBehaviour
{
    // アプリケーションを終了するメソッド
    public void OnExitButtonPressed()
    {
        // エディターで動作確認用
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 実行環境でアプリケーションを終了
        Application.Quit();
#endif
    }
}

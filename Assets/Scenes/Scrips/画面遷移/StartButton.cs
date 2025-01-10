using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // シーン管理のために必要

public class StartButton : MonoBehaviour
{
    public Image buttonImage;   // ボタンのImageコンポーネント
    public float blinkSpeed = 1.0f; // 点滅速度

    private bool isIncreasing = true;
    private float alpha = 1.0f;

    void Update()
    {
        if (buttonImage != null)
        {
            // 点滅の透明度計算
            if (isIncreasing)
            {
                alpha += Time.deltaTime * blinkSpeed;
                if (alpha >= 1.0f)
                {
                    alpha = 1.0f;
                    isIncreasing = false;
                }
            }
            else
            {
                alpha -= Time.deltaTime * blinkSpeed;
                if (alpha <= 0.3f)
                {
                    alpha = 0.3f;
                    isIncreasing = true;
                }
            }

            // Imageの透明度を設定
            Color color = buttonImage.color;
            color.a = alpha;
            buttonImage.color = color;
        }
    }

    // ボタンを押したときに呼ばれる関数
    public void OnStartButtonClicked()
    {
        // 名前入力シーンに遷移
        SceneManager.LoadScene("キャラ選択画面");
    }
}

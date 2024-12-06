using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Image buttonImage;  // ボタンのImageコンポーネントを指定
    public float blinkSpeed = 1.0f;  // 点滅速度

    private bool isIncreasing = true;  // 透明度が増加中か減少中か
    private float alpha = 1.0f;        // 透明度

    void Update()
    {
        if (buttonImage != null)
        {
            // 点滅の透明度計算
            if (isIncreasing)
            {
                alpha += Time.deltaTime * blinkSpeed;
                if (alpha >= 1.0f) // 最大透明度に達したら減少に切り替え
                {
                    alpha = 1.0f;
                    isIncreasing = false;
                }
            }
            else
            {
                alpha -= Time.deltaTime * blinkSpeed;
                if (alpha <= 0.3f) // 最小透明度に達したら増加に切り替え
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
}

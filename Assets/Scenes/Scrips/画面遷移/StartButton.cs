using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Image buttonImage;  // �{�^����Image�R���|�[�l���g���w��
    public float blinkSpeed = 1.0f;  // �_�ő��x

    private bool isIncreasing = true;  // �����x������������������
    private float alpha = 1.0f;        // �����x

    void Update()
    {
        if (buttonImage != null)
        {
            // �_�ł̓����x�v�Z
            if (isIncreasing)
            {
                alpha += Time.deltaTime * blinkSpeed;
                if (alpha >= 1.0f) // �ő哧���x�ɒB�����猸���ɐ؂�ւ�
                {
                    alpha = 1.0f;
                    isIncreasing = false;
                }
            }
            else
            {
                alpha -= Time.deltaTime * blinkSpeed;
                if (alpha <= 0.3f) // �ŏ������x�ɒB�����瑝���ɐ؂�ւ�
                {
                    alpha = 0.3f;
                    isIncreasing = true;
                }
            }

            // Image�̓����x��ݒ�
            Color color = buttonImage.color;
            color.a = alpha;
            buttonImage.color = color;
        }
    }
}

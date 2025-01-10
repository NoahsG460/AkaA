using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // �V�[���Ǘ��̂��߂ɕK�v

public class StartButton : MonoBehaviour
{
    public Image buttonImage;   // �{�^����Image�R���|�[�l���g
    public float blinkSpeed = 1.0f; // �_�ő��x

    private bool isIncreasing = true;
    private float alpha = 1.0f;

    void Update()
    {
        if (buttonImage != null)
        {
            // �_�ł̓����x�v�Z
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

            // Image�̓����x��ݒ�
            Color color = buttonImage.color;
            color.a = alpha;
            buttonImage.color = color;
        }
    }

    // �{�^�����������Ƃ��ɌĂ΂��֐�
    public void OnStartButtonClicked()
    {
        // ���O���̓V�[���ɑJ��
        SceneManager.LoadScene("�L�����I�����");
    }
}

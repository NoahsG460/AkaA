using UnityEngine;

public class Exitbtn: MonoBehaviour
{
    // �A�v���P�[�V�������I�����郁�\�b�h
    public void OnExitButtonPressed()
    {
        // �G�f�B�^�[�œ���m�F�p
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ���s���ŃA�v���P�[�V�������I��
        Application.Quit();
#endif
    }
}

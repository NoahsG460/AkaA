using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    void Start()
    {
        // �V�[�������[�h���ꂽ�Ƃ��ɌĂ΂��C�x���g��o�^
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // �C�x���g�o�^�������i���������[�N��h�����߁j
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �w�肵���V�[���Ɉړ�������L�����N�^�[���폜
        if (scene.name == "���U���g") // ���U���g�V�[���Ɉړ������Ƃ�
        {
            Destroy(gameObject); // �L�����N�^�[���폜
        }
    }
}

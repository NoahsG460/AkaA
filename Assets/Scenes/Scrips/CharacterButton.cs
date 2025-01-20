using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterButton : MonoBehaviour
{
    public int characterIndex;  // �L�����N�^�[�I���C���f�b�N�X
    public string sceneName = "SampleScene";  // �J�ڐ�̃V�[����
    private bool isCharacterSelected = false;  // �L�����N�^�[�I���t���O

    public Button selectButton;  // �L�����N�^�[�I���{�^��
    public Button confirmButton;  // ����{�^��

    void Start()
    {
        // ����{�^���̓L�����N�^�[���I�΂��܂Ŗ�����
        confirmButton.interactable = false;

        selectButton.onClick.AddListener(OnCharacterSelected);  // �L�����N�^�[�I���{�^���̃��X�i�[��ݒ�
        confirmButton.onClick.AddListener(OnConfirmSelection);  // ����{�^���̃��X�i�[��ݒ�
    }

    // �L�����N�^�[�I�����ꂽ���̏���
    void OnCharacterSelected()
    {
        // �L�����N�^�[�I���t���O�𗧂ĂāA����{�^����L����
        isCharacterSelected = true;
        confirmButton.interactable = true;

        // �I�����ꂽ�L�����N�^�[�� CharacterSelectionManager �ɕۑ�
        CharacterSelectionManager.Instance.selectedCharacterIndex = characterIndex;

        Debug.Log("�L�����N�^�[ " + characterIndex + " ���I������܂���");
    }


    // ����{�^���������ꂽ���̏���
    void OnConfirmSelection()
    {
        if (isCharacterSelected)
        {
            // �V�[���J��
            SceneManager.LoadScene(sceneName);
            Debug.Log("�V�[���J��: " + sceneName);
        }
    }
}


using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    public static CharacterSelectionManager Instance { get; private set; }

    void Awake()
    {
        // �V���O���g���C���X�^���X�̏�����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[���؂�ւ��ł��C���X�^���X��ێ�
        }
        else
        {
            Destroy(gameObject); // ���łɃC���X�^���X������ꍇ�A���̃I�u�W�F�N�g��j��
        }
    }

    public int selectedCharacterIndex; // �I�����ꂽ�L�����N�^�[�̃C���f�b�N�X
}

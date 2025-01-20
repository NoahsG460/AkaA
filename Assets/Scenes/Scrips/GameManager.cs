using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform spawnPoint; // �L�����N�^�[�̃X�|�[���ʒu
    public GameObject[] characterPrefabs; // �L�����N�^�[�̃v���n�u�z��

    void Start()
    {
        // �I�����ꂽ�L�����N�^�[���擾
        int selectedIndex = CharacterSelectionManager.Instance.selectedCharacterIndex;

        if (selectedIndex >= 0 && selectedIndex < characterPrefabs.Length)
        {
            // �I�����ꂽ�L�����N�^�[���X�|�[��
            Instantiate(characterPrefabs[selectedIndex], spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("�I�����ꂽ�L�����N�^�[�̃C���f�b�N�X�������ł�: " + selectedIndex);
        }
    }
}

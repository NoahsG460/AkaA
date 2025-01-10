using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // �V���O���g���̃C���X�^���X

    public GameObject[] characterPrefabs; // �L�����N�^�[�v���n�u�̔z��
    public Transform spawnPoint;          // �L�����N�^�[�����ʒu
    private GameObject currentPlayer;     // ���ݑ��쒆�̃L�����N�^�[

    void Awake()
    {
        // �V���O���g���C���X�^���X�̐ݒ�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����Ŕj������Ȃ�
        }
        else
        {
            Destroy(gameObject); // ���ɃC���X�^���X������΍폜
        }
    }

    public void OnCharacterSelect(int characterIndex)
    {
        // �����̃L�����N�^�[���폜
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }

        // �V�����L�����N�^�[�𐶐�
        currentPlayer = Instantiate(characterPrefabs[characterIndex], spawnPoint.position, Quaternion.identity);

        // ����\�ɐݒ�
        PlayerStamina playerStamina = currentPlayer.GetComponent<PlayerStamina>();
        if (playerStamina != null)
        {
            playerStamina.isControlled = true; // �V�����L�����N�^�[�𑀍�\��
        }
        else
        {
            Debug.LogWarning("PlayerStamina���A�^�b�`����Ă��܂���");
        }
    }
}

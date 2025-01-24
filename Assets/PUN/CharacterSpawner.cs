using Photon.Pun;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private string[] characterPrefabNames; // �����̃v���n�u��
    [SerializeField] private Transform spawnPoint;         // �X�|�[���ʒu�i1�ŗǂ��j

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnCharacters();
        }
    }

    private void SpawnCharacters()
    {
        for (int i = 0; i < characterPrefabNames.Length; i++)
        {
            // ���ׂē����X�|�[���ʒu
            PhotonNetwork.Instantiate(
                characterPrefabNames[i],         // �v���n�u��
                spawnPoint.position,             // �����X�|�[���ʒu
                spawnPoint.rotation              // ������]
            );
        }
    }
}


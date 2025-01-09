using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // �v���C���[�𐶐�����
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        // �v���C���[Prefab�̖��O
        string prefabName = "PlayerPrefab";

        // ��������ʒu�Ɖ�]
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
        Quaternion spawnRotation = Quaternion.identity;

        // PhotonNetwork.Instantiate��Prefab�𐶐�
        PhotonNetwork.Instantiate(prefabName, spawnPosition, spawnRotation);
    }
}

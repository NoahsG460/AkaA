using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // プレイヤーを生成する
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        // プレイヤーPrefabの名前
        string prefabName = "PlayerPrefab";

        // 生成する位置と回転
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
        Quaternion spawnRotation = Quaternion.identity;

        // PhotonNetwork.InstantiateでPrefabを生成
        PhotonNetwork.Instantiate(prefabName, spawnPosition, spawnRotation);
    }
}

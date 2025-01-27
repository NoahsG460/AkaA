using Photon.Pun;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private string[] characterPrefabNames; // 複数のプレハブ名
    [SerializeField] private Transform spawnPoint;         // スポーン位置（1つで良い）

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
            // すべて同じスポーン位置
            PhotonNetwork.Instantiate(
                characterPrefabNames[i],         // プレハブ名
                spawnPoint.position,             // 同じスポーン位置
                spawnPoint.rotation              // 同じ回転
            );
        }
    }
}


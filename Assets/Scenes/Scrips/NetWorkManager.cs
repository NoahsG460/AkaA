using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // �T�[�o�[�ɐڑ�
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // ���[���ɐڑ��ł����玩���Ń��[�����쐬�܂��͎Q��
        PhotonNetwork.JoinOrCreateRoom("Room1", new Photon.Realtime.RoomOptions(), null); // TypedLobby.Default��null�ɕύX
    }

    public override void OnJoinedRoom()
    {
        // �v���C���[�𐶐�
        PhotonNetwork.Instantiate("PlayerPrefab", new Vector3(0, 0, 0), Quaternion.identity);
    }
}

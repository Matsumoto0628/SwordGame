using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class OnlineBattleManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    Vector3 position1, position2;

    GameObject player;

    private void Start()
    {
        Connect("1.0");
    }

    private void Connect(string gameVersion)
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    /*
     * Callbacks
     */
    // Photon�ɐڑ�
    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    // Photon����ؒf���ꂽ��
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
    }

    // �}�X�^�[�T�[�o�[�ɐڑ�������
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");

        PhotonNetwork.JoinRandomRoom();
    }

    // �����_���ȕ����ւ̓����Ɏ��s������
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");

        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    // �����ɓ���������
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        if (PhotonNetwork.IsMasterClient)
        {
            player = PhotonNetwork.Instantiate("MasterPlayer", position1, Quaternion.identity);
            Vector3 scale = player.transform.localScale;
            scale = new Vector3(0f - scale.x, scale.y, scale.z);
            player.transform.localScale = scale;
        }
        else
        {
            player = PhotonNetwork.Instantiate("ClientPlayer", position2, Quaternion.identity);
        }
    }
}

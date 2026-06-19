using UnityEngine;
using Photon.Pun;

public class NetworkLauncher : MonoBehaviourPunCallbacks{
    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster() {
        base.OnConnectedToMaster();
        //加入或创建房间
        PhotonNetwork.JoinOrCreateRoom("Room", new Photon.Realtime.RoomOptions() { MaxPlayers = 4 }, default);
    }
    public override void OnJoinedRoom() {
        base.OnJoinedRoom();

        //初始化管理器
        StateManager.instance.InitializeRoomState();
        //实例化玩家
        PhotonNetwork.Instantiate("Player", new Vector3(0, 1, 0), Quaternion.identity);
    }
}

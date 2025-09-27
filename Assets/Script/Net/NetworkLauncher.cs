using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkLauncher : MonoBehaviourPunCallbacks{
    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster() {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinOrCreateRoom("Room", new Photon.Realtime.RoomOptions() { MaxPlayers = 4 }, default);
    }
    public override void OnJoinedRoom() {
        base.OnJoinedRoom();

        PhotonNetwork.Instantiate("Player", new Vector3(0, 1, 0), Quaternion.identity, 0);
    }
}

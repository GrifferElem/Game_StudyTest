using UnityEngine;
using Photon.Pun;

public class StateManager : MonoBehaviourPunCallbacks
{
    public static StateManager instance;
    public ExitGames.Client.Photon.Hashtable interactState;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        interactState = new ExitGames.Client.Photon.Hashtable();
    }
    //初始化房间状态
    public void InitializeRoomState() {
        if (!PhotonNetwork.IsMasterClient) return;

        if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("InteractState")) {
            //初始化状态
            interactState = new ExitGames.Client.Photon.Hashtable();
            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "InteractState", interactState } });
        } else {
            interactState = (ExitGames.Client.Photon.Hashtable)PhotonNetwork.CurrentRoom.CustomProperties["InteractState"];
        }
    }
    //更改状态：物体申请交互
    public void RequestInteract(string ID) {
        if (!PhotonNetwork.IsMasterClient) return;

        Debug.Log("申请交互");
        //检测状态
        bool isFinish = interactState.ContainsKey(ID) && (bool)interactState[ID];
        if (!isFinish) {
            //更新状态
            interactState[ID] = true;
            //同步到房间
            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "InteractState", interactState } });
        }
    }
    //查询状态
    public bool GetInteractState(string ID) {
        return interactState.ContainsKey(ID) && (bool)interactState[ID];
    }
}

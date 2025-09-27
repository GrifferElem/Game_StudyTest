using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviourPunCallbacks
{
    private PhotonView view;
    public InputActionReference inputAction;

    public string ID;
    private bool isFinish = false;

    private void Start() {
        view = GetComponent<PhotonView>();

        if (string.IsNullOrEmpty(ID)) {
            ID = "CanInteract_" + gameObject.name;
        }
        //已交互就销毁
        if (StateManager.instance.GetInteractState(ID)) {
            isFinish = true;
            Destroy(gameObject);
        }
        //启用inputAction
        if (inputAction != null && inputAction.action != null) {
            inputAction.action.Enable();
        }
    }
    private void Update() {
        if (IsFindPlayer() && inputAction.action.WasPressedThisFrame()&&!isFinish) {
            if (isFinish) return;
            Debug.Log("检测交互情况");
            //请求交互
            StateManager.instance.RequestInteract(ID);
            Interacted();
        }
    }
    //检测玩家是否在交互物体附近
    private bool IsFindPlayer() {
        float Range = 2f;
        Collider[] player = Physics.OverlapSphere(transform.position, Range);
        foreach (Collider c in player) {
            if(c.CompareTag("Player")) return true;
        }
        return false;
    }
    //销毁
    private void Interacted() {
        if (PhotonNetwork.IsMasterClient) {
            Debug.Log("Destory IntectableObject");
            PhotonNetwork.Destroy(gameObject);
        } else {
            view.RPC("RPC_Destory", RpcTarget.MasterClient);
        }
    }
    [PunRPC]
    private void RPC_Destory() {
        isFinish = true;
        PhotonNetwork.Destroy(gameObject);
    }
}

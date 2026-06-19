using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviourPunCallbacks
{
    private PhotonView view;
    public InputActionReference inputAction;
    private Animator anim;
    public DropObject dropObj;

    public string ID;
    private bool isFinish = false;

    private void Start() {
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        dropObj = GetComponent<DropObject>();

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
            Debug.Log("交互");
            //请求交互
            StateManager.instance.RequestInteract(ID);
            StartCoroutine(OpenChest());
        }
    }
    //检测附近是否玩家
    private bool IsFindPlayer() {
        float Range = 2f;
        Collider[] player = Physics.OverlapSphere(transform.position, Range);
        foreach (Collider c in player) {
            if(c.CompareTag("Player")) return true;
        }
        return false;
    }
    //播放 宝箱开启动画
    private IEnumerator OpenChest() {
        view.RPC("RPC_ChestAnim",RpcTarget.All);

        //等待动画
        yield return new WaitForSeconds(1f);
        //掉落物
        dropObj.DropOccur(transform.position);
        Interacted();
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
    [PunRPC]
    private void RPC_ChestAnim() {
        anim.SetTrigger("open");
    }
}

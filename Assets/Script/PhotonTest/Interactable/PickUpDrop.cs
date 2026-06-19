using UnityEngine;

public class PickUpDrop : MonoBehaviour
{
    private Transform playerTrans;
    public float pickUpRange = 2f;
    public float pickUpSpeed = 3f;

    private void Start() {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) {
            playerTrans = playerObj.transform;
        }
    }
    private void FixedUpdate() {
        if (playerTrans == null) return;

        if (DistancePlayer() <= pickUpRange) {
            MoveToPlayer();
        }
    }
    //跑向玩家
    private void MoveToPlayer() {
        Vector3 dir = (playerTrans.position - transform.position).normalized;
        transform.position += dir * pickUpSpeed * Time.fixedDeltaTime;
        if (DistancePlayer() < 0.5f) {
            OnDestory();
        }
    }
    //与玩家距离
    private float DistancePlayer() {
        float distance = Vector3.Distance(transform.position, playerTrans.position);
        return distance;
    }
    //销毁
    private void OnDestory() {
        Destroy(gameObject);
    }
}

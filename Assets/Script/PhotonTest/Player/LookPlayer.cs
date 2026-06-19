using UnityEngine;
using Cinemachine;

public class LookPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        if (player != null && virtualCamera != null) {
            virtualCamera.Follow = player.transform;
            virtualCamera.LookAt = player.transform;

            //÷ÿ÷√ ”Ω«
            virtualCamera.transform.position = player.transform.position + new Vector3(0, 2, -5);
            virtualCamera.transform.LookAt(player.transform);
        }
    }
}

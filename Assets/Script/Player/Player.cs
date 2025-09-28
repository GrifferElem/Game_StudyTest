using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPun
{
    public PlayInput playInput;
    private Vector2 inputDirection;
    private Rigidbody rb;

    private float moveSpeed = 5f;
    private float rotateSpeed = 10f;

    private void Awake() {
        playInput = new PlayInput();
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable() {
        playInput.Enable();
    }
    private void OnDisable() {
        playInput.Disable();
    }
    private void FixedUpdate() {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;

        inputDirection = playInput.GamePlay.Move.ReadValue<Vector2>().normalized;
        Move(inputDirection);
    }
    private void Move(Vector2 dir) {
        if (dir.magnitude > 0.1f) {
            Vector3 moveDir = new Vector3(dir.x, 0, dir.y);
            rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.fixedDeltaTime);
        } else {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

}

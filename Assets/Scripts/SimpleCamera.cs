using Photon.Pun;
using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    public float moveSpeed = 5f;     // Speed of movement
    public float lookSensitivity = 2f;

    private float pitch = 0f;  // Rotation around X axis
    private float yaw = 0f;    // Rotation around Y axis
    PhotonView view;


    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (view.IsMine)
        {
            float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow
            float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down arrow


            Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;


            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }


    }
}
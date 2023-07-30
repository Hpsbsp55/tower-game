using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    private float lookSpeed = 2f;
    [SerializeField] float VRotation;

    [SerializeField] Rigidbody rb;
    private float moveSpeed = 2f;
    [SerializeField] LayerMask ground;
    private float jumpForce = 1000f;
    [SerializeField] Transform groundCheck;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //confines cursor to window
        Cursor.visible = false; //makes cursor invisible
        //rb = player.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float lookX = Input.GetAxis("Mouse X") * lookSpeed;
        float lookY = Input.GetAxis("Mouse Y") * lookSpeed;
        VRotation -= lookY;
        VRotation = Mathf.Clamp(VRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * VRotation;
        player.transform.Rotate(Vector3.up * lookX);
    }
    void FixedUpdate() {
        rb.velocity = Vector3.Normalize(transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")) * moveSpeed;
        Debug.Log(Physics.CheckSphere(groundCheck.position, 0.6f, ground));
        if(Input.GetKeyDown("space") && Physics.CheckSphere(groundCheck.position, 0.6f, ground)) {
            rb.AddForce(transform.up * jumpForce);
        }
    }
}

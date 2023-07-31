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
    private float jumpForce = 175f;
    [SerializeField] Transform groundCheck;
    private bool isJumping = false;
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
        if(Input.GetKeyDown("space") && Physics.CheckSphere(player.position, player.localScale.y + 0.1f, ground)) {
            isJumping = true;
            Debug.Log("Jumping");
        }
    }
    void FixedUpdate() {
        Vector3 movementVelocity = Vector3.Normalize(transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")) * moveSpeed;
        rb.velocity = new Vector3(movementVelocity.x, rb.velocity.y, movementVelocity.z);
        //Debug.Log(Physics.CheckSphere(player.position, player.localScale.y + 0.1f, ground));
        if(isJumping) {
            rb.AddForce(transform.up * jumpForce);
            isJumping = !isJumping;
        }
    }
}

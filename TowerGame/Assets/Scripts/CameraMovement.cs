using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    private float lookSpeed = 2f;
    [SerializeField] float VRotation;

    [SerializeField] Rigidbody rb;
    private float moveSpeed = 4f;
    [SerializeField] LayerMask ground;
    private float jumpForce = 450f;
    [SerializeField] Transform groundCheck;
    private bool isJumping = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //confines cursor to window
        Cursor.visible = false; //makes cursor invisible
    }

    void Update()
    {
        CheckInput();
    }
    void FixedUpdate() {
        Move();
    }
    void CheckInput() {
        float lookX = Input.GetAxisRaw("Mouse X") * lookSpeed; //get mouse movement horizontal
        float lookY = Input.GetAxisRaw("Mouse Y") * lookSpeed; //get mouse movement vertical
        VRotation -= lookY;
        VRotation = Mathf.Clamp(VRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * VRotation;
        player.transform.Rotate(Vector3.up * lookX);
        if(Input.GetKeyDown("space") && Physics.CheckSphere(player.position, player.localScale.y + 0.1f, ground)) {
            isJumping = true;
        }
    }
    void Move() {
        Vector3 movementVelocity = Vector3.Normalize(player.transform.forward * Input.GetAxisRaw("Vertical") + player.transform.right * Input.GetAxisRaw("Horizontal")) * moveSpeed;
        rb.velocity = new Vector3(movementVelocity.x, rb.velocity.y, movementVelocity.z);
        if(isJumping) {
            rb.AddForce(Vector3.up * jumpForce);
            isJumping = !isJumping;
        }
    }
}

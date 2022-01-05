using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;
    [SerializeField] float mouseSensitivity, runSpeed, walkSpeed, jumpForce, smoothTime;
    Rigidbody rb;
    float verticalLookRoatation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    PhotonView PV;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if(!PV.IsMine)
        {
           Destroy(GetComponentInChildren<Camera>().gameObject);
           Destroy(rb);//fix janky
        }
    }


    void Update()
    {
        if (!PV.IsMine)
            return;
        Look();
        Move();
        Jump();

    }
    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);//if left key press run

    }
    void Look()//camera follow mouse
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
        verticalLookRoatation += Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalLookRoatation = Mathf.Clamp(verticalLookRoatation, -90f, 90f);
        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRoatation;
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    public void SetGroundedState(bool onGround)
    {
        grounded = onGround;
    }
    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}

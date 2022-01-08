using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static int score;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _mouseSensitivity = 7f;
    [SerializeField] private float _minCameraView = -70f, _maxCameraView = 80f;
    PhotonView PV;
    [SerializeField] TMP_Text  txtScore;
    [SerializeField] TMP_Text  txtOver;
    [SerializeField] Button btnMenu;
    Rigidbody rb;
    private CharacterController _charController;
    private Camera _camera;
    private float xRotation = 0f;
    private Vector3 _playerVelocity;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        score = 0;
        _charController = GetComponent<CharacterController>();
        _camera = Camera.main;

        if (_charController == null) Debug.Log("No character attached to player");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (!PV.IsMine)
        {
            GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1, 0.5f);
            Destroy(rb);
            Destroy(GetComponentInChildren<TextMeshProUGUI>());
        }
        txtScore.text = "Score: " + score;
    }




    void Update()
    {
       if (!PV.IsMine)
           return;
        PlayerMovement();
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, _minCameraView, _maxCameraView);
        _camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
        if (Input.GetMouseButtonDown(0)) {
            shoot();
        }
        
    }

    private void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * vertical + transform.right * horizontal  ;
        
        if (_charController.isGrounded) {
            if (Input.GetButton("Jump"))
                movement.y = jumpSpeed;
        }
        movement.y -= 9.18f * Time.deltaTime;
        _charController.Move(movement * Time.deltaTime * _speed);
    }

    private void FixedUpdate()
    {
        if (transform.position.y < 1.08f) {
            Debug.Log("00000");
            transform.position = Vector3.right * transform.position.x + Vector3.up * 1.08f + Vector3.forward * transform.position.z;
            _playerVelocity.y = 0f;
        }
        else {
            _playerVelocity.y += -9.18f * Time.deltaTime;
            _charController.Move(_playerVelocity * Time.deltaTime);
        }
    }
    private void shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit)) {
            if (hit.transform.tag == "Enemy") {
                score++;
                Debug.Log(score);
                txtScore.text = "Score: " + score;
            }
        }
        if(score == 5)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            txtOver.gameObject.SetActive(true);
            btnMenu.gameObject.SetActive(true);
            Time.timeScale = 0f;
            //StartCoroutine(wait());
            //PhotonNetwork.LoadLevel(0);
        }
    }
    /*
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
    }*/
}



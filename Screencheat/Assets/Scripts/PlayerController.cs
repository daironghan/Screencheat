using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static int score;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _jumpSpeed = 3.5f;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _mouseSensitivity = 7f;
    [SerializeField] private float _minCameraView = -70f, _maxCameraView = 80f;
    PhotonView PV;
    [SerializeField] TMP_Text  txtScore;
    [SerializeField] TMP_Text  txtOver;
    [SerializeField] Button btnMenu;
    Rigidbody rb;
    MeshRenderer render;
    private CharacterController _charController;
    private Camera _camera;
    private float timeStamp;
    private float xRotation = 0f;
    private float _directionY;
    private Vector3 _playerVelocity;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        render = gameObject.GetComponentInChildren<MeshRenderer>();
    }
    void Start()
    {
        timeStamp = Time.time;
        score = 0;
        _charController = GetComponent<CharacterController>();
        _camera = Camera.main;
        render.enabled = false;

        if (_charController == null) Debug.Log("No character attached to player");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (!PV.IsMine)
        {
            Destroy(transform.GetChild(0).transform.GetChild(1).gameObject);
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
        if (Input.GetMouseButtonDown(0) && Time.time > timeStamp) {
            timeStamp = Time.time + 1f;
            shoot();
        }
        
    }

    private void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * vertical + transform.right * horizontal;

        if (_charController.isGrounded) {
            if (Input.GetButtonDown("Jump")) 
            {
                Debug.Log("jump");
                _directionY = _jumpSpeed;
            }
                
        }
        _directionY -= _gravity * Time.deltaTime;
        movement.y = _directionY;
        
        _charController.Move(movement * Time.deltaTime * _speed);
    }

    private void FixedUpdate()
    {
        if (transform.position.y < 1.08f) {
            transform.position = Vector3.right * transform.position.x + Vector3.up * 1.08f + Vector3.forward * transform.position.z;
            _playerVelocity.y = 0f;
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
        if(score == 3)
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



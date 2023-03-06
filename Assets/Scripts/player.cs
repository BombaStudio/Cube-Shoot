using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

public class player : MonoBehaviour
{
#if ENABLE_INPUT_SYSTEM
    InputAction movement;
    InputAction jump;
#endif

    Rigidbody rb;
    public Camera cam,cam2;

    [Range(1,10)]
    public float speed = 1;

    [Range(1, 10)]
    public float Mspeed = 1;

    [Range(1, 10)]
    public int dashSpeed = 1;
    int dashSave;

    [Range(0.1f, 1)]
    public float dashTime = 0.25f;

    bool dash;

    float mx;
    float my;
    public float rx;
    public float ry;

    float camSpeed = 3;

    [SerializeField] bool isGrounded;

    int sin = 0;

    string[] portalName = new string[] {
        "portal1",
        "portal2"
    };

    public GameObject[] portalPos;

    public float gravity = -10f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float xRotation;

    Vector3 velocity;

    

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Screen.lockCursor = true;
    }

    void Start()
    {
        /*
#if ENABLE_INPUT_SYSTEM
        movement = new InputAction("PlayerMovement", binding: "<Gamepad>/leftStick");
        movement.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/s")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/a")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/d")
            .With("Right", "<Keyboard>/rightArrow");

        jump = new InputAction("PlayerJump", binding: "<Gamepad>/a");
        jump.AddBinding("<Keyboard>/space");

        movement.Enable();
        jump.Enable();
#endif
        */
        rb = GetComponent<Rigidbody>();
        //cam = Camera.main;
        dash = false;
        dashSave = dashSpeed;
        StartCoroutine("dashSystem");
    }

    private void Update()
    {
        rx = Input.GetAxis("Mouse X") * Mspeed;
        ry = Input.GetAxis("Mouse Y") * Mspeed;

        Vector3 rrot = Vector3.up * rx;
        transform.Rotate(rrot * 10 * camSpeed * 4 * Time.fixedDeltaTime);


        xRotation += -ry * 10 * camSpeed * 4 * Time.fixedDeltaTime;
        xRotation = Mathf.Clamp(xRotation, -60f, 10f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        cam2.transform.localRotation = Quaternion.Euler(xRotation / 2, Input.GetMouseButton(0) && GetComponent<PlayerAct>().hasGun ? -15 : 0, Input.GetMouseButton(0) && GetComponent<PlayerAct>().hasGun ? 15 : 0);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, Mathf.Sqrt(jumpHeight * -2f * gravity), 0);
        }
    }

    private void FixedUpdate()
    {
        mx = Input.GetAxis("Horizontal") * speed;
        my = Input.GetAxis("Vertical") * speed;


        
        //bool jumpPressed = false;
        /*
#if ENABLE_INPUT_SYSTEM
        var delta = movement.ReadValue<Vector2>();
        mx = delta.x;
        my = delta.y;
        jumpPressed = Mathf.Approximately(jump.ReadValue<float>(), 1);
#else
        mx = Input.GetAxis("Horizontal") * speed;
        my = Input.GetAxis("Vertical") * speed;
        
        jumpPressed = Input.GetButtonDown("Jump");
#endif
#if ENABLE_INPUT_SYSTEM
        float rx = 0, ry = 0;

        if (Mouse.current != null)
        {
            var delta = Mouse.current.delta.ReadValue() / 15.0f;
            rx += delta.x;
            ry += delta.y;
        }
        if (Gamepad.current != null)
        {
            var value = Gamepad.current.rightStick.ReadValue() * 2;
            rx += value.x;
            ry += value.y;
        }

        rx *= Mspeed;
        ry *= Mspeed;
#else
        rx = Input.GetAxis("Mouse X") * Mspeed;
        ry = Input.GetAxis("Mouse Y") * Mspeed;
#endif
        */
        


        

        Vector3 direction = Vector3.forward * my + Vector3.right * mx;
        transform.Translate(direction * -speed * 0.5f * Time.fixedDeltaTime);
        
    }
    IEnumerator dashSystem()
    {
        yield return new WaitForSeconds(dashTime);
        if (dash)
        {
            if (dashSpeed > 0)dashSpeed--;
            if (dashSpeed <= 0)
            {
                dashSpeed = dashSave;
                dash = false;
            }
        }
        sin++;
        StartCoroutine("dashSystem");
    }
    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < portalName.Length; i++)
        {
            if (collision.transform.name == portalName[i]) transform.position = portalPos[i].transform.position;
        }
    }
}

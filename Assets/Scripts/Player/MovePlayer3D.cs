using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer3D : MonoBehaviour
{
    //Data
    [SerializeField] private PlayerData3D playerData3D;
    
    private float axisHorizontal;
    private float axisVertical;
    private float mouseHorizontal;
    private float mouseVertical;
    private float currentSpeed;
    private bool isRunning = false;
    private bool isOnGround = true;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = playerData3D._walkSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(Vector3.up * playerData3D._jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            Run();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            StopRunning();
        }

    }

    void FixedUpdate()
    {
        axisHorizontal = Input.GetAxis("Horizontal");
        axisVertical = Input.GetAxis("Vertical");
        mouseHorizontal = Input.GetAxis("Mouse X");
        mouseVertical = Input.GetAxis("Mouse Y");

        //Vector3 cameraRotation = new Vector3(mouseVertical, mouseHorizontal, 0f);
        //Camera.main.transform.Rotate(cameraRotation * playerData3D._mouseSensibility * Time.deltaTime);

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        Vector3 desiredMoveDirection = forward * axisVertical;
        rb.transform.Translate(desiredMoveDirection * currentSpeed * Time.deltaTime, Space.World);
        rb.transform.Translate(right * axisHorizontal * currentSpeed * Time.deltaTime, Space.World);

        if (desiredMoveDirection != Vector3.zero)
        {
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, Quaternion.LookRotation(desiredMoveDirection), playerData3D._rotateSpeed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            if (isRunning)
                Run();
            else
                StopRunning();
        }
    }
    private void Run()
    {
        if (isOnGround)
        {
            currentSpeed = playerData3D._runSpeed;
        }
    }

    private void StopRunning()
    {
        if (isOnGround)
        {
            currentSpeed = playerData3D._walkSpeed;
        }
    }
}

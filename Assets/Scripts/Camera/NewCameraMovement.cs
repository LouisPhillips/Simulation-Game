using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class NewCameraMovement : MonoBehaviour
{
    private Vector3 inputDirection;
    private Vector3 moveDirection;
    private UISwitch ui;
    private float rotateDirection = 0f;
    private Vector3 zoomDirection;
    private Vector3 followOffset;

    public CinemachineVirtualCamera cam;
    public float moveSpeed = 20f;
    public float rotateSpeed = 100f;
    public float zoomSpeed = 20f;
    public float zoomMin = 1f;
    public float zoomMax = 10f;

    private void Awake()
    {
        ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UISwitch>();
        followOffset = cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    private void Update()
    {
        Movement();
        Rotation();
        Zoom();
        BoundCheck();
    }

    private void Movement()
    {
        inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.parent = null;
        }
    }

    private void Rotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            rotateDirection = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotateDirection = -1f;
        }
        if (!Input.GetKey(KeyCode.Q) && (!Input.GetKey(KeyCode.E)))
        {
            rotateDirection = 0f;
        }

        transform.eulerAngles += new Vector3(0, rotateDirection * rotateSpeed * Time.deltaTime, 0);
    }

    private void Zoom()
    {
        if (ui.state == UISwitch.State.normal)
        {
            zoomDirection = followOffset.normalized;
            if (Input.mouseScrollDelta.y > 0)
            {
                followOffset -= zoomDirection * 3f;
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                followOffset += zoomDirection * 3f;
            }

            if (followOffset.magnitude < zoomMin)
            {
                followOffset = zoomDirection * zoomMin;
            }

            if ( followOffset.magnitude > zoomMax)
            {
                followOffset = zoomDirection * zoomMax;
            }
            
            cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, zoomSpeed * Time.deltaTime);


        }
    }

    private void BoundCheck()
    {
        if (transform.position.z > 14.2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 14.2f);
        }
        if (transform.position.z < -15.82)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -15.82f);
        }
        if (transform.position.x > 20.03)
        {
            transform.position = new Vector3(20.03f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -9.94)
        {
            transform.position = new Vector3(-9.94f, transform.position.y, transform.position.z);
        }
    }
}

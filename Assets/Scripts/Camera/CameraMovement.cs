using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float distance = 20.0f;
    public float maxDistance = 2f;
    public float zoomSpd = 2.0f;

    public float xSpeed = 240.0f;
    public float ySpeed = 123.0f;

    public int yMinLimit = -723;
    public int yMaxLimit = 877;

    private float x = 0f;
    private float y = 45f;

    public float targetMovementSpeed = 10f;

    private UISwitch ui;

    private bool rotating = false;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UISwitch>();
    }

    public void LateUpdate()
    {

        if (target)
        {
            float rotateDirection = 0f;
            if (Input.GetKey(KeyCode.Q))
            {
                rotating = true;
                rotateDirection = +1f;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                rotating = true;
                rotateDirection = -1f;
            }
            if (!Input.GetKey(KeyCode.Q) && (!Input.GetKey(KeyCode.E)))
            {
                rotating = false;
            }

            float rotateSpeed = 100f;



            transform.RotateAround(target.transform.position, target.transform.up, rotateDirection * rotateSpeed * Time.deltaTime);
            transform.LookAt(target.position);




            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 camMove = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles) * movement;
            target.transform.position += camMove * targetMovementSpeed * Time.deltaTime;
            target.transform.position = new Vector3(target.transform.position.x, -3.794175f, target.transform.position.z);
            //target.transform.position += movement * targetMovementSpeed * Time.deltaTime;

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                target.transform.parent = null;
            }

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            if (ui.state == UISwitch.State.normal)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    distance -= zoomSpd * 0.02f;
                }

                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    distance += zoomSpd * 0.02f;
                }
            }

            Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            // transform.rotation = rotation;
            if (!rotating)
            {
                transform.position = position;
            }


            target.rotation = transform.rotation;

            if (distance < maxDistance)
            {
                distance = maxDistance;
            }
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360.0f)
            angle += 360.0f;
        if (angle > 360.0f)
            angle -= 360.0f;
        return Mathf.Clamp(angle, min, max);
    }
}

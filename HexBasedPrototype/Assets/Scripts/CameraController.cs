using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //movement variables
    private float hor;
    private float vert;
    private float rot;
    
    //Speed variables
    public float rotationSpeed;
    public float mouseMoveSpeed;

    void Update()
    {
        hor = 0;
        vert = 0;
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            MovementCameraManualInput();
        }
        else
        {
            MoveCameraEdgeDetection();
        }
        RotateCamera();
        
    }
    
    //Move mouse to edge controlls
    public void MoveCameraEdgeDetection()
    {
        if (Input.mousePosition.x >= Screen.width)
        {
            
            hor += Time.deltaTime * mouseMoveSpeed;
        }
        else if (Input.mousePosition.x <= 0)
        {
            hor += Time.deltaTime * -mouseMoveSpeed;
        }
        if(Input.mousePosition.y <= 0)
        {
            vert += Time.deltaTime * -mouseMoveSpeed;
        }
        else if(Input.mousePosition.y >= Screen.height)
        {
            vert += Time.deltaTime * mouseMoveSpeed;
        }

        Vector3 move = new Vector3(hor,vert,0);
        Camera.main.transform.Translate(move,Space.Self);
    }

    // WASD input controll
    public void MovementCameraManualInput()
    {
        hor += Input.GetAxis("Horizontal") * Time.deltaTime * mouseMoveSpeed;
        vert += Input.GetAxis("Vertical") * Time.deltaTime * mouseMoveSpeed;
        Vector3 move = new Vector3(hor,vert,0);
        Camera.main.transform.Translate(move,Space.Self);
    }

    //Camera Rotation using the middle mouse button
    public void RotateCamera()
    {
        if (Input.GetButton("Fire3"))
        {
            rot += Input.GetAxis("Mouse X");
            Camera.main.transform.Rotate(0,rot * Time.deltaTime * rotationSpeed / 2,0,Space.World);
        }
        else if (Input.GetButtonUp("Fire3"))
        {
            rot = 0;
        }
        else if (Input.GetButton("Q"))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);
        }

        else if (Input.GetButton("E"))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        }
    }
}

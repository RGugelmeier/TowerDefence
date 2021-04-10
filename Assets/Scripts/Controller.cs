using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //Variables
    RaycastHit2D rayHit;
    LayerMask unplaceableLayer;
    new Camera camera;
    float xAxisValue;
    float yAxisValue;

    void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        //**--CAMERA MOVEMENT--**//
        //If w, a, s, or d are being pressed. Move camera. Arrow keys also work.
        {
            if (Input.GetAxis("Horizontal") > 0.0f || Input.GetAxis("Vertical") > 0.0f ||
                Input.GetAxis("Horizontal") < 0.0f || Input.GetAxis("Vertical") < 0.0f)
            {
                CameraMovement();
            }
        }
        //**--END CAMERA MOVEMENT--**//
    }

    //FUNCTION//
    //CAMERAMOVEMENT: Called when Horizontal or Vertical axis > 0.0.
    //Translates the camera's position.
    void CameraMovement()
    {
        //Camera movement axis.
        xAxisValue = Input.GetAxis("Horizontal") / 10;
        yAxisValue = Input.GetAxis("Vertical") / 10;
        //Move camera on input.
        camera.transform.Translate(xAxisValue, 0.0f, 0.0f);
        camera.transform.Translate(0.0f, yAxisValue, 0.0f);
    }
}

using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MovimentacaoDaCamera : MonoBehaviour {

    float velocidade = 200;
    float perspectiveZoomSpeed = 1f;        // The rate of change of the field of view in perspective mode.
    float orthoZoomSpeed = 1f;        // The rate of change of the orthographic size in orthographic mode.
    public GameObject Joystick3Person;

    private float x, y; //x e y do joysick visao de cima

    void FixedUpdate() //move a camera
    {

        x = Joystick3Person.GetComponent<SimpleTouchController>().movementVector.x; //pega x do joystick
        y = Joystick3Person.GetComponent<SimpleTouchController>().movementVector.y; //pega y do joystick
        movimentacaoCamera(x, y); //move a camera

        #region zoom camera
        // If there are two touches on the device...

        if (Input.touchCount == 2) {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Camera camera = GetComponent<Camera>(); //tem problema declarar coisa dentro do UPDATE?
                                                    // If the camera is orthographic...
            if (camera.orthographic) {
                Debug.Log("Camera ortografica");
                // ... change the orthographic size based on the change in distance between the touches.
                camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
            } else {
                // Otherwise change the field of view based on the change in distance between the touches.
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 20f, 60f);
            }
        }
        #endregion
    }

    void movimentacaoCamera(float horizontal, float vertical) {
        if (horizontal > 0) {
            //if our current X is > than the minimum range AND less than maximum range
            if (transform.position.x < 320)  //300
            {
                transform.Translate(Vector3.right * velocidade * Time.deltaTime, Space.World);
            }
        } else if (horizontal < 0) {
            //if our current X is > than the minimum range AND less than maximum range
            if (transform.position.x > 180)  //160
            {
                transform.Translate(Vector3.left * velocidade * Time.deltaTime, Space.World);
            }
        }
        if (vertical > 0) {
            //if our current X is > than the minimum range AND less than maximum range
            if (transform.position.z < 390) {
                transform.Translate(Vector3.forward * velocidade * Time.deltaTime, Space.World);
            }
        }
        if (vertical < 0) {
            //if our current X is > than the minimum range AND less than maximum range
            if (transform.position.z > 110) {
                transform.Translate(Vector3.back * velocidade * Time.deltaTime, Space.World);
            }
        }
    }
}

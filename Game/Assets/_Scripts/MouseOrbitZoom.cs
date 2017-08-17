//
//Filename: maxCamera.cs
//
// original: http://www.unifycommunity.com/wiki/index.php?title=MouseOrbitZoom
//
// --01-18-2010 - create temporary target, if none supplied at start

using UnityEngine;
using System.Collections;


[AddComponentMenu("Camera-Control/3dsMax Camera Style")]
public class MouseOrbitZoom : MonoBehaviour
{
    public Transform target;
    //private Quaternion rotation;
    //private Vector3 position;

    //public Vector3 targetOffset;

    public float distance = 450.0f;

    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;

    /*private float xDeg = 0.0f;
    private float yDeg = 0.0f;*/

    public float maxDistance = 20;
    public float minDistance = .6f;

    public int zoomRate = 40;

    float x = 0.0f;
    float y = 0.0f;

    bool lookSun;
    Vector3 savedPosition;
    Quaternion savedRotation;
    float savedSize;
    Transform savedTarget;


    void Start() { Init(); }
    void OnEnable() { Init(); }

    public void Init()
    {
        lookSun = true;
        distance = Vector3.Distance(transform.position, target.position);

        //be sure to grab the current rotations as starting points.
        //position = transform.position;
        //rotation = transform.rotation;

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        /*xDeg = Vector3.Angle(Vector3.right, transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);*/
    }

    /*
     * Camera logic on LateUpdate to only update after all character movement logic has been handled. 
     */
    void LateUpdate()
    {
        if (target)
        {
            if (Input.GetMouseButton(0))
            {
                x += (float)(Input.GetAxis("Mouse X") * xSpeed * 0.02);
                y += (float)(Input.GetAxis("Mouse Y") * xSpeed * 0.02);


                y = ClampAngle(y, -90, 90);

                Quaternion rotation = Quaternion.Euler(y, x, 0);
                Vector3 position = rotation * new Vector3(0.0f, 0.0f, -this.distance) + target.position;

                transform.rotation = rotation;
                transform.position = position;
            }

        }

        float distance = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * Mathf.Pow(zoomRate, 2) * 0.5f;
        gameObject.GetComponent<Camera>().orthographicSize -= distance;
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    public void SetTarget(Transform target)
    {
        if (lookSun)
        {
            savedTarget = this.target;
            this.target = target;
            lookSun = false;



            savedPosition = transform.position;
            savedRotation = transform.rotation;
            savedSize = gameObject.GetComponent<Camera>().orthographicSize;

            transform.position = new Vector3(target.transform.position.x, 0, -750);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            gameObject.GetComponent<Camera>().orthographicSize = target.transform.lossyScale.x;
        }
    }

    public void ReturnToSun()
    {
        if (!lookSun)
        {
            lookSun = true;

            this.target = savedTarget;
            transform.position = savedPosition;
            transform.rotation = savedRotation;
            gameObject.GetComponent<Camera>().orthographicSize = savedSize;
        }
    }
}
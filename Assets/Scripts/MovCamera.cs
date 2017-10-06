using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCamera : MonoBehaviour
{
    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;

    public float verticalSpeed = 2.0F;

    Camera myCam;
    
    public float fielOfViewMax = 70;
    public float fielOfViewNormal = 60;
    public float fielOfViewMin = 50;

    private void Awake()
    {
        myCam = this.gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        float translationV = Input.GetAxis("Vertical") * speed;
        float translationH = Input.GetAxis("Horizontal") * speed;

        translationV *= Time.deltaTime;
        translationH *= Time.deltaTime;

        float v = verticalSpeed * Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(2))
        {
            myCam.fieldOfView = fielOfViewMax;
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                myCam.fieldOfView = fielOfViewMin;
            }
            else
            {
                myCam.fieldOfView = fielOfViewNormal;
            }
        }

        transform.Rotate(v, 0, 0, 0);
        transform.Translate(translationH, 0, translationV);
    }
}
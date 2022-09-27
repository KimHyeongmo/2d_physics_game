using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_control : MonoBehaviour
{

    Transform camera_control;

    // Start is called before the first frame update
    void Awake()
    {
        camera_control = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            for(int i = 0; i<50; i++)
            {
                camera_control.position += new Vector3(0,0,-0.1f);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < 50; i++)
            {
                camera_control.position += new Vector3(0, 0, 0.1f);
            }
        }
    }
}

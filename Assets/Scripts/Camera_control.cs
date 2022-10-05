using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_control : MonoBehaviour
{
    public GameObject player;

    Transform camera_control;

    // Start is called before the first frame update
    void Awake()
    {
        camera_control = GetComponent<Transform>();
    }

    void Start()
    {
        //camera_control = player.transform;
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

    void LateUpdate()
    {
        //카메라 따라 움직이기
        Vector3 move_camera = new Vector3(player.transform.position.x, player.transform.position.y, camera_control.position.z);
        camera_control.position = move_camera;
    }
}

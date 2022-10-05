using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    RaycastHit2D hit2D;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition);
            Vector3 position_raw = Input.mousePosition;
            position_raw.z = 0 - Camera.main.transform.position.z;
            Vector3 position = Camera.main.ScreenToWorldPoint(position_raw); //"MainCamera 태그 붙어있는 놈을 찾아냄

            Debug.Log(position);

            //해당 좌표 오브젝트 찾기
            hit2D = Physics2D.Raycast(position, Vector2.zero, 0f);
        }

        control_physics();

    }
    
    void control_physics()
    {
        //중력 변환
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (hit2D.transform.tag == "item")
            {
                hit2D.rigidbody.gravityScale *= -1;
            }
            else if (hit2D.transform.tag == "Player")
            {
                hit2D.rigidbody.gravityScale *= -1;
                if (hit2D.rigidbody.gravityScale < 0)
                {
                    hit2D.transform.gameObject.GetComponent<Player_move>().spriteRenderer.flipY = true;
                }
                else
                    hit2D.transform.gameObject.GetComponent<Player_move>().spriteRenderer.flipY = false;
            }
            else
                Debug.Log("cant");
        }

        if (Input.GetKeyDown(KeyCode.E)) // 위로 세게
        {
            if (hit2D.transform.tag == "item")
            {
                hit2D.rigidbody.gravityScale -= 2;
            }
            else if (hit2D.transform.tag == "Player")
            {
                hit2D.rigidbody.gravityScale -= 2;
                if (hit2D.rigidbody.gravityScale < 0)
                {
                    hit2D.transform.gameObject.GetComponent<Player_move>().spriteRenderer.flipY = true;
                }
            }
            else
                Debug.Log("cant");
        }

        if (Input.GetKeyDown(KeyCode.R)) // 아래로 세게
        {
            if (hit2D.transform.tag == "item")
            {
                hit2D.rigidbody.gravityScale += 2;
            }
            else if (hit2D.transform.tag == "Player")
            {
                hit2D.rigidbody.gravityScale += 2;
                if (hit2D.rigidbody.gravityScale > 0)
                {
                    hit2D.transform.gameObject.GetComponent<Player_move>().spriteRenderer.flipY = false;
                }
            }
            else
                Debug.Log("cant");
        }

        //충돌 크기 조절(플랫폼)
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (hit2D.transform.tag == "platform")
            {
                hit2D.transform.gameObject.GetComponent<Platform>().against_scale /= 1.125f;
            }
            else
                Debug.Log("cant");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (hit2D.transform.tag == "platform")
            {
                hit2D.transform.gameObject.GetComponent<Platform>().against_scale *= 1.125f;
            }
            else
                Debug.Log("cant");
        }

        //마찰(플랫폼)
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (hit2D.transform.tag == "platform" && hit2D.collider.gameObject.GetComponent<Platform>().friction_scale > 0)
            {
                hit2D.collider.gameObject.GetComponent<Platform>().friction_scale -= 0.03125f;
            }
            else
                Debug.Log("cant");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hit2D.transform.tag == "platform")
            {
                hit2D.collider.gameObject.GetComponent<Platform>().friction_scale += 0.03125f;
            }
            else
                Debug.Log("cant");
        }
    }
}

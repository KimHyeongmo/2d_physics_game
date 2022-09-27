using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    Rigidbody2D rigid;

    Collider2D[] collider2d;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //줍기
        if (Input.GetKeyDown(KeyCode.Z))
        {
            float short_distance = 2.0f;
            int index_check = 0;
            if (collider2d.Length > 0)
            {
                for (int i = 0; i < collider2d.Length; i++)
                {
                    float distance_two_object = Vector2.Distance(rigid.position, collider2d[i].transform.position);
                    if (distance_two_object < short_distance)
                    {
                        short_distance = distance_two_object;
                        index_check = i;
                    }
                }

                Debug.Log(collider2d[index_check].name);

            }
        }
    }

    void FixedUpdate()
    {
        //주위 아이템 감지
        collider2d = Physics2D.OverlapCircleAll(rigid.position, 1.0f, LayerMask.GetMask("Human"));
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{
    public float player_speedright_max;
    public float player_speedleft_max;
    public float player_jump_power;

    public int handling = 0; // 0:false, 1:sensing but not handling, 2:handling

    public bool player_direction = false; //우측이 false, 좌측이 true. flipX에 맞추어

    Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    Animator animator;

    Collider2D[] collider2d;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        
        //줍기
        if(Input.GetKeyDown(KeyCode.Z) && handling == 0)
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

                collider2d[index_check].GetComponent<item>().handed = true;
                    
                Debug.Log(collider2d[index_check].GetComponent<item>().handed);

                handling = 1;

            }
        }
        
        /*
        //중력 변환
        if(Input.GetKeyDown(KeyCode.G))
        {
            rigid.gravityScale *= -1;
            if (rigid.gravityScale < 0)
            {
                spriteRenderer.flipY = true;
            }
            else
                spriteRenderer.flipY = false;

        }

        if(Input.GetKeyDown(KeyCode.E)) // 위로 세게
        {
            rigid.gravityScale -= 2;
            if (rigid.gravityScale < 0)
            {
                spriteRenderer.flipY = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) // 아래로 세게
        {
            rigid.gravityScale += 2;
            if (rigid.gravityScale > 0)
            {
                spriteRenderer.flipY = false;
            }
        }
        */

        //Jump
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {   
            rigid.AddForce(Vector2.up * player_jump_power, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }

        //stop state
        if (Input.GetButtonUp("Horizontal"))
        {
            //rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.2f, rigid.velocity.y);
            animator.SetBool("isWalking", Input.GetButton("Horizontal"));
            //animator.SetBool("isWalking", false);
        }

        //sprite direction change and walking state
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetKeyDown(KeyCode.LeftArrow) == true;
            player_direction = spriteRenderer.flipX;
            //spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1; (I don't know why It makes matter)
            animator.SetBool("isWalking", true);
        }
    }

    // Update is called once per frame
    // FixedUpdate is called per fixed time
    void FixedUpdate()
    {
        //key control move

        //Move and Max speed

        float h = Input.GetAxisRaw("Horizontal");

        if (rigid.velocity.x > player_speedright_max)
        {
            rigid.velocity = new Vector2(player_speedright_max, rigid.velocity.y);
        }
        else if (rigid.velocity.x < player_speedleft_max)
        {
            rigid.velocity = new Vector2(player_speedleft_max, rigid.velocity.y);
        }
        else
            rigid.AddForce(Vector2.right * 3.0f * h, ForceMode2D.Impulse);


        //Landing platform
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(1, 1, 1));

            RaycastHit2D raycastHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform")); // 0.5는 캐릭터크기에 종속되도록.
            RaycastHit2D raycastHit_item = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("item"));

            if (raycastHit.collider != null || raycastHit_item.collider != null)
            {
                if (raycastHit.distance <= 0.5f || raycastHit_item.distance <= 0.5f)//점프가 낮다면 모션변화를 눈치채지못할 것.
                {
                    animator.SetBool("isJumping", false);
                }
            }


        }

        
        //주위 아이템 감지
        collider2d = Physics2D.OverlapCircleAll(rigid.position, 1.0f, LayerMask.GetMask("item"));
        //Debug.Log(collider2d[0].name);
        
    }

}

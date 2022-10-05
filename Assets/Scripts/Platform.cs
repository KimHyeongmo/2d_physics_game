using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    

    public float against_scale = 1; //반사 충돌 크기 //new로 class별도 분리하여 관리. 생성자에 이 class를 담도록. 변수 수정하기가 까다로움.
    public float friction_scale = 0.5f; //마찰계수(임의)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //공 반사 충돌
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.tag.Equals("item")) //사람 반사도 고려
        {
            Vector2 enter_vector = collision.gameObject.GetComponent<item>().velocity_oneframe;            
            enter_vector = against_scale * Vector2.Reflect(enter_vector, collision.contacts[0].normal);
            enter_vector = new Vector2(enter_vector.x, enter_vector.y * 0.9f);
            collision.rigidbody.velocity = enter_vector;            
        }
    }
    

    
    //마찰
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.rigidbody.velocity.x > 0.3f)
        {
            collision.rigidbody.AddForce(Vector2.right * -1.0f * friction_scale, ForceMode2D.Impulse);
        }
        else if (collision.rigidbody.velocity.x < -0.3f)
        {
            collision.rigidbody.AddForce(Vector2.right * 1.0f * friction_scale, ForceMode2D.Impulse);
        }
        else
            collision.rigidbody.velocity = new Vector2(0, collision.rigidbody.velocity.y)   ;
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    

    public float against_scale = 1; //�ݻ� �浹 ũ�� //new�� class���� �и��Ͽ� ����. �����ڿ� �� class�� �㵵��. ���� �����ϱⰡ ��ٷο�.
    public float friction_scale = 0.5f; //�������(����)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //�� �ݻ� �浹
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.tag.Equals("item")) //��� �ݻ絵 ���
        {
            Vector2 enter_vector = collision.gameObject.GetComponent<item>().velocity_oneframe;            
            enter_vector = against_scale * Vector2.Reflect(enter_vector, collision.contacts[0].normal);
            enter_vector = new Vector2(enter_vector.x, enter_vector.y * 0.9f);
            collision.rigidbody.velocity = enter_vector;            
        }
    }
    

    
    //����
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

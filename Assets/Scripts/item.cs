using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    Rigidbody2D rigid;
    rigid_property_tempt tempt_rigid;
    Player_move player_script;
    GameObject player_hand;
        
    private float tempt_gravityscale;
    public bool handed = false;
    public Vector2 velocity_oneframe = new Vector2(1f, 1f);

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_move>();
        player_hand = GameObject.FindGameObjectWithTag("player_hand");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //줍기(포지션) - 추후 joint로 시도해보기
        if (player_script.handling == 1 && handed == true)
        {
            tempt_rigid = rigidbody2d_copy_for_tempt(rigid);
            Destroy(rigid);
            transform.SetParent(player_hand.transform);
            transform.localPosition = Vector3.zero;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            player_script.handling = 2;
        }
        //true면 던지기
        if (player_script.handling == 2 && Input.GetKeyDown(KeyCode.LeftControl) && handed == true)
        {
            transform.SetParent(null);
            player_script.handling = 0;
            gameObject.AddComponent<Rigidbody2D>();
            rigid = GetComponent<Rigidbody2D>();
            rigidbody2d_paste_for_tempt(rigid);
            rigid.AddForce(Vector2.right * (player_script.player_direction == true ? -10 : 10), ForceMode2D.Impulse);
            handed = false;
        }
        //velocity직전 보관
        StartCoroutine(velocity_oneframe_store());
    }

    IEnumerator velocity_oneframe_store()
    {
        yield return null;
        if(rigid != null)
            velocity_oneframe = rigid.velocity;
    }

    rigid_property_tempt rigidbody2d_copy_for_tempt(Rigidbody2D rigid_origin)
    {
        rigid_property_tempt rigid_copy = new rigid_property_tempt(0, 0, 0, 0);
        rigid_copy.mass = rigid_origin.mass;
        rigid_copy.drag = rigid_origin.drag;
        rigid_copy.angularDrag = rigid_origin.angularDrag;
        rigid_copy.gravityScale = rigid_origin.gravityScale;

        return rigid_copy;
    }

    void rigidbody2d_paste_for_tempt(Rigidbody2D rigid_origin)
    {
        rigid_origin.mass = tempt_rigid.mass;
        rigid_origin.drag = tempt_rigid.drag;
        rigid_origin.angularDrag = tempt_rigid.angularDrag;
        rigid_origin.gravityScale = tempt_rigid.gravityScale;
    }
}

// 보안(캡슐화) 문제

public class rigid_property_tempt
{
    public float mass, drag, angularDrag, gravityScale;

    public rigid_property_tempt(float a, float b, float c, float d)
    {
        mass = a;
        drag = b;
        angularDrag = c;
        gravityScale = d;
    }
}
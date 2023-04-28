using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMove : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private int upForce;
    private bool isGround;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        upForce = 200;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        //Wキーで上、Aキーで左、Sキーで下、Dキーで右に移動。
            if (Input.GetKey(KeyCode.A))
            {
                position.x -= speed * Time.deltaTime;
                //移動しているときはrunflagにtrueを入れる
                /*worldAngle.x = 0f; // ワールド座標を基準に、x軸を軸にした回転を10度に変更
                worldAngle.y = 0f; // ワールド座標を基準に、y軸を軸にした回転を10度に変更
                worldAngle.z = 0f; // ワールド座標を基準に、z軸を軸にした回転を10度に変更
                myTransform.eulerAngles = worldAngle; // 回転角度を設定*/
            }
            else if (Input.GetKey(KeyCode.D))
            {
                position.x += speed * Time.deltaTime;
                //移動しているときはrunflagにtrueを入れる
                /*worldAngle.x = 0f; // ワールド座標を基準に、x軸を軸にした回転を10度に変更
                worldAngle.y = 180f; // ワールド座標を基準に、y軸を軸にした回転を10度に変更
                worldAngle.z = 0f; // ワールド座標を基準に、z軸を軸にした回転を10度に変更
                myTransform.eulerAngles = worldAngle;  回転角度を設定*/

            }
            else if (Input.GetKey(KeyCode.W))
            {
                position.z += speed * Time.deltaTime;
                //移動しているときはrunflagにtrueを入れる
            }
            else if (Input.GetKey(KeyCode.S))
            {
                position.z -= speed * Time.deltaTime;
                //移動しているときはrunflagにtrueを入れる
            }
        
        

        transform.position = position;

        if (Input.GetKeyDown("space") && isGround)
            rb.AddForce(new Vector3(0, upForce, 0));
    }

    

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Floor")
            isGround = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Floor")
            isGround = false;
    }


}

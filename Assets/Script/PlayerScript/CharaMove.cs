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

        //W�L�[�ŏ�AA�L�[�ō��AS�L�[�ŉ��AD�L�[�ŉE�Ɉړ��B
            if (Input.GetKey(KeyCode.A))
            {
                position.x -= speed * Time.deltaTime;
                //�ړ����Ă���Ƃ���runflag��true������
                /*worldAngle.x = 0f; // ���[���h���W����ɁAx�������ɂ�����]��10�x�ɕύX
                worldAngle.y = 0f; // ���[���h���W����ɁAy�������ɂ�����]��10�x�ɕύX
                worldAngle.z = 0f; // ���[���h���W����ɁAz�������ɂ�����]��10�x�ɕύX
                myTransform.eulerAngles = worldAngle; // ��]�p�x��ݒ�*/
            }
            else if (Input.GetKey(KeyCode.D))
            {
                position.x += speed * Time.deltaTime;
                //�ړ����Ă���Ƃ���runflag��true������
                /*worldAngle.x = 0f; // ���[���h���W����ɁAx�������ɂ�����]��10�x�ɕύX
                worldAngle.y = 180f; // ���[���h���W����ɁAy�������ɂ�����]��10�x�ɕύX
                worldAngle.z = 0f; // ���[���h���W����ɁAz�������ɂ�����]��10�x�ɕύX
                myTransform.eulerAngles = worldAngle;  ��]�p�x��ݒ�*/

            }
            else if (Input.GetKey(KeyCode.W))
            {
                position.z += speed * Time.deltaTime;
                //�ړ����Ă���Ƃ���runflag��true������
            }
            else if (Input.GetKey(KeyCode.S))
            {
                position.z -= speed * Time.deltaTime;
                //�ړ����Ă���Ƃ���runflag��true������
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

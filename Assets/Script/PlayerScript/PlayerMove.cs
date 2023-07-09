using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float speed;
    private Rigidbody rb;
    [SerializeField] private PlayerFollowCamera refCamera;  // カメラの水平回転を参照する用
    [SerializeField] private float applySpeed = 0.2f;       // 振り向きの適用速度
    public JoyStickMove jsm;
    public AnimationManager anm;
    private bool isGround;
    private float upForce;
    private bool isAttackPressed;
    private bool isAttacking;
    [SerializeField] private Vector3 velocity;
    private int count;

    [SerializeField]
    private float attackDelay;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        upForce = 8.0f;
        isGround = false;
        Debug.Log(isAttackPressed);
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (jsm.joyStick.activeSelf)
        {
            velocity = Vector3.zero;
            if (JoyStickMove.joyStickPosY > 0)
                velocity.z += JoyStickMove.joyStickPosY;
            if (JoyStickMove.joyStickPosX < 0)
                velocity.x += JoyStickMove.joyStickPosX;
            if (JoyStickMove.joyStickPosY < 0)
                velocity.z += JoyStickMove.joyStickPosY;
            if (JoyStickMove.joyStickPosX > 0)
                velocity.x += JoyStickMove.joyStickPosX;

            // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
            velocity = velocity.normalized * speed * Time.deltaTime;


            // プレイヤーの回転(transform.rotation)の更新
            // 無回転状態のプレイヤーのZ+方向(後頭部)を、
            // カメラの水平回転(refCamera.hRotation)で回した移動の反対方向(-velocity)に回す回転に段々近づけます
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(refCamera.hRotation * velocity),
                                                  applySpeed);

            // プレイヤーの位置(transform.position)の更新
            // カメラの水平回転(refCamera.hRotation)で回した移動方向(velocity)を足し込みます
            transform.position += refCamera.hRotation * velocity;

            //動くべき方向を変数に格納
            /*Vector3 moveDir = ((transform.forward * JoyStickMove.joyStickPosY) +
                (transform.right * JoyStickMove.joyStickPosX)).normalized;*/

            //回転を更新
            //transform.rotation = Quaternion.Euler(0, JoyStickCam.rotX, 0);

            //ポジション更新
            //transform.position += moveDir * speed * Time.deltaTime;

        }

        if (isGround && !isAttacking)
        {
            if (jsm.joyStick.activeSelf)
            {

                anm.Play("Run");

            }
            else
            {
                anm.Play("Idle");
            }

            if (Input.GetKeyDown("space"))
            {
                rb.AddForce(transform.up * upForce, ForceMode.Impulse);
                anm.Play("Jumping");
                isGround = false;
            }

        }

        
            if (isAttackPressed)
            {

                if (!isAttacking)
                {
                    
                    isAttacking = true;
                    if (count == 0)
                    {
                       anm.Play("Attack1");
                    }

                    if(count == 1)
                    {
                       anm.Play("Attack2");
                    }

                    if(count == 2)
                    {
                       anm.Play("Attack3");
                    }

                    isAttackPressed = false;
                    Invoke("AttackComplete", attackDelay);

                    count = 0;


                    
                }



            }
        

    }

    public void AttackComplete()
    {
        isAttacking = false;
    }

    public void AttackStart()
    {
        
        if (anm.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            count = 1;
        }

        if (anm.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            count = 2;
        }
        isAttackPressed = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
            isGround = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Floor")
            isGround = false;
    }

    /*IEnumerator AnimationChange()
    {
        var state = anm.animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length);
        attackFlag = false;
    }*/

}

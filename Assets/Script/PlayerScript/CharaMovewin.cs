using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMovewin : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private int upForce;
    private bool isGround;
    [SerializeField] private Vector3 velocity;              // 移動方向
    [SerializeField] private float moveSpeed = 15.0f;        // 移動速度
    [SerializeField] private float applySpeed = 0.2f;       // 振り向きの適用速度
    [SerializeField] private PlayerFollowCamera refCamera;  // カメラの水平回転を参照する用

    Animator animator;
    public GameObject player;
    public AllStateDataBase allStateDate;
    private string currentState;


    //private AnimationManager amm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        upForce = 3;
        transform.hasChanged = false;
        animator = GetComponent<Animator>();
        isGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
        velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            velocity.z += 5;
        if (Input.GetKey(KeyCode.A))
            velocity.x -= 5;
        if (Input.GetKey(KeyCode.S))
            velocity.z -= 5;
        if (Input.GetKey(KeyCode.D))
            velocity.x += 5;

        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown("space") && isGround)
        {
            rb.AddForce(new Vector3(1.0f, upForce, 1.0f),ForceMode.Impulse);
            isGround = false;
            ChangeAnimationState(allStateDate.allStateList[2].stateName);
            
        }

        if (isGround)
        {
            if (velocity.magnitude > 0)
            {
                ChangeAnimationState(allStateDate.allStateList[1].stateName);
                // プレイヤーの回転(transform.rotation)の更新
                // 無回転状態のプレイヤーのZ+方向(後頭部)を、
                // カメラの水平回転(refCamera.hRotation)で回した移動の反対方向(-velocity)に回す回転に段々近づけます
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(refCamera.hRotation * velocity),
                                                      applySpeed);

                // プレイヤーの位置(transform.position)の更新
                // カメラの水平回転(refCamera.hRotation)で回した移動方向(velocity)を足し込みます
                transform.position += refCamera.hRotation * velocity;
            }
            else
            {
                ChangeAnimationState(allStateDate.allStateList[0].stateName);
            }
        }
        
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

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.CrossFadeInFixedTime(newState, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        animator.Play(newState);
        currentState = newState;
    }



}

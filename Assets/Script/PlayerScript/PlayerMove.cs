using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float speed;
    private Rigidbody rb;
    [SerializeField] private PlayerFollowCamera refCamera;  // �J�����̐�����]���Q�Ƃ���p
    [SerializeField] private float applySpeed = 0.2f;       // �U������̓K�p���x
    public JoyStickMove jsm;
    public AnimationManager anm;
    private bool isGround;
    private float upForce;
    private bool isAttackPressed;
    private bool isAttacking;
    [SerializeField] private Vector3 velocity;
    private int count;
    private bool isAvoidPressed;
    private AnimatorStateInfo state;
    [SerializeField]
    private float attackDelay;
    private bool isAvoiding;
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



        if (Input.GetKeyDown(KeyCode.A))
        {
            isAvoidPressed = true;
            
        }

        if (jsm.joyStick.activeSelf && !isAttacking)
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

            // ���x�x�N�g���̒�����1�b��moveSpeed�����i�ނ悤�ɒ������܂�
            velocity = velocity.normalized * speed * Time.deltaTime;


            // �v���C���[�̉�](transform.rotation)�̍X�V
            // ����]��Ԃ̃v���C���[��Z+����(�㓪��)���A
            // �J�����̐�����](refCamera.hRotation)�ŉ񂵂��ړ��̔��Ε���(-velocity)�ɉ񂷉�]�ɒi�X�߂Â��܂�
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(refCamera.hRotation * velocity),
                                                  applySpeed);

            // �v���C���[�̈ʒu(transform.position)�̍X�V
            // �J�����̐�����](refCamera.hRotation)�ŉ񂵂��ړ�����(velocity)�𑫂����݂܂�
            transform.position += refCamera.hRotation * velocity;


        }

        if (isGround && !isAttacking && !isAvoiding)
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

        if (isAvoidPressed)
        {
            if (!isAvoiding)
            {
                isAvoiding = true;
                anm.Play("Rolling");
                isAvoidPressed = false;

                state = anm.animator.GetCurrentAnimatorStateInfo(0);
                Invoke("AvoidFinish", state.length);
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

                    state = anm.animator.GetCurrentAnimatorStateInfo(0);
                    Invoke("AttackComplete", state.length);

                    count = 0;



                    
                }



            }
        

    }

    private void AttackComplete()
    {
        isAttacking = false;

    }

    private void AvoidFinish()
    {
        isAvoiding = false;
    }

    public void AttackComboReset()
    {
        if(count == 2)
        {
            count = 0;
        }
    }
    public void AttackStart()
    {

        if(count == -1)
        {
            count = 0;
        }
        
        if (anm.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            count = 1;
        }

        if (anm.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            count = 2;
        }

        if (anm.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            count = -1;
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


}

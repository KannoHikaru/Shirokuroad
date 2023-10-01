using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMovewin : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private int upForce;
    private bool isGround;
    [SerializeField] private Vector3 velocity;              // �ړ�����
    [SerializeField] private float moveSpeed = 15.0f;        // �ړ����x
    [SerializeField] private float applySpeed = 0.2f;       // �U������̓K�p���x
    [SerializeField] private PlayerFollowCamera refCamera;  // �J�����̐�����]���Q�Ƃ���p

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
        // WASD���͂���AXZ����(�����Ȓn��)���ړ��������(velocity)�𓾂܂�
        velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            velocity.z += 5;
        if (Input.GetKey(KeyCode.A))
            velocity.x -= 5;
        if (Input.GetKey(KeyCode.S))
            velocity.z -= 5;
        if (Input.GetKey(KeyCode.D))
            velocity.x += 5;

        // ���x�x�N�g���̒�����1�b��moveSpeed�����i�ނ悤�ɒ������܂�
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

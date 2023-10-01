using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    public EnemyAnimationManager.EnemyState currentstate;//�L�����̏��
    private Transform targetTransform;//�^�[�Q�b�g�̏��
    private NavMeshAgent navMeshAgent;//NavMeshAgent�R���|�[�l���g
    private Vector3 destination;//�ړI�n�̈ʒu�����i�[���邽�߂̃p�����[�^
    public EnemyAnimationManager eAnm;
    private bool isAttackPossibled;




    // Start is called before the first frame update
    void Start()
    {
        

        //�L������NavMeshAgent�R���|�[�l���g��navMeshAgent���֘A�t����
        navMeshAgent = GetComponent<NavMeshAgent>();

        //������Ԃ�Idle�ɐݒ肷��
        SetState(EnemyAnimationManager.EnemyState.Idle);

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(currentstate);

        if (currentstate == EnemyAnimationManager.EnemyState.Chase || currentstate == EnemyAnimationManager.EnemyState.Freeze)
        {
            if(targetTransform == null)
            {
                SetState(EnemyAnimationManager.EnemyState.Idle);
            }
            else
            {
                SetDestination(targetTransform.position);
                navMeshAgent.SetDestination(GetDestination());
            }

            //�G�̌������v���C���[�̕����ɏ������ς���
            var dir = (GetDestination() - transform.position).normalized;
            //�㉺�����Ɍ����Ȃ��悤�ɂ���
            dir.y = 0;
            Quaternion setRotation = Quaternion.LookRotation(dir);
            //�Z�o�������̊p�x��G�̊p�x�ɐݒ�
            transform.rotation = Quaternion.Slerp(transform.rotation, setRotation, navMeshAgent.angularSpeed * 0.1f * Time.deltaTime);

        }




        //if (isAttackPossibled)
        //{

        // eAnm.Play("Attack1");

        /*animationState = eAnm.animator.GetCurrentAnimatorStateInfo(0);
        myAnimatorClip = eAnm.animator.GetCurrentAnimatorClipInfo(0);
        attackDelay = myAnimatorClip[0].clip.length * animationState.normalizedTime;*/
        //var state = eAnm.animator.GetCurrentAnimatorStateInfo(0);

        //Invoke("AttackComplete", state.length);



        //}
    }

    public void SetState(EnemyAnimationManager.EnemyState tempState,Transform targetObject = null) //�ݒ肳�ꂽ�Ƃ��ɌĂ΂�鏈��
    {
        currentstate = tempState;

        if(tempState == EnemyAnimationManager.EnemyState.Idle && !isAttackPossibled)
        {
            navMeshAgent.isStopped = true;//�����Ȃ��悤�ɂ���
            eAnm.Play("Idle");
        }
        else if(tempState == EnemyAnimationManager.EnemyState.Chase && !isAttackPossibled)
        {
            
            targetTransform = targetObject;//�^�[�Q�b�g�Ȃ�I�u�W�F�N�g�̍��W��targetTransform�ɐݒ肷��
            navMeshAgent.SetDestination(targetTransform.position); //�ړI�n���^�[�Q�b�g�̈ʒu�ɐݒ�
            navMeshAgent.isStopped = false;//������悤�ɂ���
            eAnm.Play("Chase");
        }
        else if (tempState == EnemyAnimationManager.EnemyState.Attack && !isAttackPossibled)
        {
            isAttackPossibled = true;

            if (isAttackPossibled)
            {
                eAnm.Play("Attack1");

                var state = eAnm.animator.GetCurrentAnimatorStateInfo(0);

                Invoke("AttackComplete", 5.09f);
            }

            navMeshAgent.isStopped = true;
            
            

        }
        else if(tempState == EnemyAnimationManager.EnemyState.Freeze)
        {

           

            Invoke("ResetState", 0.50f);
        }
    }
    //�G�L�����N�^�[�̏�Ԃ��擾���邽�߂̃��\�b�h
    public EnemyAnimationManager.EnemyState GetState()
    {
        return currentstate;
    }
    //�ړI�n��ݒ肷�邽�߂̃��\�b�h
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }
    //�ړI�n���擾���邽�߂̃��\�b�h
    public Vector3 GetDestination()
    {
        return destination;
    }


    /*IEnumerator AnimationChange()
    {


        yield return null;
        var state = eAnm.animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length);


        isAttacking = false;


    }*/

    private void AttackComplete()
    {
        
        SetState(EnemyAnimationManager.EnemyState.Freeze);
        
    }

    private void ResetState()
    {
        isAttackPossibled = false;
        SetState(EnemyAnimationManager.EnemyState.Idle);

    }

}

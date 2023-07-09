using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    public EnemyAnimationManager.EnemyState state;//�L�����̏��
    private Transform targetTransform;//�^�[�Q�b�g�̏��
    private NavMeshAgent navMeshAgent;//NavMeshAgent�R���|�[�l���g
    private Vector3 destination;//�ړI�n�̈ʒu�����i�[���邽�߂̃p�����[�^
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
        if(state == EnemyAnimationManager.EnemyState.Chase)
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
            //var dir = (GetDestination() - transform.position).normalized;
            //�㉺�����Ɍ����Ȃ��悤�ɂ���
            //dir.y = 0;
            //Quaternion setRotation = Quaternion.LookRotation(dir);
            //�Z�o�������̊p�x��G�̊p�x�ɐݒ�
            //targetTransform.rotation = Quaternion.Slerp(transform.rotation, setRotation, navMeshAgent.angularSpeed * 0.1f * Time.deltaTime);
        }
    }

    public void SetState(EnemyAnimationManager.EnemyState tempState,Transform targetObject = null) //�ݒ肳�ꂽ�Ƃ��ɌĂ΂�鏈��
    {
        state = tempState;

        if(tempState == EnemyAnimationManager.EnemyState.Idle)
        {
            navMeshAgent.isStopped = true;//�����Ȃ��悤�ɂ���
        }
        else if(tempState == EnemyAnimationManager.EnemyState.Chase)
        {
            targetTransform = targetObject;//�^�[�Q�b�g�Ȃ�I�u�W�F�N�g�̍��W��targetTransform�ɐݒ肷��
            navMeshAgent.isStopped = false;//������悤�ɂ���
        }else if (tempState == EnemyAnimationManager.EnemyState.Attack1)
        {
            navMeshAgent.isStopped = true;
        }
    }
    //�G�L�����N�^�[�̏�Ԃ��擾���邽�߂̃��\�b�h
    public EnemyAnimationManager.EnemyState GetState()
    {
        return state;
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

}

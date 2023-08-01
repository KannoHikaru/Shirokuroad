using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    private EnemyAnimationManager.EnemyState currentstate;//キャラの状態
    private Transform targetTransform;//ターゲットの情報
    private NavMeshAgent navMeshAgent;//NavMeshAgentコンポーネント
    private Vector3 destination;//目的地の位置情報を格納するためのパラメータ
    public EnemyAnimationManager eAnm;
    private bool isAttacking;
    private bool isAttackPossibled;
    public bool firstInIsAttackPossibled;


    // Start is called before the first frame update
    void Start()
    {
        //キャラのNavMeshAgentコンポーネントとnavMeshAgentを関連付ける
        navMeshAgent = GetComponent<NavMeshAgent>();

        //初期状態をIdleに設定する
        SetState(EnemyAnimationManager.EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isAttacking);

        Debug.Log("呼び出されているか:" + IsInvoking("AttackComplete"));

        if (currentstate == EnemyAnimationManager.EnemyState.Chase)
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

            


            //敵の向きをプレイヤーの方向に少しずつ変える
            //var dir = (GetDestination() - transform.position).normalized;
            //上下方向に向かないようにする
            //dir.y = 0;
            //Quaternion setRotation = Quaternion.LookRotation(dir);
            //算出下方向の角度を敵の角度に設定
            //targetTransform.rotation = Quaternion.Slerp(transform.rotation, setRotation, navMeshAgent.angularSpeed * 0.1f * Time.deltaTime);
        }

        if (isAttackPossibled)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                eAnm.Play("Attack1");
                isAttackPossibled = false;

                /*animationState = eAnm.animator.GetCurrentAnimatorStateInfo(0);
                myAnimatorClip = eAnm.animator.GetCurrentAnimatorClipInfo(0);
                attackDelay = myAnimatorClip[0].clip.length * animationState.normalizedTime;*/
                var state = eAnm.animator.GetCurrentAnimatorStateInfo(0);
                Invoke("AttackComplete", state.length);
                Debug.Log("呼び出されているか:" + IsInvoking("AttackComplete"));


            }
        }
    }

    public void SetState(EnemyAnimationManager.EnemyState tempState,Transform targetObject = null) //設定されたときに呼ばれる処理
    {
        currentstate = tempState;

        if(tempState == EnemyAnimationManager.EnemyState.Idle && !isAttacking)
        {
            //isAttackPossibled = false;
            firstInIsAttackPossibled = false;
            navMeshAgent.isStopped = true;//動けないようにする
            eAnm.Play("Idle");
        }
        else if(tempState == EnemyAnimationManager.EnemyState.Chase && !isAttacking)
        {
            //isAttackPossibled = false;
            firstInIsAttackPossibled = false;
            targetTransform = targetObject;//ターゲットなるオブジェクトの座標をtargetTransformに設定する
            navMeshAgent.isStopped = false;//動けるようにする
            eAnm.Play("Chase");
        }
        else if (tempState == EnemyAnimationManager.EnemyState.Attack)
        {
            if (!firstInIsAttackPossibled)
            {
                isAttackPossibled = true;
                firstInIsAttackPossibled = true;
            }
            navMeshAgent.isStopped = true;
            

        }
        else if(tempState == EnemyAnimationManager.EnemyState.Freeze)
        {
            //isAttackPossibled = false;
            Invoke("ResetState", 1.0f);
        }
    }
    //敵キャラクターの状態を取得するためのメソッド
    public EnemyAnimationManager.EnemyState GetState()
    {
        return currentstate;
    }
    //目的地を設定するためのメソッド
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }
    //目的地を取得するためのメソッド
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
        isAttacking = false;
    }

    private void ResetState()
    {
        SetState(EnemyAnimationManager.EnemyState.Idle);
        isAttackPossibled = true;
    }

}

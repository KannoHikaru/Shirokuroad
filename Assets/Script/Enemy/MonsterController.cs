using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    public EnemyAnimationManager.EnemyState currentstate;//キャラの状態
    private Transform targetTransform;//ターゲットの情報
    private NavMeshAgent navMeshAgent;//NavMeshAgentコンポーネント
    private Vector3 destination;//目的地の位置情報を格納するためのパラメータ
    public EnemyAnimationManager eAnm;
    private bool isAttacking;
    private int count;
    private bool isAttackPossibled;

    // Start is called before the first frame update
    void Start()
    {
        //キャラのNavMeshAgentコンポーネントとnavMeshAgentを関連付ける
        navMeshAgent = GetComponent<NavMeshAgent>();

        count = 1;

        //初期状態をIdleに設定する
        SetState(EnemyAnimationManager.EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentstate == EnemyAnimationManager.EnemyState.Chase)
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
                switch (count)
                {
                    case 1:
                        eAnm.Play("Attack1");
                        break;
                    case 2:
                        eAnm.Play("Attack2");
                        break;

                }

                isAttackPossibled = false;

                /*animationState = eAnm.animator.GetCurrentAnimatorStateInfo(0);
                myAnimatorClip = eAnm.animator.GetCurrentAnimatorClipInfo(0);
                attackDelay = myAnimatorClip[0].clip.length * animationState.normalizedTime;*/
                StartCoroutine("AnimationChange");
                //Invoke("AttackComplete", 0.9f);


            }
        }
    }

    public void SetState(EnemyAnimationManager.EnemyState tempState,Transform targetObject = null) //設定されたときに呼ばれる処理
    {
        currentstate = tempState;

        if(tempState == EnemyAnimationManager.EnemyState.Idle)
        {
            count = 0;
            navMeshAgent.isStopped = true;//動けないようにする
            eAnm.Play("Idle");
        }
        else if(tempState == EnemyAnimationManager.EnemyState.Chase)
        {
            count = 0;
            targetTransform = targetObject;//ターゲットなるオブジェクトの座標をtargetTransformに設定する
            navMeshAgent.isStopped = false;//動けるようにする
            eAnm.Play("Chase");
        }
        else if (tempState == EnemyAnimationManager.EnemyState.Attack)
        {
            navMeshAgent.isStopped = true;
            isAttackPossibled = true;

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


    IEnumerator AnimationChange()
    {
        yield return null;
        var state = eAnm.animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length);
        isAttacking = false;
        if (currentstate == EnemyAnimationManager.EnemyState.Attack)
        {
            isAttackPossibled = true;

            if (count == 2)
            {
                count = 1;
            }
            else if (count < 2)
            {
                count++;
            }
        }

    }

}

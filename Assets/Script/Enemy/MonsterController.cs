using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    public EnemyAnimationManager.EnemyState state;//キャラの状態
    private Transform targetTransform;//ターゲットの情報
    private NavMeshAgent navMeshAgent;//NavMeshAgentコンポーネント
    private Vector3 destination;//目的地の位置情報を格納するためのパラメータ
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

            
            //敵の向きをプレイヤーの方向に少しずつ変える
            //var dir = (GetDestination() - transform.position).normalized;
            //上下方向に向かないようにする
            //dir.y = 0;
            //Quaternion setRotation = Quaternion.LookRotation(dir);
            //算出下方向の角度を敵の角度に設定
            //targetTransform.rotation = Quaternion.Slerp(transform.rotation, setRotation, navMeshAgent.angularSpeed * 0.1f * Time.deltaTime);
        }
    }

    public void SetState(EnemyAnimationManager.EnemyState tempState,Transform targetObject = null) //設定されたときに呼ばれる処理
    {
        state = tempState;

        if(tempState == EnemyAnimationManager.EnemyState.Idle)
        {
            navMeshAgent.isStopped = true;//動けないようにする
        }
        else if(tempState == EnemyAnimationManager.EnemyState.Chase)
        {
            targetTransform = targetObject;//ターゲットなるオブジェクトの座標をtargetTransformに設定する
            navMeshAgent.isStopped = false;//動けるようにする
        }else if (tempState == EnemyAnimationManager.EnemyState.Attack1)
        {
            navMeshAgent.isStopped = true;
        }
    }
    //敵キャラクターの状態を取得するためのメソッド
    public EnemyAnimationManager.EnemyState GetState()
    {
        return state;
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

}

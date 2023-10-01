using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    private MonsterController enemyMove = default;
    private float distance;
    public EnemyAnimationManager eAnm;
    private AnimatorStateInfo animationState;
    private AnimatorClipInfo[] myAnimatorClip;

    private float attackDelay;
    // Start is called before the first frame update
    void Start()
    {
        enemyMove = transform.parent.GetComponent<MonsterController>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            distance = Vector3.Distance(other.transform.position, this.transform.position);

            if (distance <= 3.0f && distance >= 0f && !(enemyMove.currentstate == EnemyAnimationManager.EnemyState.Freeze))
            {
                enemyMove.SetState(EnemyAnimationManager.EnemyState.Attack, other.gameObject.transform);
            }
            else if (distance > 3.0f && enemyMove.currentstate == EnemyAnimationManager.EnemyState.Idle)
            {
                enemyMove.SetState(EnemyAnimationManager.EnemyState.Chase, other.gameObject.transform);
                
            }

            if(enemyMove.currentstate == EnemyAnimationManager.EnemyState.Freeze)
            {
                enemyMove.SetDestination(other.gameObject.transform.position);
            }

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyMove.SetState(EnemyAnimationManager.EnemyState.Idle, null);

        }

    }


}

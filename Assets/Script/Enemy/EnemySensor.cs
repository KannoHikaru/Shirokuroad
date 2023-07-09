using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    private MonsterController enemyMove = default;
    private float distance;
    public EnemyAnimationManager eAnm;
    private bool isAttacking;
    private bool isAttackPossibled;
    private int count;
    private AnimatorStateInfo animationState;
    private AnimatorClipInfo[] myAnimatorClip;

    private float attackDelay;
    // Start is called before the first frame update
    void Start()
    {
        enemyMove = transform.parent.GetComponent<MonsterController>();
        count = 1;
        attackDelay = 0.8f;


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(count);


        if (isAttackPossibled)
        {
                if (!isAttacking)
                {
                    isAttacking = true;
                    enemyMove.SetState(EnemyAnimationManager.EnemyState.Attack1, null);
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

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            distance = Vector3.Distance(other.transform.position, this.transform.position);

            if (distance <= 3.0f && distance >= 1.0f)
            {
                isAttackPossibled = true;
            }else if (distance > 3.0f)
            {
                count = 0;
                enemyMove.SetState(EnemyAnimationManager.EnemyState.Chase, other.gameObject.transform);
                eAnm.Play("Chase");
            }

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyMove.SetState(EnemyAnimationManager.EnemyState.Idle, null);
            eAnm.Play("Idle");
        }

    }

    public void AttackComplete()
    {
        isAttacking = false;

        if (distance <= 3.0f && distance >= 1.0f)
        {
            isAttackPossibled = true;
        }

    }

    IEnumerator AnimationChange()
    {
        yield return null;
        var state = eAnm.animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length);
        isAttacking = false;
        if (distance <= 3.0f && distance >= 1.0f)
        {
            isAttackPossibled = true;

            if(count == 2)
            {
                count = 1;
            }else if(count < 2)
            {
                count++;
            }
        }

    }


}

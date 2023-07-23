using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAnimationManager : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack1,
        Attack2,
        Attack3,
        Attack
    }

    public EnemyState[] cangedstates;

    [SerializeField]
    private float transitionDuration;
    public EnemyState curretState;
    public Animator animator;
    private string currentState;

    // Start is called before the first frame update
    void Start()
    {
        
        //currentState = State.Idle.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play(string newState)
    {
        foreach (EnemyState an in cangedstates)
        {
            if (currentState == newState) return;
            if (an.ToString() == newState)
            {
                animator.CrossFadeInFixedTime(newState, transitionDuration);
                //animator.Play(newState);
                currentState = newState;
            }
        }


    }

}

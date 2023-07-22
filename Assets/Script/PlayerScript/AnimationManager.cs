using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationManager : MonoBehaviour
{
    public enum State
    {
        Idle,
        Run,
        Attack1,
        Attack2,
        Attack3,
        Jumping,
        Rolling
    }

    public State[] cangedstates;

    public float transitionDuration;
    public State curretState;
    public Animator animator;
    private string currentState;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //currentState = State.Idle.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string newState)
    {
        foreach(State an in cangedstates)
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

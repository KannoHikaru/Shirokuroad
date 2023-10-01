using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isOpen;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("switchOn", false);
        isOpen = false;
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.SetBool("switchOn", true);
            StartCoroutine("AnimationChange");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.SetBool("switchOn", false);
        }
    }

    IEnumerator AnimationChange()
    {
        
        yield return null;
        var state = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length);
        isOpen = true;
    }
}

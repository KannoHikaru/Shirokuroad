using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Testy : MonoBehaviour
{
    [SerializeField] Transform target;
    private NavMeshAgent agent;
    private float speed = 3f;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
       
        distance = Vector3.Distance(target.position,this.transform.position);

        if(distance < 4)
        {
            agent.destination = target.position;
        }
    }
}

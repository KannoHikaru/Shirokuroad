using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MonsterSenser : MonoBehaviour
{
    [SerializeField]
    private SphereCollider searchArea = default;
    [SerializeField]
    private float searchAngle = 45f;
    [SerializeField]
    private GameObject control;

    [SerializeField]
    private bool editor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            
        }
        else
        {
            
        }
    }

    /*private void OnDrawGizmos()
    {
        if (editor)
        {
            Handles.color = Color.red;
            Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -searchAngle, 0f) * transform.forward,searchAngle * 2f, searchArea.radius * 0.5f);
        }
    }*/

}

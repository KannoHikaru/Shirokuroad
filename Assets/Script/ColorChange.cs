using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{

    void Start()
    {
        GetComponent<UnityEngine.Camera>().backgroundColor = Color.black;
    }


    void Update()
    {

    }
}
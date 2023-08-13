using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusProcess : MonoBehaviour
{

    [SerializeField] PlayerStatusSO playerStatusSO;
    private int currentHP;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = playerStatusSO.HP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

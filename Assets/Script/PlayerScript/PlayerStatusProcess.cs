using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusProcess : MonoBehaviour
{

    private int damage;

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            /*damage = (enemyStrength / 2) - (diffence / 4)

            if(damage > 0)
            {
                currentHP -= damage;
            }*/

            
        }
    }
}

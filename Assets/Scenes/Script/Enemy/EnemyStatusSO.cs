using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStatusSO : ScriptableObject
{

    public List<EnemyStatus> enemyStatusList = new List<EnemyStatus>();

    [System.Serializable]
    public class EnemyStatus
    {
        [SerializeField] string name;
        [SerializeField] int hp;
        [SerializeField] int strength;
        [SerializeField] int diffence;
        [SerializeField] int exp;


        public string NAME { get => name; }
        public int HP { get => hp; }

        public int STRENGTH { get => strength; }
       
        public int DIFFENCE { get => diffence; }

        public int EXP { get => exp; }
    }

    
}

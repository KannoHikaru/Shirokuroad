using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponSO : ScriptableObject
{
    public List<WeaponStatus> weaponStatusList = new List<WeaponStatus>();

    [System.Serializable]
    public class WeaponStatus
    {
        [SerializeField] string name;
        [SerializeField] int strength;
        [SerializeField] WeaponType weapontype;
        public int STRENGTH { get => strength; }

        public WeaponType WEAPONTYPE { get => weapontype; }

        [SerializeField]
        public enum WeaponType
        {
            sword,
            bow,
            wand
        }




    }
    
    
}

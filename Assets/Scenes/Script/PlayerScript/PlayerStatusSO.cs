using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStatusSO : ScriptableObject
{
    [SerializeField] int lv;
    [SerializeField] int hp;
    [SerializeField] int mp;
    [SerializeField] int strength;
    [SerializeField] int magicStrength;
    [SerializeField] int diffence;
    [SerializeField] int exp;
    [SerializeField] int statusPoint;

    public int HP { get => hp; }
    public int EXP { get => exp; }
    public int LV { get => lv; }

    public int STRENGTH { get => strength; }
    public int MAGICSTRENGTH { get => magicStrength; }

    public int DIIFENCE { get => diffence; }

    public int STATUSPOINT { get => statusPoint; }

    public void LoadPlayerData(PlayerData playerData)
    {
        lv = playerData.lv;

    }
}

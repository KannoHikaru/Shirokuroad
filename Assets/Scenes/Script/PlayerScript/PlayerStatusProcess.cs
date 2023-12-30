using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusProcess : MonoBehaviour,IDamageable
{

    private int damage;

    [SerializeField] PlayerStatusSO playerStatusSO;
    [SerializeField] LvData lvData;
    private int currentHP;
    private int maxHP;
    
    private int aEXP;
    private int currentLV;
    private int currentStatusPoint;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = playerStatusSO.HP;
        maxHP = playerStatusSO.HP;
        aEXP = playerStatusSO.EXP;
        currentLV = playerStatusSO.LV;
        currentStatusPoint = playerStatusSO.STATUSPOINT;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int MAXHP { get => maxHP; }
    public int CURRENTLV { get => currentLV; }


    public int CURRENTSTATUSPOINT { get => currentStatusPoint; }

    public void Damage(int strength)
    {

        // charadata��null�łȂ������`�F�b�N
        if (playerStatusSO != null)
        {
            damage = (strength / 2) - (playerStatusSO.DIIFENCE / 4);


            if (damage > 0)
            {
                //�󂯎����strength���玩�g��DIFFENCE���������l��currentHP�������
                currentHP -= damage;
            }
            else
            {
                currentHP -= 1;
            }



        }


        // HP��0�ȉ��Ȃ�Death()���\�b�h���Ăяo���B
        if (currentHP <= 0)
        {
            Death();
        }
    }

    public void AddEXP(int getEXP)
    {
        if(getEXP > 0)
        {
            aEXP += getEXP;

            var tableEXP = lvData.playerExpTable[playerStatusSO.LV];

            if(aEXP >= tableEXP.exp)
            {
                currentLV += 1;
                currentStatusPoint += 1;
            }
        }


    }

    public void Death()
    {
        // �Q�[���I�u�W�F�N�g��j��
        this.gameObject.SetActive(false);
    }


}

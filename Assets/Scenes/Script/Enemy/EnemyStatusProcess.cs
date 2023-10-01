using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusProcess : MonoBehaviour, IDamageable
{
    //�V���A�������Ă���Bcharadata��Mazokusoldier���w��B
    private EnemyStatusSO.EnemyStatus enemyStatus;
    int currentHP;
    int damage;

    void Start()
    {
        enemyStatus = EnemyManager.instance.GetEnemyStatus(this.name);

        //charadata��null�łȂ����Ƃ��m�F
        if (enemyStatus != null)
        {
            //charadata�̍ő�HP�����B
            currentHP = enemyStatus.HP;

        }
    }

    // �_���[�W�����̃��\�b�h�@value�ɂ�Player1��ATK�̒l�������Ă�
    public void Damage(int strength)
    {

        // charadata��null�łȂ������`�F�b�N
        if (enemyStatus != null)
        {
            damage = (strength / 2) - (enemyStatus.DIFFENCE / 4);


            if(damage > 0)
            {
                //�󂯎����strength���玩�g��DIFFENCE���������l��currentHP�������
                currentHP -= damage;
            }

           
            
        }


        // HP��0�ȉ��Ȃ�Death()���\�b�h���Ăяo���B
        if (currentHP <= 0)
        {
            Death();
        }
    }
    // ���S�����̃��\�b�h
    public void Death()
    {
        EnemyManager.instance.PlayerAddEXP(enemyStatus.EXP);
        
        // �Q�[���I�u�W�F�N�g��j��
        Destroy(gameObject);
    }

    
}

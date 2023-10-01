using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusProcess : MonoBehaviour, IDamageable
{
    //シリアル化している。charadataのMazokusoldierを指定。
    private EnemyStatusSO.EnemyStatus enemyStatus;
    int currentHP;
    int damage;

    void Start()
    {
        enemyStatus = EnemyManager.instance.GetEnemyStatus(this.name);

        //charadataがnullでないことを確認
        if (enemyStatus != null)
        {
            //charadataの最大HPを代入。
            currentHP = enemyStatus.HP;

        }
    }

    // ダメージ処理のメソッド　valueにはPlayer1のATKの値が入ってる
    public void Damage(int strength)
    {

        // charadataがnullでないかをチェック
        if (enemyStatus != null)
        {
            damage = (strength / 2) - (enemyStatus.DIFFENCE / 4);


            if(damage > 0)
            {
                //受け取ったstrengthから自身のDIFFENCEを引いた値をcurrentHPから引く
                currentHP -= damage;
            }

           
            
        }


        // HPが0以下ならDeath()メソッドを呼び出す。
        if (currentHP <= 0)
        {
            Death();
        }
    }
    // 死亡処理のメソッド
    public void Death()
    {
        EnemyManager.instance.PlayerAddEXP(enemyStatus.EXP);
        
        // ゲームオブジェクトを破壊
        Destroy(gameObject);
    }

    
}

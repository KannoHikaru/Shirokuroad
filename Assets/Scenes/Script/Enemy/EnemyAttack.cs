using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyStatusSO.EnemyStatus enemyStatus;

    // Start is called before the first frame update
    void Start()
    {
        enemyStatus = EnemyManager.instance.GetEnemyStatus("Mutant");
        Debug.Log(this.name);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //otherのゲームオブジェクトのインターフェースを呼び出す
        IDamageable damageable = other.GetComponent<IDamageable>();

        //damageableにnull値が入っていないかチェック
        if (damageable != null)
        {

            //damageableのダメージ処理メソッドを呼び出す。引数としてPlayer1のATKを指定
            damageable.Damage(enemyStatus.STRENGTH);
        }
    }
}

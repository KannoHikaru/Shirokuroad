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
        //other�̃Q�[���I�u�W�F�N�g�̃C���^�[�t�F�[�X���Ăяo��
        IDamageable damageable = other.GetComponent<IDamageable>();

        //damageable��null�l�������Ă��Ȃ����`�F�b�N
        if (damageable != null)
        {

            //damageable�̃_���[�W�������\�b�h���Ăяo���B�����Ƃ���Player1��ATK���w��
            damageable.Damage(enemyStatus.STRENGTH);
        }
    }
}

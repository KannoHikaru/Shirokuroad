using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatusProcess : MonoBehaviour
{
    [SerializeField] PlayerStatusSO playerStatusSO;
    [SerializeField] WeaponSO.WeaponStatus weaponStatus;


    // Start is called before the first frame update
    void Start()
    {
        
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
          
            switch (weaponStatus.WEAPONTYPE)
            {
                case WeaponSO.WeaponStatus.WeaponType.bow:
                    //damageable�̃_���[�W�������\�b�h���Ăяo���B�����Ƃ���Player1��ATK���w��
                    damageable.Damage(playerStatusSO.STRENGTH * weaponStatus.STRENGTH);
                    break;
                case WeaponSO.WeaponStatus.WeaponType.sword:
                    damageable.Damage(playerStatusSO.STRENGTH * weaponStatus.STRENGTH);
                    break;
                case WeaponSO.WeaponStatus.WeaponType.wand:
                    damageable.Damage(playerStatusSO.MAGICSTRENGTH * weaponStatus.STRENGTH);
                    break;
            }
            
        }
    }
}

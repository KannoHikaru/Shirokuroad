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
        //otherのゲームオブジェクトのインターフェースを呼び出す
        IDamageable damageable = other.GetComponent<IDamageable>();

        //damageableにnull値が入っていないかチェック
        if (damageable != null)
        {
          
            switch (weaponStatus.WEAPONTYPE)
            {
                case WeaponSO.WeaponStatus.WeaponType.bow:
                    //damageableのダメージ処理メソッドを呼び出す。引数としてPlayer1のATKを指定
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;    // シングルトン用の変数

    [Header("EnemyStatusSOのスクリプタブル・オブジェクト")]
    public EnemyStatusSO enemyStatusSO;

    [Header("生成するアイテムのプレファブ")]
    public GameObject[] dropItemPrefabs;       // EnemyBaseに個別で持たせていたアイテムのプレファブ情報をこちらで集約して扱う
                                               // Start is called before the first frame update

    void Awake()
    {
        // このゲームオブジェクトをシングルトンにし、かつ、シーン遷移しても破棄されないようにします
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

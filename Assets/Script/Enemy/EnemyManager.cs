using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public AllEnemyDataBase allEnemyData;
    private List<GameObject> enemyList;
    MeshRenderer enemyMr;
    //時間間隔の最小値
    public float minTime;
    //時間間隔の最大値
    public float maxTime;
    //X座標の最小値
    public float xMinPosition;
    //X座標の最大値
    public float xMaxPosition;
    //Y座標の最小値
    public float yMinPosition;
    //Y座標の最大値
    public float yMaxPosition;
    //Z座標の最小値
    public float zMinPosition;
    //Z座標の最大値
    public float zMaxPosition;
    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0f;
    //経過時間(全体)
    private float gameTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        interval = GetRandomTime();
        EnemyGenerator();
        enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyGenerator();
        TransparentEnemy();
        gameTime += Time.deltaTime;
    }

    private float GetRandomTime()
    {
        return Random.Range(minTime, maxTime);
    }

    private void TransparentEnemy()
    {
        if (GameManager.instance.floorFlag)
        {
            foreach (GameObject enemy in enemyList)
            {
                enemyMr = enemy.GetComponent<MeshRenderer>();
                enemyMr.material.color = new Color(0f,0f,0f, 0.0f);
            }
        }
        else
        {
            foreach (GameObject enemy in enemyList)
            {
                enemyMr = enemy.GetComponent<MeshRenderer>();
                enemyMr.material.color = new Color(0.4f, 0.2f, 0.3f, 1.0f);
            }
        }
    }

    private void EnemyGenerator()
    {
        //時間計測
        time += Time.deltaTime;

        //経過時間が生成時間になったとき(生成時間より大きくなったとき)
        if (time > interval)
        {
            
            int ei = Random.Range(0, allEnemyData.allEnemyList.Count);
            //enemyをインスタンス化する(生成する)
            GameObject enemy = Instantiate(allEnemyData.allEnemyList[ei].enemy);
            enemyList.Add(enemy);
            
            //生成した敵の座標をランダムに決定する
            enemy.transform.position = GetRandomPosition();
            //経過時間を初期化して再度時間計測を始める
            time = 0f;
            //次に発生する時間間隔をランダムに決定する
            interval = GetRandomTime();

            
        }
    }

    private Vector3 GetRandomPosition()
    {
        //それぞれの座標をランダムに生成する
        float x = Random.Range(xMinPosition, xMaxPosition);
        float y = Random.Range(yMinPosition, yMaxPosition);
        float z = Random.Range(zMinPosition, zMaxPosition);


        //Vector3型のPositionを返す
        return new Vector3(0, 0.5f, 0f);
    }
}

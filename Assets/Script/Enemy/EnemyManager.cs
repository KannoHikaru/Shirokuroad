using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public AllEnemyDataBase allEnemyData;
    private List<GameObject> enemyList;
    MeshRenderer enemyMr;
    //���ԊԊu�̍ŏ��l
    public float minTime;
    //���ԊԊu�̍ő�l
    public float maxTime;
    //X���W�̍ŏ��l
    public float xMinPosition;
    //X���W�̍ő�l
    public float xMaxPosition;
    //Y���W�̍ŏ��l
    public float yMinPosition;
    //Y���W�̍ő�l
    public float yMaxPosition;
    //Z���W�̍ŏ��l
    public float zMinPosition;
    //Z���W�̍ő�l
    public float zMaxPosition;
    //�G�������ԊԊu
    private float interval;
    //�o�ߎ���
    private float time = 0f;
    //�o�ߎ���(�S��)
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
        //���Ԍv��
        time += Time.deltaTime;

        //�o�ߎ��Ԃ��������ԂɂȂ����Ƃ�(�������Ԃ��傫���Ȃ����Ƃ�)
        if (time > interval)
        {
            
            int ei = Random.Range(0, allEnemyData.allEnemyList.Count);
            //enemy���C���X�^���X������(��������)
            GameObject enemy = Instantiate(allEnemyData.allEnemyList[ei].enemy);
            enemyList.Add(enemy);
            
            //���������G�̍��W�������_���Ɍ��肷��
            enemy.transform.position = GetRandomPosition();
            //�o�ߎ��Ԃ����������čēx���Ԍv�����n�߂�
            time = 0f;
            //���ɔ������鎞�ԊԊu�������_���Ɍ��肷��
            interval = GetRandomTime();

            
        }
    }

    private Vector3 GetRandomPosition()
    {
        //���ꂼ��̍��W�������_���ɐ�������
        float x = Random.Range(xMinPosition, xMaxPosition);
        float y = Random.Range(yMinPosition, yMaxPosition);
        float z = Random.Range(zMinPosition, zMaxPosition);


        //Vector3�^��Position��Ԃ�
        return new Vector3(0, 0.5f, 0f);
    }
}

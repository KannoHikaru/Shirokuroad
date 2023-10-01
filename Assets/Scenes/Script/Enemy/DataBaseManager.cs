using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;    // �V���O���g���p�̕ϐ�

    [Header("EnemyStatusSO�̃X�N���v�^�u���E�I�u�W�F�N�g")]
    public EnemyStatusSO enemyStatusSO;

    [Header("��������A�C�e���̃v���t�@�u")]
    public GameObject[] dropItemPrefabs;       // EnemyBase�ɌʂŎ������Ă����A�C�e���̃v���t�@�u����������ŏW�񂵂Ĉ���
                                               // Start is called before the first frame update

    void Awake()
    {
        // ���̃Q�[���I�u�W�F�N�g���V���O���g���ɂ��A���A�V�[���J�ڂ��Ă��j������Ȃ��悤�ɂ��܂�
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

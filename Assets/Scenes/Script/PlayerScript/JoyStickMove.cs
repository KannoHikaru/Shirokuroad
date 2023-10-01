using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMove : MonoBehaviour
{
    //スティック格納
    public GameObject joyStick;
    //ジョイスティックキャンバスのポジション
    private RectTransform joyStickRectTransform;
    //ジョイスティックの後ろのやつ
    public GameObject backGround;
    //スティックが動ける範囲
    public int stickRange = 3;
    //実際に動く値
    private int stickMovement = 0;

    public static float joyStickPosX;
    public static float joyStickPosY;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        //初期設定
        Initialization();
        angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialization()
    {
        //違う画面サイズでも似たような挙動にするため
        stickMovement = stickRange * (Screen.width + Screen.height) / 100;
        joyStickRectTransform = joyStick.GetComponent<RectTransform>();

        //ジョイスティック非表示
        JoyStickDisplay(false);
    }

    //ジョイスティックの表示
    private void JoyStickDisplay(bool x)
    {
        backGround.SetActive(x);
        joyStick.SetActive(x);
    }

    //ジョイスティックの動き
    public void Move(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;

        //ジョイスティックと入力位置の差を格納
        float x = backGround.transform.position.x - pointer.position.x;
        float y = backGround.transform.position.y - pointer.position.y;

        angle = Mathf.Atan2(y, x);

        if(Vector2.Distance(backGround.transform.position,pointer.position) > stickMovement)
        {
            y = stickMovement * Mathf.Sin(angle);
            x = stickMovement * Mathf.Cos(angle);
        }

        //プレイヤーを動かす値を格納
        joyStickPosX = -x / stickMovement;
        joyStickPosY = -y / stickMovement;

        joyStick.transform.position = new Vector2(backGround.transform.position.x - x, backGround.transform.position.y - y);
    }

    //入力中に呼ぶ関数
    public void PointerDown(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;
        JoyStickDisplay(true);
        backGround.transform.position = pointer.position;
    }

    //指を離した瞬間に呼ぶ関数
    public void PointerUp(BaseEventData data)
    {
        //ジョイスティックのポジション初期化関数を呼ぶ
        PositionInitialization();

        JoyStickDisplay(false);   
    }

    //ジョイスティックのポジション初期化
    public void PositionInitialization()
    {
        joyStickRectTransform.anchoredPosition = Vector2.zero;
        joyStickPosX = 0;
        joyStickPosY = 0;
    }
}

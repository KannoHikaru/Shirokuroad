using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMove : MonoBehaviour
{
    //�X�e�B�b�N�i�[
    public GameObject joyStick;
    //�W���C�X�e�B�b�N�L�����o�X�̃|�W�V����
    private RectTransform joyStickRectTransform;
    //�W���C�X�e�B�b�N�̌��̂��
    public GameObject backGround;
    //�X�e�B�b�N��������͈�
    public int stickRange = 3;
    //���ۂɓ����l
    private int stickMovement = 0;

    public static float joyStickPosX;
    public static float joyStickPosY;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        //�����ݒ�
        Initialization();
        angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialization()
    {
        //�Ⴄ��ʃT�C�Y�ł������悤�ȋ����ɂ��邽��
        stickMovement = stickRange * (Screen.width + Screen.height) / 100;
        joyStickRectTransform = joyStick.GetComponent<RectTransform>();

        //�W���C�X�e�B�b�N��\��
        JoyStickDisplay(false);
    }

    //�W���C�X�e�B�b�N�̕\��
    private void JoyStickDisplay(bool x)
    {
        backGround.SetActive(x);
        joyStick.SetActive(x);
    }

    //�W���C�X�e�B�b�N�̓���
    public void Move(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;

        //�W���C�X�e�B�b�N�Ɠ��͈ʒu�̍����i�[
        float x = backGround.transform.position.x - pointer.position.x;
        float y = backGround.transform.position.y - pointer.position.y;

        angle = Mathf.Atan2(y, x);

        if(Vector2.Distance(backGround.transform.position,pointer.position) > stickMovement)
        {
            y = stickMovement * Mathf.Sin(angle);
            x = stickMovement * Mathf.Cos(angle);
        }

        //�v���C���[�𓮂����l���i�[
        joyStickPosX = -x / stickMovement;
        joyStickPosY = -y / stickMovement;

        joyStick.transform.position = new Vector2(backGround.transform.position.x - x, backGround.transform.position.y - y);
    }

    //���͒��ɌĂԊ֐�
    public void PointerDown(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;
        JoyStickDisplay(true);
        backGround.transform.position = pointer.position;
    }

    //�w�𗣂����u�ԂɌĂԊ֐�
    public void PointerUp(BaseEventData data)
    {
        //�W���C�X�e�B�b�N�̃|�W�V�����������֐����Ă�
        PositionInitialization();

        JoyStickDisplay(false);   
    }

    //�W���C�X�e�B�b�N�̃|�W�V����������
    public void PositionInitialization()
    {
        joyStickRectTransform.anchoredPosition = Vector2.zero;
        joyStickPosX = 0;
        joyStickPosY = 0;
    }
}

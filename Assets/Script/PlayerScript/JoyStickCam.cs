using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickCam : MonoBehaviour
{
    //�J�����̊��x
    public float aimSensitivity = 10;
    //�X�e�B�b�N�̓�����
    private int stickMovement = 0;
    //�������ׂ�����X,Y
    public float positionX, positionY;
    //�p�x����
    public float viewPointValue = 0;
    //��r�p�Ɉꎞ�I�ɐ��l���i�[����X,Y
    private float tempPosX = 0,tempPosY = 0;
    //Player�Ɍ����Ăق�����]���i�[����X,Y
    public static float rotX = 0, rotY = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        stickMovement = 3 * (Screen.width + Screen.height) / 100;
    }


    //�E��ʂ��h���b�O���Ă���Ƃ��ɌĂԊ֐�
    public void Move(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;

        //�h���b�O���ꂽ���l���i�[����
        positionX = pointer.position.x / stickMovement;
        positionY = pointer.position.y / stickMovement;

        //���x����
        positionX *= aimSensitivity;
        positionY *= aimSensitivity;

        //�֐����Ă�
        Rotation();

    }

    public void Rotation()
    {
        if(positionX != tempPosX)
        {
            if(tempPosX == 0)
            {
                tempPosX = positionX;
            }

            if(positionX == 0)
            {
                tempPosX = 0;
            }

            rotX -= (tempPosX - positionX);

            if(rotX > 360)
            {
                rotX -= 360;
            }

            if(rotX < -360)
            {
                rotX += 360;
            }

            tempPosX = positionX;
        }

        if (positionY != tempPosY)
        {
            if (tempPosY == 0)
            {
                tempPosY = positionY;
            }

            if (positionY == 0)
            {
                tempPosY = 0;
            }

            rotY += (tempPosY - positionY);

            if (rotY > viewPointValue)
            {
                rotY = viewPointValue;
            }

            if (rotY < viewPointValue)
            {
                rotY = viewPointValue;
            }

            tempPosY = positionY;
        }
    }

    //�E��ʂ���w�𗣂����Ƃ��ɌĂԊ֐�
    public void PointerUp(BaseEventData data)
    {
        //�|�W�V�����������֐�
        PositionInitialization();
        //��]
        Rotation();
    }

    public void PositionInitialization()
    {
        positionX = 0;
        positionY = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool floorFlag;
    public GameObject[] whiteWorldObjects;
    public GameObject[] blackWorldObjects;
    public GameObject playerCamera;

    MeshRenderer floorMr;
    public static GameManager instance = null;
    // Start is called before the first frame update

    private void Awake() //startÇÊÇËêÊÇ…åƒÇŒÇÍÇÈÇΩÇﬂÅAñúÇ™àÍstartä÷
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        floorFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        TransparentChangeStage();
       
    }

    /*private void ChangehideObjects()
    {

        if (Input.GetKeyDown(KeyCode.C) && floorFlag == true)
        {
            floorFlag = false;
        }else if(Input.GetKeyDown(KeyCode.C) && floorFlag == false)
        {
            floorFlag = true;
        }

        if (floorFlag)
        {
            foreach (GameObject floor in whitehideObjects)
            {
                floor.SetActive(true);
            }

            foreach (GameObject floor in blackhideObjects)
            {
                floor.SetActive(false);
            }

            playerCamera.GetComponent<UnityEngine.Camera>().backgroundColor = Color.black;

        }
        else
        {
            foreach (GameObject floor in blackhideObjects)
            {
                floor.SetActive(true);
            }

            foreach (GameObject floor in whitehideObjects)
            {
                floor.SetActive(false);
            }

            playerCamera.GetComponent<UnityEngine.Camera>().backgroundColor = Color.white;
        }

        
    }*/

    private void TransparentChangeStage()
    {
        if (Input.GetKeyDown(KeyCode.C) && floorFlag == true)
        {
            floorFlag = false;
        }
        else if (Input.GetKeyDown(KeyCode.C) && floorFlag == false)
        {
            floorFlag = true;
        }

        if (floorFlag)
        {
            foreach (GameObject floor in whiteWorldObjects)
            {
                floorMr = floor.GetComponent<MeshRenderer>();
                floorMr.material.color = new Color(0, 0, 0, 0.0f);
            }

            foreach (GameObject floor in blackWorldObjects)
            {
                floorMr = floor.GetComponent<MeshRenderer>();
                floorMr.material.color = new Color(255.0f, 255.0f, 255.0f, 1.0f);
            }

            playerCamera.GetComponent<UnityEngine.Camera>().backgroundColor = Color.black;

        }
        else
        {
            foreach (GameObject floor in whiteWorldObjects)
            {
                floorMr = floor.GetComponent<MeshRenderer>();
                floorMr.material.color = new Color(0, 0, 0, 1.0f);
            }

            foreach (GameObject floor in blackWorldObjects)
            {
                floorMr = floor.GetComponent<MeshRenderer>();
                floorMr.material.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
            }

            playerCamera.GetComponent<UnityEngine.Camera>().backgroundColor = Color.white;
        }
    }

}

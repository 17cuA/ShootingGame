using UnityEngine;
using TextDisplay;
using System.Collections.Generic;


public class CanvasManager : MonoBehaviour
{
    //public Numeric_Display Score_Display{private set; get;}
    public GameObject warningObj{ private set; get;}
    public Character_Display Score_Display{private set; get;}
    public List<GameObject> Default_UI {private set;get; }

    // Start is called before the first frame update
    void Start()
    {
        //Score_Display = transform.GetChild(0).GetComponent<Numeric_Display>();
        //Score_Display.Digits_Preference(10);
        Default_UI = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Default_UI.Add(transform.GetChild(i).gameObject);
        }

        Score_Display = transform.GetChild(0).GetComponent<Character_Display>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Game_Master.MY.Management_In_Stage)
        {
            case Game_Master.CONFIGURATION_IN_STAGE.eNORMAL:
                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eBOSS_CUT_IN:
                Boss_Cut_In_Updata();
                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eCLEAR:
                break;
            default:
                break;
        }
    }

    public void Boss_Cut_In_Updata()
    {
        if(warningObj == null)
        {
            warningObj = new GameObject();
            warningObj.AddComponent<Warning_Ui_Display>();
            warningObj.transform.parent = transform;
        }
    }
}

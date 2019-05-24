using UnityEngine;
using TextDisplay;
using System.Collections.Generic;


public class CanvasManager : MonoBehaviour
{
    //public Numeric_Display Score_Display{private set; get;}
	public Character_Display Score_Display { private set; get; }
    public GameObject warningObj{ private set; get;}
    public List<GameObject> Canvas_Children {private set;get; }

    // Start is called before the first frame update
    void Start()
    {
		//Score_Display = transform.GetChild(0).GetComponent<Numeric_Display>();
		//Score_Display.Digits_Preference(10);
		Score_Display = new Character_Display(10, "morooka/SS",transform,new Vector3(0.0f,480.0f,0.0f));
		Score_Display.Character_Preference("0000000000");
        Score_Display.Size_Change(new Vector3(0.5f,0.5f,0.5f));
        Canvas_Children = new List<GameObject>();
        for(int i = 0; i < transform.childCount; i++)
        {
            Canvas_Children.Add(transform.GetChild(i).gameObject);
        }
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

﻿using System.Collections;
//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/24
//----------------------------------------------------------------------------------------------
// Score の文字の制御
//----------------------------------------------------------------------------------------------
// 2019/05/24：スコアを表示するだけの挙動
//----------------------------------------------------------------------------------------------
using UnityEngine;
using TextDisplay;

public class Score_Display : MonoBehaviour
{
    //---------------------------------------------------------------------------
    // Unity 側調節よう変数
    //---------------------------------------------------------------------------
    [SerializeField]
    [Header("読み込みたいフォントの場所")]
    private string font_path;
    [SerializeField]
    [Header("フォントのサイズ")]
    private float font_size;
    [SerializeField]
    [Header("表示位置")]
    private Vector3 position;
    //---------------------------------------------------------------------------

    public Character_Display Object_To_Display{private set; get;}

    void Start()
    {
        Object_To_Display = new Character_Display(10, font_path, gameObject, position);
        Object_To_Display.Character_Preference("0000000000");
        Object_To_Display.Size_Change(new Vector3(font_size, font_size, font_size));
    }

    void Update()
    {
        switch (Game_Master.MY.Management_In_Stage)
        {
            case Game_Master.CONFIGURATION_IN_STAGE.eNORMAL:
                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eBOSS_CUT_IN:
                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eBOSS_BUTLE:
                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eCLEAR:
                break;
            default:
                break;
        }
    }

    public void Score_Addition(uint add)
    {
        Object_To_Display.Character_Preference(add.ToString("D10"));
    }
}

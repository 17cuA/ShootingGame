using System.Collections;
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

public class Scoer_Display : MonoBehaviour
{
    public Character_Display Score_Display{private set; get;}

    void Start()
    {
        Score_Display = new Character_Display(10, "morooka/SS", gameObject, new Vector3(0.0f, 480.0f, 0.0f));
        Score_Display.Character_Preference("0000000000");
        Score_Display.Size_Change(new Vector3(0.5f, 0.5f, 0.5f));
    }

    void Update()
    {
        
    }
}

using System.Collections;
//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/24
//----------------------------------------------------------------------------------------------
// Score の文字の制御
//----------------------------------------------------------------------------------------------
// 2019/05/24：スコアを表示するだけの挙動
// 2019/05/27：表示する数値を受け取る関数追加
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

    /// <summary>
    /// 表示数値の設定
    /// </summary>
    /// <param name="number_to_display"> 数値 </param>
    public void Display_Number_Preference(uint number_to_display)
    {
        Object_To_Display.Character_Preference(number_to_display.ToString("D10"));
    }
}

//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/20
//----------------------------------------------------------------------------------------------
// 数字の表示
//----------------------------------------------------------------------------------------------
// 2019/05/20：フォントを使わない数字の表示、表示数の加算
//----------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

public class Numeric_Display : MonoBehaviour
{ 
    private Sprite[] character_look{set;get; }              // スプライトの保存
    private GameObject[] Each_Digits_Object{set;get; }      // 各桁のオブジェクト
    public uint Digits{ private set; get;}                  // 桁数
    public byte[] Each_Save{private set; get;}              // スコア
    public Image[] Character_Render{set; get;}              // 表示

    /// <summary>
    /// 取得スコアの加算
    /// </summary>
    /// <param name="addition"> 加算数 </param>
    public void Score_Addition(uint addition)
    {
        uint confirmation_digits = 10;      // 確認桁数

        // 各桁の設定の繰り返し
        for (byte b = 0; b < Each_Save.Length; b++)
        {
            // 下一桁加算
            Each_Save[b] += (byte)(addition % confirmation_digits);
            // 桁を左にずらす
            addition /= confirmation_digits;
            // 配列の要素の中が10以上のとき
            if (Each_Save[b] >= 10)
            {
                //繰り上げ作業
                Each_Save[b] -= 10;
                if (b != Each_Save.Length - 1)
                {
                    Each_Save[b + 1] += 1;
                }
            }

            // 表示数字の切り替え
            Character_Render[b].sprite = character_look[Each_Save[b]];
        }
    }

    /// <summary>
    /// 桁の設定
    /// </summary>
    /// <param name="get_digits"> 桁数 </param>
    public void Digits_Preference(uint get_digits)
    {
        // 数字の絵のロード
        character_look = Resources.LoadAll<Sprite>("morooka/isyuuwohanatikuru");

        Digits = get_digits;
        Each_Save = new byte[Digits];
        Character_Render = new Image[Digits];
        Each_Digits_Object = new GameObject[Digits];        // 

        Vector3 temp = transform.position;

        for (byte i = 0; i < Digits; i++)
        {
            Each_Digits_Object[i] = new GameObject();
            Each_Digits_Object[i].AddComponent<Image>();
            Character_Render[i] = Each_Digits_Object[i].GetComponent<Image>();
            Character_Render[i].sprite = character_look[0];
            Each_Digits_Object[i].transform.position = temp;
            Each_Digits_Object[i].transform.parent = transform;
            temp.x -= 80.0f;
        }
    }
}

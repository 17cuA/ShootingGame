using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDis : MonoBehaviour
{ 
    private Sprite[] Character_Picture{set;get; }               // スプライトの保存
    private SpriteRenderer[] Character_Render{ set; get;}       // レンダー情報の保存
    private GameObject[] Digit_Numbers{set;get; }               // 各桁の数の変数
    public int Digits{ private set; get;}                       // 桁数
    public byte[] Display_Score{private set; get;}              // スコア

    // Start is called before the first frame update
    void Start()
    {
        Digits = 10;
        Display_Score = new byte[Digits];
        Character_Render = new SpriteRenderer[Digits];
        Digit_Numbers = new GameObject[Digits];

        Character_Picture = Resources.LoadAll<Sprite>("morooka/Score_Number");

        Character_Generation();
    }

    // Update is called once per frame
    void Update()
    {
        var jjjj = 1;
        Score_Addition((uint)jjjj);

        for(byte i = 0;i < Character_Render.Length; i++)
        {
            Character_Render[i].sprite =  Character_Picture[Display_Score[i]];
        }
    }

    /// <summary>
    /// 取得スコアの加算
    /// </summary>
    /// <param name="addition"> 加算数 </param>
    public void Score_Addition(uint addition)
    {
        uint confirmation_digits = 10;      // 確認桁数

        // 各桁確認の繰り返し
        for (byte b = 0; b < Display_Score.Length; b++)
        {
            Display_Score[b] += (byte)(addition % confirmation_digits);
            addition /= confirmation_digits;
            if (Display_Score[b] >= 10)
            {
                Display_Score[b] -= 10;
                if (b != Display_Score.Length - 1)
                {
                    Display_Score[b + 1] += 1;
                }
            }
        }
    }

    /// <summary>
    /// 文字の生成
    /// </summary>
    private void Character_Generation()
    {
        Vector3 temp = transform.position;

        for (byte i = 0; i < Digits; i++)
        {
            Digit_Numbers[i] = new GameObject();
            Digit_Numbers[i].AddComponent<SpriteRenderer>();
            Character_Render[i] = Digit_Numbers[i].GetComponent<SpriteRenderer>();
            Character_Render[i].sprite = Character_Picture[9];
            Digit_Numbers[i].transform.position = temp;
            temp.x -= 2.5f;
            Digit_Numbers[i].transform.parent = transform;
        }
    }
}

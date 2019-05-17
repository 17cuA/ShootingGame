using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDis : MonoBehaviour
{ 
    private Sprite[] sprites;                            // スプライトの保存
    private SpriteRenderer[] renderers;                  // レンダー情報の保存
    private GameObject[] digit_numbers;                  // 各桁の数の変数

    public int digits{ private set; get;}       // 桁数
    public byte[] Display_Score{private set; get;}      // スコア

    // Start is called before the first frame update
    void Start()
    {
        Display_Score = new byte[digits];
        renderers = new SpriteRenderer[digits];
        digit_numbers = new GameObject[digits];

        for(byte i = 0; i < digits;i++)
        {
            
            renderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
            renderers[i].sprite = sprites[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        var jjjj = 1;
        Score_Addition((uint)jjjj);

        for(byte i = 0;i < renderers.Length; i++)
        {
            renderers[i].sprite =  sprites[Display_Score[i]];
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

}

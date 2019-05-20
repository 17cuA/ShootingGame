//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/20
//----------------------------------------------------------------------------------------------
// 文字の表示
//----------------------------------------------------------------------------------------------
// 2019/05/20：0～9,A～Zのフォントを使わない字の表示
//----------------------------------------------------------------------------------------------
//
// この形に並べろ
// ┏━┳━┳━┳━┳━┳━┳━┳━┳━┳━┓
// ┃０┃１┃２┃３┃４┃５┃６┃７┃８┃９┃
// ┣━╋━╋━╋━╋━╋━╋━╋━╋━╋━┫
// ┃Ａ┃Ｂ┃Ｃ┃Ｄ┃Ｅ┃Ｆ┃Ｇ┃Ｈ┃Ｉ┃Ｊ┃
// ┣━╋━╋━╋━╋━╋━╋━╋━╋━╋━┫
// ┃Ｋ┃Ｌ┃Ｍ┃Ｎ┃Ｏ┃Ｐ┃Ｑ┃Ｒ┃Ｓ┃Ｔ┃
// ┣━╋━╋━╋━╋━╋━╋━╋━╋━╋━┫
// ┃Ｕ┃Ｖ┃Ｗ┃Ｘ┃Ｙ┃Ｚ┃  ┃  ┃  ┃  ┃
// ┗━┻━━━┻━┻━┻━┻━┻━┻━┻━┛
// 

using UnityEngine;
using UnityEngine.UI;

public class Character_Display : MonoBehaviour
{
    //public string Chara_Data { get;private set;}     // 文字入れ
    private Sprite[] Look { set; get;}
    public GameObject[] obj {set;get;}
    public Image[] Display_Characters {private set; get;}

    int n;

    private void Start()
    {
        Look_Load("morooka/SS");
    }

    private void Update()
    {
        Character_Preference("02135430K");
    }


    /// <summary>
    /// 見た目のロード
    /// </summary>
    /// <param name="resource_path"> リソース内の Sprite のパス </param>
    public void Look_Load(string resource_path)
    {
        Look = Resources.LoadAll<Sprite>(resource_path);
    }

    /// <summary>
    /// 文字の設定
    /// </summary>
    /// <param name="s"> 表示する文字列 </param>
    public void Character_Preference(string s )
    {
        // 初期のとき、文字数が違うとき
        if (obj == null || obj.Length != s.Length)
        {
            obj = new GameObject[s.Length];
            Display_Characters = new Image[s.Length];
            Vector2 posTemp = transform.position;
            for (int i = 0; i < s.Length; i++)
            {
                obj[i] = new GameObject();
                obj[i].transform.parent = transform;
                Display_Characters[i] = obj[i].AddComponent<Image>();
                Display_Characters[i].sprite = Look[character_search(s[i])];

                obj[i].transform.position = posTemp;
                posTemp.x -= 80.0f;
            }
        }
        else if(obj != null || obj.Length == s.Length)
        {
            for (int i = 0; i < s.Length; i++)
            {
                Display_Characters[i].sprite = Look[character_search(s[i])];
            }

        }
    }

    /// <summary>
    /// 文字検索
    /// </summary>
    /// <param name="search_c"> 1文字 </param>
    /// <returns></returns>
    public byte character_search(char search_c)
    {
        switch(search_c)
        {
            case '0':
                return 0;

            case '1':
                return 1;

            case '2':
                return 2;

            case '3':
                return 3;

            case '4':
                return 4;

            case '5':
                return 5;

            case '6':
                return 6;

            case '7':
                return 7;

            case '8':
                return 8;

            case '9':
                return 9;

            case 'A':
                return 10;

            case 'B':
                return 11;

            case 'C':
                return 12;

            case 'D':
                return 13;

            case 'E':
                return 14;

            case 'F':
                return 15;

            case 'G':
                return 16;

            case 'H':
                return 17;

            case 'I':
                return 18;

            case 'J':
                return 19;

            case 'K':
                return 20;

            case 'L':
                return 21;
            case 'M':
                return 22;
            case 'N':
                return 23;
            case 'O':
                return 24;
            case 'P':
                return 25;
            case 'Q':
                return 26;
            case 'R':
                return 27;
            case 'S':
                return 28;
            case 'T':
                return 29;
            case 'U':
                return 30;
            case 'V':
                return 31;
            case 'W':
                return 32;
            case 'X':
                return 33;
            case 'Y':
                return 34;
            case 'Z':
                return 35;
            default:
                return 39;
        }
    }
}

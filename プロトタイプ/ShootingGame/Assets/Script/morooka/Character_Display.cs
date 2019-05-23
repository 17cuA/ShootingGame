//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/20
//----------------------------------------------------------------------------------------------
// 文字の表示
//----------------------------------------------------------------------------------------------
// 2019/05/20：0～9,A～Zのフォントを使わない字の表示
// 2019/05/23：文字数の固定、固定された文字数以上、未満はエラー
//----------------------------------------------------------------------------------------------
//
// 私を読んで
//
// 画像準備　編
// 1.デザイナーにこの形で画像データを並べて作ってもらって！
//	 ┏━┳━┳━┳━┳━┳━┳━┳━┳━┳━┓
//	 ┃０┃１┃２┃３┃４┃５┃６┃７┃８┃９┃
//	 ┣━╋━╋━╋━╋━╋━╋━╋━╋━╋━┫
//	 ┃Ａ┃Ｂ┃Ｃ┃Ｄ┃Ｅ┃Ｆ┃Ｇ┃Ｈ┃Ｉ┃Ｊ┃
//	 ┣━╋━╋━╋━╋━╋━╋━╋━╋━╋━┫
//	 ┃Ｋ┃Ｌ┃Ｍ┃Ｎ┃Ｏ┃Ｐ┃Ｑ┃Ｒ┃Ｓ┃Ｔ┃
//	 ┣━╋━╋━╋━╋━╋━╋━╋━╋━╋━┫
//	 ┃Ｕ┃Ｖ┃Ｗ┃Ｘ┃Ｙ┃Ｚ┃  ┃  ┃  ┃  ┃
//	 ┗━┻━━━┻━┻━┻━┻━┻━┻━┻━┛
// 
// 2.読み込みたい画像の Inspector にある Texture Type を Sprite(2D and UI)に変更
// 
// 3.いつも通り Sprite Editor で任意の大きさに画像を切る
//
// 3.5.画像データは Asset の中の Resource の中に保存
//
// プログラム　編
// 4.Characters_Count に文字数指定
//
// 5.

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Character_Display : MonoBehaviour
{
	public int Characters_Count { set; get; }						// 文字数の保存場所
	public string Resource_Path { private set; get; }				// 画像のパス
	private Sprite[] Look { set; get;}								// PNGでできたフォントデータの保存場所
    public List<GameObject> obj {private set; get; }				// 表示用の一文字分の object
	public List<Image> Display_Characters {private set; get;}		// 表示用の一文字分の Image コンポーネント

	/// <summary>
	/// コンストラクタコンストラクタ
	/// </summary>
	/// <param name="n"> 文字数 </param>
	/// <param name="s"> 文字用画像の保存場所 </param>
	Character_Display(int n = 0, string s = "")
	{
		Characters_Count = n;
		obj = new List<GameObject>();
		Display_Characters = new List<Image>();

		if (s != "")
		{
			Look_Load(s);
		}
	}

	/// <summary>
	/// 見た目のロード
	/// </summary>
	/// <param name="resource_path"> リソース内の Sprite のパス </param>
	public void Look_Load(string path)
    {
		Resource_Path = path;
        Look = Resources.LoadAll<Sprite>(Resource_Path);
    }

    /// <summary>
    /// 文字の設定
    /// </summary>
    /// <param name="s"> 表示する文字列 </param>
    public void Character_Preference(string s )
    {
		if (s.Length == Characters_Count)
		{
			// 初期のとき、文字数が違うとき
			if (obj.Count == 0)
			{
				Vector2 posTemp = transform.position;
				for (int i = 0; i < Characters_Count; i++)
				{
					obj.Add(new GameObject());
					obj[i].transform.parent = transform;
					obj[i].AddComponent<Image>();
					Display_Characters.Add(obj[i].GetComponent<Image>());
					obj[i].transform.position = posTemp;
					posTemp.x += 100.0f;
				}
			}
			else if (obj.Count > 0)
			{
				for (int i = 0; i < s.Length; i++)
				{
					Display_Characters[i].sprite = Look[character_search(s[i])];
				}
			}

		}
		else if(s.Length > Characters_Count)
		{
			Debug.LogError("設定文字数以上");
		}
		else if(s.Length < Characters_Count)
		{
			Debug.LogError("設定文字数未満");
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

	private void Reset()
	{
		// Image コンポーネントの情報削除
		Display_Characters.Clear();

		// object の削除
		foreach (GameObject obj1 in obj)
		{
			Destroy(obj1);
		}
		obj.Clear();
	}
}


//----------------------------------------------------------
//　作成日　2018/11/09
//　作成者　諸岡勇樹
//----------------------------------------------------------
//　ランキング関連
//----------------------------------------------------------
//　作成時　2018/11/09　ランキングの表示
//　　追加　2018/12/11　RankingCarry クラスから情報をもらう
//　　追加　2018/12/11　ディスプレイサイズの設定
//　　変更　2019/01/19　背景の変更
//----------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TextDisplay;

public class RankingDisplay : MonoBehaviour
{
	private Ranking_Strage.RankingInformation[] Rankings_Dis { get; set; }
	public Character_Display[] Object_To_Display { private set; get; }

	public Vector3[] Rank_Pos { get; set; }

	private GameObject[] Rank_Parent { get; set; }

	private float Font_Size { get; set; }

	public void shoki()
	{
			Rankings_Dis = Ranking_Strage.Strage;

			Rank_Parent = new GameObject[Ranking_Strage.Max_num];
			Object_To_Display = new Character_Display[Ranking_Strage.Max_num];
			Rank_Pos = new Vector3[Ranking_Strage.Max_num];

			float y_pos = 400.0f;
		Font_Size = 1.0f / 4.0f;

			for (int i = 0; i < Object_To_Display.Length; i++)
			{
				Rank_Pos[i].y = y_pos;
				Rank_Pos[i].x = 1920.0f / 2.0f;
				y_pos -= 100.0f /4.0f;
				
				int ranking_num = i + 1;
				string s_temp = ranking_num.ToString().PadLeft(2) + "___" + Rankings_Dis[i].name + "__" + Rankings_Dis[i].score.ToString("D10");
				Rank_Parent[i] = new GameObject();
				Rank_Parent[i].transform.parent = transform;
				Rank_Parent[i].name = "Rank" + ranking_num.ToString();
				Object_To_Display[i] = new Character_Display(s_temp.Length, "morooka/SS", Rank_Parent[i], Rank_Pos[i]);
				Object_To_Display[i].Character_Preference(s_temp);
			Object_To_Display[i].Size_Change(new Vector3(Font_Size, Font_Size, Font_Size));
				Object_To_Display[i].Centering();
			}
	}
}

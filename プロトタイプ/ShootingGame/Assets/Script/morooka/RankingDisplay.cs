﻿
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
	const float kScreenWidth = 3840f;

	[SerializeField, Header("表示文字")] public string string_to_display;

	// ヘッダー用
	private Character_Display header_Display;
	private GameObject header_parent;
	public Vector3 header_pos;
	public float header_size;
	public string String_String { get; set; }

	// 名前入力用
	private Character_Display inputNameDisplay;
	private InputRankingName inputNameClass;
	private Vector2 inputNamePos = Vector2.zero;
	private float inputNameSize = 0f;
	private string previousName = "";
	public bool IsDecision { get { return inputNameClass.IsDecision; } }

	// ランキング用
	private Ranking_Strage.RankingInformation[] DataArray { get; set; }
	public Character_Display[] RankingCharacterDisplay { private set; get; }
	public Vector3[] Rank_Pos { get; set; }
	private GameObject[] Rank_Parent { get; set; }
	private float Font_Size { get; set; }

	public static RankingDisplay instance;

	void Start()
	{
		if (!instance) { instance = FindObjectOfType<RankingDisplay>(); }
	}

	public void Init()
	{
		// ヘッダー表示
		header_parent = new GameObject();
		header_parent.transform.parent = transform;
		String_String = string_to_display;
		header_pos.x = kScreenWidth / 2f / 2f + 10f;
		header_pos.y = 180f;
		header_size = 0.8f;
		header_Display = new Character_Display(String_String.Length, "morooka/SS", header_parent, header_pos);
		header_Display.Character_Preference(string_to_display);
		header_Display.Size_Change(new Vector3(header_size, header_size, header_size));
		header_Display.Centering();

		// 名前入力表示
		inputNameClass = new InputRankingName(Ranking_Strage.kDefaultName);
		GameObject inputNameParent = new GameObject("InputName");
		inputNameParent.transform.parent = transform;
		inputNamePos.x = -330f;
		inputNamePos.y = 20f;
		inputNameSize = 1f;
		inputNameDisplay = new Character_Display(inputNameClass.Name.Length, "morooka/SS", inputNameParent, inputNamePos);
		inputNameDisplay.Character_Preference(inputNameClass.Name);
		inputNameClass.NameImageList = inputNameDisplay.Display_Characters;
		inputNameDisplay.Size_Change(Vector3.one * inputNameSize);
		inputNameDisplay.Centering();

		// ランキング表示
		DataArray = Ranking_Strage.Strage;

		Rank_Parent = new GameObject[Ranking_Strage.Max_num];
		RankingCharacterDisplay = new Character_Display[Ranking_Strage.Max_num];
		Rank_Pos = new Vector3[Ranking_Strage.Max_num];

		float y_pos = 15.0f;
		Font_Size = 1.0f / 2.0f;

		for (int i = 0; i < 5; i++)
		{
			Rank_Pos[i].y = y_pos;
			Rank_Pos[i].x = kScreenWidth / 2.0f / 2.0f;
			y_pos -= 150.0f / 2.0f;

			int ranking_num = i + 1;
			string s_temp = ranking_num.ToString().PadLeft(2) + "___" + DataArray[i].name + "__" + DataArray[i].score.ToString("D10");
			Rank_Parent[i] = new GameObject();
			Rank_Parent[i].transform.parent = transform;
			Rank_Parent[i].name = "Rank" + ranking_num.ToString();
			RankingCharacterDisplay[i] = new Character_Display(s_temp.Length, "morooka/SS", Rank_Parent[i], Rank_Pos[i]);
			RankingCharacterDisplay[i].Character_Preference(s_temp);
			RankingCharacterDisplay[i].Size_Change(new Vector3(Font_Size, Font_Size, Font_Size));
			RankingCharacterDisplay[i].Centering();
		}
	}

	void Update()
	{
		// ゲームクリアシーンでなければ処理しない
		if (Scene_Manager.Manager.Now_Scene != Scene_Manager.SCENE_NAME.eGAME_CLEAR) { return; }
		// 名前変更
		inputNameClass.SelectingName();
		// 表示の更新
		inputNameDisplay.Character_Preference(inputNameClass.Name);
		// 変更された名前を逐一保存していく
		if (previousName != inputNameClass.Name)
		{
			int i = 0;
			for (; i < Ranking_Strage.Strage.Length - 1 && Ranking_Strage.Strage[i].name != previousName && Ranking_Strage.Strage[i].score != Game_Master.display_score; ++i) ;
			Ranking_Strage.Strage[i].name = inputNameClass.Name;
			RankingCharacterDisplay[i].Character_Preference((i + 1).ToString().PadLeft(2) + "___" + Ranking_Strage.Strage[i].name + "__" + Ranking_Strage.Strage[i].score.ToString("D10"));
		}
		previousName = inputNameClass.Name;
	}
}

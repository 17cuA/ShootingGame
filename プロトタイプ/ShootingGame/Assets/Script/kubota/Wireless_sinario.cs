//久保田 達己
//無線信号の文字表示ようスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Wireless_sinario : MonoBehaviour
{
	public enum Stage_No
	{
		Stage1,
		Stage2,
	}

	public static Stage_No Stage;


	public enum Sinario_No
	{
		Curtain_up,                         //開戦時
		First_half_boss_before,             //前半ボス前
		First_falf_boss_after,              //前半ボス後
		Middle_Boss_before,					//一面でいうところの🗿
		Middle_Boss_after,					//中ボス後
		Second_half_boss_before,            //後半ボス前
		Second_half_boss_after,              //後半ボス後
		end
	}




	[System.Serializable]
	public struct Story
	{
		public string name;
		public List<string> Sinario;

		[Header("シナリオを表示しているフレーム")]
		public int SinarioFrame;
		[Header("無線を表示している時間")]
		public float UnShouwTimecnt;
		public Sinario_No No;
		public Story(string Name):this()
		{
			this.name = Name;
		}

	}


	[SerializeField]
	private List<Story> StoryGroups = new List<Story>();
	public Story NowStory;
	//文字の表示に使っている変数たち--------------------------------------------------------
	[SerializeField] Text uiText;                   //uitextへの参照

	[SerializeField]
	[Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;             //１文字の表示にかかる時間
	private string currentText = string.Empty;           //現在の文字列
	private float timeUntilDisplay = 0;                   //表示にかかる時間
	private float timeElapsed = 1;                         //文字列の表示を開始した時間
	private int currentLine = 0;                      //現在の行番号
	private int lastUpdateCharacter = -1;       //表示中の文字数
	private int VoiceNo;                    //無線の声の情報を取得数するための要素数として使用
	//-------------------------------------------------------------------------------
	private int frame = 0;                   // フレーム管理するためのフレームカウント用の変数
	public static bool Is_using_wireless;       //外部scriptから変更するためにつかう
	//-------------------------------------------------------------------------------
	public int No;          //どの無線の状態なのか
	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText
	{
		get { return Time.time > timeElapsed + timeUntilDisplay; }
	}

	public bool isShowOver = false;     //表示が終了したかどうか（明示的に示すため）
	private float unShowTimer;
	[SerializeField] private float unShowTime;

	private int first_start;            //ゲーム開始時からカウントするためのもの
	private Color color;        //文字の色を保存しておくようの変数

	private int frameMax;
	public AudioSource audiosource;         //無線受信時の音などを鳴らすよう
	private int soundcnt;
	int Start_cnt;

	public Sinario_No No_cnt;       //どの無線を鳴らすのかをチェックする用

	public static bool IsFinish_Wireless;	//一番最後の無線が終わったかどうかの判定用


	void Start()
	{
		Game_Master.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.WIRELESS;
		frame = 0;
		first_start = 0;
		uiText.text = "";
		color = uiText.color;
		SetNext_sinario();
		frameMax = 180;
		soundcnt = 0;
		Start_cnt = 0;
		No_cnt = 0;
		IsFinish_Wireless = false;
	}

	void Update()
	{
		//デバック用（珍さんチェック用）ラスボスの無線
		if(Input.GetKeyDown(KeyCode.F))
		{
			No_cnt = (Sinario_No)StoryGroups.Count - 1;
			SetNext_sinario();
			Reset_Value();

		}

		//ゲーム内のモードが無線状態の時
		if (Game_Master.Management_In_Stage == Game_Master.CONFIGURATION_IN_STAGE.WIRELESS)
		{
			uiText.color = color;
			Worddisplay();
		}
		else
		{
			uiText.color = Color.clear;
		}
		if (/*Input.GetKeyDown(KeyCode.Space) || */Is_using_wireless)
		{
			Game_Master.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.WIRELESS;
			first_start = 0;
			frame = (int)(frameMax * 0.67f);
			//No_cnt++;
			Is_using_wireless = false;
		}

	}
	//文字の表示
	void Worddisplay()
	{
		//プレイヤーのアニメーションの行動が終わるまで飛ばす-----------------
		first_start++;
		//受信時の音(初回のみ)
		if (soundcnt == 0 && first_start > 180 && No_cnt == 0 /*&& S_No == 0*/)
		{
			Sound_Active();
			soundcnt = 1;
		}
		//受信時の音（２回目以降）
		else if (soundcnt == 0 && first_start > 45 && No_cnt > 0 /*&& S_No == 0*/)
		{
			Sound_Active();
			soundcnt = 1;
		}
		if (first_start < 120)
		{
			return;
		}
		//-------------------------------------------------------------------------------
		if (isShowOver)
		{
			//既定のシナリオまでだしたら
			if (Time.time >= unShowTimer)
			{
				//無線のモードから通常のモードに治す
				if (currentLine >= NowStory.Sinario.Count)
				{
					No_cnt++;
					SetNext_sinario();
					uiText.text = "";
					Sound_Active();
					Reset_Value();
					if ((int)No_cnt == StoryGroups.Count) IsFinish_Wireless = true;

				}
				isShowOver = false;
			}
			//デバック用
			if (Input.GetKeyDown(KeyCode.Alpha0))
			{
				No_cnt++;
				SetNext_sinario();
				Voice_Manager.VOICE_Obj.Sinario_Stop();
				uiText.text = "";
				Sound_Active();
				Reset_Value();
			}
		}
		else
		{
			if (soundcnt == 1)
			{
				Sound_Active();
			}
			frame++;
			Start_cnt++;
			//文字の表示が完了してるならクリック時に次の行を表示する
			if (IsCompleteDisplayText)
			{
				if (currentLine == NowStory.Sinario.Count)
				{
					isShowOver = true;
					unShowTimer = Time.time + NowStory.UnShouwTimecnt;
				}
				if (currentLine < NowStory.Sinario.Count /*scenarios.Length*/ && frame > frameMax || Input.GetKeyDown(KeyCode.Alpha0) /*|| Input.GetButtonDown("P2_Fire1")*/)
				{
					frame = 0;
					SetNextLine();
				}
			}
			else
			{
				if (currentLine > 0)
				{
					//1面の無線
					if(Stage == Stage_No.Stage1)
					{
						switch (No_cnt)
						{
							case Sinario_No.Curtain_up:
								//開戦時
								Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.First_Wireless[0]);
								break;
							case Sinario_No.First_half_boss_before:
								//前半ボス前
								Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Second_Wireless[0]);
								break;
							case Sinario_No.First_falf_boss_after:
								//前半ボス後
								if (currentLine == 1) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Third_Wireless[0]);
								else if (currentLine == 2) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Third_Wireless[1]);
								break;
							case Sinario_No.Middle_Boss_before:
								//モアイの音声
								if (currentLine == 1) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Forth_Wireless[0]);
								else if (currentLine == 2) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Forth_Wireless[1]);
								break;
							case Sinario_No.Middle_Boss_after:
								if (currentLine == 1) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Fifth_Wireless[0]);
								else if (currentLine == 2) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Fifth_Wireless[1]);
								break;
							case Sinario_No.Second_half_boss_before:
								//後半ボス前
								if (currentLine == 1) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Sixth_Wireless[0]);
								else if (currentLine == 2) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Sixth_Wireless[1]);
								break;
							case Sinario_No.Second_half_boss_after:
								//後半ボス後
								if (currentLine == 1) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Seventh_Wireless[0]);
								else if (currentLine == 2) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Seventh_Wireless[1]);
								break;
							default:
								break;
						}
					}
					//二面の無線
					else
					{
						if (No_cnt == Sinario_No.Second_half_boss_before) SceneManager.LoadScene("End_roll");
						switch (No_cnt)
						{
							case Sinario_No.Curtain_up:
								//2ステージ開始時
								Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.First_Wireless[0]);
								break;
							case Sinario_No.First_half_boss_before:
								//研究所前
								Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Second_Wireless[0]);
								break;
							case Sinario_No.First_falf_boss_after:
								//ブレイン戦１
								if (currentLine == 1) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Third_Wireless[0]);
								else if (currentLine == 2) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Third_Wireless[1]);
								break;
							case Sinario_No.Middle_Boss_before:
								//ブレイン戦後
								if (currentLine == 1) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Forth_Wireless[0]);
								else if (currentLine == 2) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Forth_Wireless[1]);
								else if (currentLine == 3) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Forth_Wireless[2]);

								break;
							case Sinario_No.Middle_Boss_after:
								//ラストブレン撃破後
								if (currentLine == 1) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Fifth_Wireless[0]);
								else if (currentLine == 2) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.Fifth_Wireless[1]);
								break;
							default:
								break;
						}

					}

				}
				if (Input.GetKeyDown(KeyCode.Alpha0) )
				{
					// 完了してないなら文字をすべて表示する
					timeUntilDisplay = 0;
				}
			}
		}
		//経過した　時間が想定表示時間の何％か確認し、表示文字数を出す。
		int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
		//表示文字数が前回の表示文字数と異なるならテキストを更新する。
		if (displayCharacterCount != lastUpdateCharacter)
		{
			uiText.text = currentText.Substring(0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}
	//次に表示する文字を確認
	void SetNextLine()
	{
		if (currentLine < NowStory.Sinario.Count)
		{
			currentText = NowStory.Sinario[currentLine];
		}
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		currentLine++;
		lastUpdateCharacter = -1;
	}

	//どの行を表示するのかを合わせる関数
	void Linesmatch(int i, int m)
	{
		currentLine = i;
		lastUpdateCharacter = -1;
	}
	//表示するシナリオを変更する
	void SetNext_sinario()
	{
		NowStory = Search_Story();
	}
	/// <summary>
	/// 無線が鳴った時に裏で鳴らすやつ
	/// </summary>
	void Sound_Active()
	{
		switch (soundcnt)
		{
			//無線開始時
			case 0:
				audiosource.PlayOneShot(Obj_Storage.Storage_Data.audio_se[23]);
				//次のSEの準備
				audiosource.clip = Obj_Storage.Storage_Data.audio_se[24];
				audiosource.loop = true;
				break;
			//無線中 & 文字表示中
			case 1:
				audiosource.Play();		//無線中はなり続けるためPlay関数を使用している。
				soundcnt = 2;			//次呼ばれたら、無線終了の音を鳴らすようにする
				//一回だけ	
				audiosource.PlayOneShot(Obj_Storage.Storage_Data.audio_se[24]);
				break;
			//無線終了時
			case 2:
				audiosource.Stop();		//ずっとなっていた音を止めてから別の音を出す
				//一回だけ
				audiosource.PlayOneShot(Obj_Storage.Storage_Data.audio_se[25]);
				break;
		}
	}

	/// <summary>
	/// 各値等の初期化
	/// </summary>
	void Reset_Value()
	{
		Game_Master.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.eNORMAL;		//無線モードから通常(敵が出てくるモード)へ変更
		soundcnt = 0;
		Start_cnt = 0;
		currentLine = 0;
		frame = 0;
		uiText.text = "";   //無線の表示で何も移さないようにするため
		unShowTimer = 0;
	}
	/// <summary>
	/// リストの中から選ばれたStoryを取り出す
	/// </summary>
	/// <returns></returns>
	Story Search_Story()
	{
		Story temporary = new Story();
		foreach (Story story in StoryGroups)
		{
			if(No_cnt == story.No)
			{
				temporary = story;
				break;
			}
		}
		return temporary;
	}

	/// <summary>
	/// 最後の無線が終わったか同課の判定用
	/// </summary>
	/// <returns></returns>
	public static bool IsFinishWireless()
	{
		return IsFinish_Wireless;
	}
}

//久保田 達己
//無線信号の文字表示ようスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Wireless_sinario : MonoBehaviour
{
    //文字の表示に使っている変数たち--------------------------------------------------------
    [SerializeField] private string[] scenarios;          // 無線セリフ、上から順に基本流れていく次のセリフにいく(unity側の設定)
	public string[] Curtain_up;
	public string[] First_half_boss_before;
	public string[] First_falf_boss_after;
	public string[] Second_half_boss_before;
	public string[] Second_half_boss_after;

	[SerializeField] Text uiText;					//uitextへの参照
    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;			   //１文字の表示にかかる時間
    private string currentText = string.Empty;			 //現在の文字列
    private float timeUntilDisplay = 0;					  //表示にかかる時間
    private float timeElapsed = 1;						   //文字列の表示を開始した時間
    public int currentLine = 0;						 //現在の行番号
    private int lastUpdateCharacter = -1;		//表示中の文字数
    //-------------------------------------------------------------------------------
    bool one = false;                       // 一度だけ動かすために使う判定型の変数
    public int frame = 0;                   // フレーム管理するためのフレームカウント用の変数
    public bool Is_Display;               //Onになったら文章表示

	public static bool Is_using_wireless;
    //-------------------------------------------------------------------------------
	public enum Sinario_No
	{
		Curtain_up,							//開戦時
		First_half_boss_before,				//前半ボス前
		First_falf_boss_after,				//前半ボス後
		Second_half_boss_before,			//後半ボス前
		Second_half_boss_after				//後半ボス後
	}
	public int No;			//どの無線の状態なのか
    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
		get { return Time.time > timeElapsed + timeUntilDisplay; }
	}

	private bool isShowOver = false;		//表示が終了したかどうか（明示的に示すため）
	private float unShowTimer;				//
	[SerializeField] private float unShowTime;

    private int first_start;            //ゲーム開始時からカウントするためのもの
    void Start()
    {
		Game_Master.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.WIRELESS;
		Is_Display = false;
        frame = 0;
        first_start = 0;
		No = 0;
        uiText.text = "";
		SetNext_sinario();
        //SetNextLine();

	}

    void Update()
    {

		//ゲーム内のモードが無線状態の時
        if(Game_Master.Management_In_Stage == Game_Master.CONFIGURATION_IN_STAGE.WIRELESS)
        {
			uiText.color = Color.white;
            Worddisplay();
        }
		else
		{
			uiText.color = Color.clear;
		}
		Debug.Log(scenarios.Length);

		if (Input.GetKeyDown(KeyCode.Space) || Is_using_wireless)
		{
			Game_Master.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.WIRELESS;
			SetNextLine();
			Is_using_wireless = false;
		}

	}
	//文字の表示
	void Worddisplay()
	{
        //プレイヤーのアニメーションの行動が終わるまで飛ばす-----------------
        first_start++; 
        if(first_start < 120)
		{
            return;
        }
        //-------------------------------------------------------------------------------
		if(isShowOver)
		{
            frame++;
			if (Time.time >= unShowTimer)
			{
				if (currentLine == 2)
				{
					currentLine = 0;
                    frame = 0;
					Game_Master.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.eNORMAL;
				}
				isShowOver = false;
			}
			else
			{
				//プレイヤーが決定ボタンを押したとき
				if (currentLine < scenarios.Length  && frame > 240 || Input.GetButtonDown("Fire1") || Input.GetButtonDown("P2_Fire1"))
				{
                     frame = 0;
					//次の行を準備
					SetNextLine();
				}
			}

		}
		else
		{
             frame++;
			// 文字の表示が完了してるならクリック時に次の行を表示する
			if (IsCompleteDisplayText)
			{
				if (currentLine == 2)
				{
					isShowOver = true;
					unShowTimer = Time.time + unShowTime;
				}
				//if (currentLine == 2)
				//{
				//	currentLine = 0;
				//	Game_Master.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.eNORMAL;
				//}

				if (currentLine < scenarios.Length  && frame > 240 || Input.GetButtonDown("Fire1") || Input.GetButtonDown("P2_Fire1"))
				{
					//sinariocount = currentLine;
                    frame = 0;
					SetNextLine();
				}
			}
			else
			{
				if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("P2_Fire1"))
				{
					Debug.Log("入力処理");
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
        currentText = scenarios[currentLine];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine++;
        lastUpdateCharacter = -1;
		Debug.Log(currentLine);
		if(scenarios.Length == currentLine)
		{
			Debug.Log("シナリオ変更");
			No++;
			SetNext_sinario();
			//Linesmatch(0, 0);
		}
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
		//ゲーム開始から幾つめの文章を出すのか
		switch (No)
		{
			case 0:
				//開戦時
				scenarios = Curtain_up;
				break;
			case 1:
				//前半ボス前
				scenarios = First_half_boss_before;
				break;
			case 2:
				//前半ボス後
				scenarios = First_falf_boss_after;
				break;
			case 3:
				//後半ボス前
				scenarios = Second_half_boss_before;
				break;
			case 4:
				//後半ボス後
				scenarios = Second_half_boss_after;
				break;
			default:
				break;
		}
	}
}

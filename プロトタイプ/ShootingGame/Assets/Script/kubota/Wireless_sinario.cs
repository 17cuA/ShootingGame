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
    private string[] scenarios;          // 無線セリフ、上から順に基本流れていく次のセリフにいく(unity側の設定)
	public string[] Curtain_up;
	public string[] First_half_boss_befor;
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
    public int sinariocount = 0;            // ストーリーの文章がどこまで表示されたかカウント
    bool one = false;                       // 一度だけ動かすために使う判定型の変数
    public int frame = 0;                   // フレーム管理するためのフレームカウント用の変数
    public bool Is_Display;               //Onになったら文章表示
    //-------------------------------------------------------------------------------
	public enum Sinario_No
	{
		Curtain_up,							//開戦時
		First_half_boss_before,				//前半ボス前
		First_falf_boss_after,				//前半ボス後
		Second_half_boss_before,			//後半ボス前
		Second_half_boss_after				//後半ボス後
	}
	public Sinario_No No;			//どの無線の状態なのか
    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.realtimeSinceStartup > timeElapsed + timeUntilDisplay; }
    }

    void Start()
    {
        Is_Display = false;
        frame = 0;
		No = Sinario_No.Curtain_up;
        SetNextLine();
    }

    void Update()
    {
		//ゲーム内のモードが無線状態の時
        if(Game_Master.Management_In_Stage == Game_Master.CONFIGURATION_IN_STAGE.WIRELESS)
        {
            Worddisplay();
        }
    }
	//文字の表示
    void Worddisplay()
    {
        frame++;
        // 文字の表示が完了してるならクリック時に次の行を表示する
        if (IsCompleteDisplayText)
        {
            if (currentLine < scenarios.Length && Input.GetButtonDown("Fire1") && Input.GetButtonDown("P2_Fire1"))
            {
                SetNextLine();
                sinariocount = currentLine - 1;
            }
        }
        else
        {
			if (Input.GetButtonDown("Fire1") && Input.GetButtonDown("P2_Fire1")	)
			{
				// 完了してないなら文字をすべて表示する
				timeUntilDisplay = 0;
			}
		}

		//経過した　時間が想定表示時間の何％か確認し、表示文字数を出す。
        int displayCharacterCount = (int)(Mathf.Clamp01((Time.realtimeSinceStartup - timeElapsed) / timeUntilDisplay) * currentText.Length);
		//法事文字数が前回の表示文字数と異なるならテキストを更新する。
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
        timeElapsed = Time.realtimeSinceStartup;
        currentLine++;
        lastUpdateCharacter = -1;
		if(scenarios.Length == currentLine)
		{
			No = No++;
			SetNext_sinario();
		}
	}

    void Linesmatch(int i, int m)
    {
        currentLine = i;
        lastUpdateCharacter = -1;
        sinariocount = m;
    }
	//表示するシナリオを変更する
	void SetNext_sinario()
	{
		switch (No)
		{
			case Sinario_No.Curtain_up:
				scenarios = Curtain_up;
				currentLine = 0;
				break;
			case Sinario_No.First_half_boss_before:
				scenarios = First_half_boss_befor;
				currentLine = 0;
				break;
			case Sinario_No.First_falf_boss_after:
				scenarios = First_falf_boss_after;
				currentLine = 0;
				break;
			case Sinario_No.Second_half_boss_before:
				scenarios = Second_half_boss_before;
				currentLine = 0;
				break;
			case Sinario_No.Second_half_boss_after:
				scenarios = Second_half_boss_after;
				currentLine = 0;
				break;
			default:
				break;
		}
	}
}

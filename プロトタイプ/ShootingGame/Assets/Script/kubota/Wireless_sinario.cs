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
    public string[] scenarios;          // 無線セリフ、上から順に基本流れていく次のセリフにいく
    [SerializeField] Text uiText;
    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;
    private string currentText = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
    public int currentLine = 0;
    private int lastUpdateCharacter = -1;
    //-------------------------------------------------------------------------------
    public int sinariocount = 0;            // ストーリーの文章がどこまで表示されたかカウント
    public GameObject Ghosttutorial;        // チュートリアルでナレーションをしてくれる重国君の箱
    bool one = false;                       // 一度だけ動かすために使う判定型の変数
    public int frame = 0;                   // フレーム管理するためのフレームカウント用の変数
    public bool can = false;
    public Image Arrow;     // 矢印
    public int frame1 = 0;
    //public GameObject sinario;
    //-------------------------------------------------------------------------------
    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.realtimeSinceStartup > timeElapsed + timeUntilDisplay; }
    }

    void Start()
    {
        SetNextLine();
        Arrow.color = Color.clear;
    }

    void Update()
    {
          
    }
    void Worddisplay()
    {
        // 文字の表示が完了してるならクリック時に次の行を表示する
        if (IsCompleteDisplayText)
        {
            if (currentLine < scenarios.Length && Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                SetNextLine();
                sinariocount = currentLine - 1;
            }
        }
        else
        {
            // 完了してないなら文字をすべて表示する
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                timeUntilDisplay = 0;
            }
        }

        int displayCharacterCount = (int)(Mathf.Clamp01((Time.realtimeSinceStartup - timeElapsed) / timeUntilDisplay) * currentText.Length);
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
    }

    void Linesmatch(int i, int m)
    {
        currentLine = i;
        lastUpdateCharacter = -1;
        sinariocount = m;
    }
}

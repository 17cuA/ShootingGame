/*
 * 20190828 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextDisplay;

/// <summary>
/// ランキングの表示をする
/// </summary>
public class ResultDisplay : MonoBehaviour
{
	// リザルト表示用
	const uint clearbonusValue = 30000;
	private Character_Display result1PScoreTextDisplay;
	private Vector2 result1PScoreTextPosition = new Vector2(-3840f / 2f - 100f * 5f, 1080f / 4f * 3f - 1080f / 2f + 75f);
	private Character_Display result1PScoreDisplay;
	private Vector2 result1PScorePosition = new Vector2(-3840f / 2f - 100f * 7f, 1080f / 4f * 3f - 1080f / 2f - 75f);
	private Character_Display result1PClearbonusTextDisplay;
	private Vector2 result1PClearbonusTextPosition = new Vector2(-3840f / 2f - 100f * 5f, 1080f / 4f * 2f - 1080f / 2f + 75f);
	private Character_Display result1PClearbonusDisplay;
	private Vector2 result1PClearbonusPosition = new Vector2(-3840f / 2f - 100f * 7f, 1080f / 4f * 2f - 1080f / 2f - 75f);
	private Character_Display result1PTotalScoreTextDisplay;
	private Vector2 result1PTotalScoreTextPosition = new Vector2(-3840f / 2f - 100f * 5f, 1080f / 3f * 1f - 1080f / 2f + 75f);
	private Character_Display result1PTotalScoreDisplay;
	private Vector2 result1PTotalScorePosition = new Vector2(-3840f / 2f - 100f * 7f, 1080f / 4f * 1f - 1080f / 2f - 75f);


	void Start()
	{
		// スコアのテキスト
		GameObject result1PScoreTextsParent = new GameObject("1PScoreText");
		result1PScoreTextsParent.transform.parent = transform;
		result1PScoreTextDisplay = new Character_Display("SCORE".Length, "morooka/SS", result1PScoreTextsParent, result1PScoreTextPosition);
		result1PScoreTextDisplay.Character_Preference("SCORE");
		result1PScoreTextDisplay.Size_Change(Vector2.one * 0.75f);
		// スコア
		GameObject result1PScoresParent = new GameObject("1PScore");
		result1PScoresParent.transform.parent = transform;
		result1PScoreDisplay = new Character_Display(10, "morooka/SS", result1PScoresParent, result1PScorePosition);
		result1PScoreDisplay.Character_Preference(Game_Master.display_score_1P.ToString("D10"));
		// クリアボーナスのテキスト
		GameObject result1PClearbonusTextsParent = new GameObject("1PClearbonusText");
		result1PClearbonusTextsParent.transform.parent = transform;
		result1PClearbonusTextDisplay = new Character_Display("CLEAR_BONUS".Length, "morooka/SS", result1PClearbonusTextsParent, result1PClearbonusTextPosition);
		result1PClearbonusTextDisplay.Character_Preference("CLEAR_BONUS");
		result1PClearbonusDisplay.Size_Change(Vector2.one * 0.75f);
		// クリアボーナス
		GameObject result1PClearbonusesParent = new GameObject("1PClearbonus");
		result1PClearbonusesParent.transform.parent = transform;
		result1PClearbonusDisplay = new Character_Display(10, "morooka/SS", result1PClearbonusesParent, result1PClearbonusPosition);
		result1PClearbonusDisplay.Character_Preference(clearbonusValue.ToString("D10"));
		// トータルスコアのテキスト
		GameObject result1PTotalScoreTextsParent = new GameObject("1PTotalScoreText");
		result1PTotalScoreTextsParent.transform.parent = transform;
		result1PTotalScoreTextDisplay = new Character_Display("TOTAL_SCORE".Length, "morooka/SS", result1PTotalScoreTextsParent, result1PTotalScoreTextPosition);
		result1PTotalScoreTextDisplay.Character_Preference("TOTAL_SCORE");
		result1PTotalScoreTextDisplay.Size_Change(Vector2.one * 0.75f);
		// トータルスコア
		uint totalScore = Game_Master.display_score_1P + clearbonusValue;
		GameObject result1PTotalScoresParent = new GameObject("1PTotalScore");
		result1PTotalScoresParent.transform.parent = transform;
		result1PTotalScoreDisplay = new Character_Display(10, "morooka/SS", result1PTotalScoresParent, result1PTotalScorePosition);
		result1PTotalScoreDisplay.Character_Preference(totalScore.ToString("D10"));
	}

	void Update()
	{

	}
}

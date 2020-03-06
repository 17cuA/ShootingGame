/*
 * 20200302 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndrollText : MonoBehaviour
{
	delegate void DisplayProcess();
	// 変数宣言------------------------------------
	[SerializeField, Tooltip("テキストなどを表示するCanvas")] Canvas m_displayCanvas;
	[SerializeField, Tooltip("使用するTextComponent")] Text m_textComponent;
	[SerializeField, TextArea(0, 5), Tooltip("表示するテキストのリスト")] List<string> m_textList = new List<string>();
	[SerializeField, Tooltip("詳細設定")] DetailedParameter m_detailedParameter;
	int m_displayingTextNumber = 0;			// 表示しているテキストの要素番号
	float m_functionTime = 0f;				// 関数内で使用するタイマー
	DisplayProcess m_displayProcess;		// 処理の制御
	/// <summary>
	/// テキストがすべて表示し終わったかどうか
	/// </summary>
	public bool IsEndText
	{
		get { return m_displayingTextNumber >= m_textList.Count; }
	}

	void Start()
	{
		if (m_textList.Count <= 0) { return; }
		if (m_textComponent == null)
		{
			SettingText();
		}
		m_textComponent.text = m_textList[0];
		Color color = m_textComponent.color;
		color.a = 0f;
		m_textComponent.color = color;

		m_displayProcess = FadeInText;
	}

	void Update()
	{
		if (m_textList.Count <= 0) { return; }
		if (Time.time < m_detailedParameter.m_startDelayTime) { return; }
		m_displayProcess?.Invoke();
	}
	/// <summary>
	/// テキストのフェードイン
	/// </summary>
	void FadeInText()
	{
		// alphaを制限をつけて設定された時間で上げる
		Color color = m_textComponent.color;
		color.a = Mathf.Clamp(color.a + 1f / m_detailedParameter.m_fadeTime * Time.deltaTime, 0f, 1f);
		m_textComponent.color = color;
		// はっきり表示されたら待機にうつる
		if (color.a >= 1f)
		{
			m_displayProcess = DisplayText;
		}
		// 入力を受ける状態で入力を受けたらフェードアウトにうつる
		if (m_detailedParameter.m_isSkipInput && Input.anyKeyDown)
		{
			m_displayProcess = FadeoutText;
		}
	}
	/// <summary>
	/// テキストの表示
	/// </summary>
	void DisplayText()
	{
		// 設定した時間待機
		m_functionTime += Time.deltaTime;
		if (m_functionTime > m_detailedParameter.m_visibleTime || (m_detailedParameter.m_isSkipInput && Input.anyKeyDown))
		{
			m_displayProcess = FadeoutText;
			m_functionTime = 0f;
		}
	}
	/// <summary>
	/// テキストのフェードアウト
	/// </summary>
	void FadeoutText()
	{
		// alphaを制限をつけて設定された時間で下げる
		Color color = m_textComponent.color;
		color.a = Mathf.Clamp(color.a - 1f / m_detailedParameter.m_fadeTime * Time.deltaTime, 0f, 1f);
		m_textComponent.color = color;
		// 見えなくなったらそれぞれ処理する
		if (color.a <= 0f)
		{
			++m_displayingTextNumber;
			// 最後のテキストであれば終了
			if (m_displayingTextNumber >= m_textList.Count)
			{
				m_displayProcess = null;
				return;
			}
			// 次のテキストにうつる
			m_textComponent.text = m_textList[m_displayingTextNumber];
			m_displayProcess = WaitInvisibleText;
		}
	}
	/// <summary>
	/// テキストの非表示状態で待機
	/// </summary>
	void WaitInvisibleText()
	{
		m_functionTime += Time.deltaTime;
		if (m_functionTime >= m_detailedParameter.m_invisibleTime)
		{
			m_displayProcess = FadeInText;
			m_functionTime = 0f;
		}
	}
	/// <summary>
	/// 名前の通り
	/// </summary>
	void SettingText()
	{
		GameObject textObject = new GameObject("StandardText");
		textObject.transform.parent = m_displayCanvas.transform;
		m_textComponent = textObject.AddComponent<Text>();
		m_textComponent.fontSize = 150;
		m_textComponent.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
		m_textComponent.rectTransform.localPosition = Vector2.zero;
		m_textComponent.alignment = TextAnchor.MiddleCenter;
		m_textComponent.color = new Color(50f / 255f, 50f / 255f, 50f / 255f);
		m_textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
	}
	#region Botsu
	IEnumerator BotsuDisplayText()
	{
		yield return new WaitForSeconds(m_detailedParameter.m_startDelayTime);
		for (int i = 0; i < m_textList.Count; ++i)
		{
			// テキスト変更
			m_textComponent.text = m_textList[i];
			// m_fadeTimeの時間をかけて表示
			while(m_textComponent.color.a < 1f)
			{
				Color color = m_textComponent.color;
				color.a = Mathf.Clamp(color.a + m_detailedParameter.m_fadeTime * Time.deltaTime, 0f, 1f);
				m_textComponent.color = color;
				yield return new WaitForEndOfFrame();
			}
			// 待機
			yield return new WaitForSeconds(m_detailedParameter.m_visibleTime);
			// m_fadeTimeの時間をかけて非表示
			while (m_textComponent.color.a > 0f)
			{
				Color color = m_textComponent.color;
				color.a = Mathf.Clamp(color.a - m_detailedParameter.m_fadeTime * Time.deltaTime, 0f, 1f);
				m_textComponent.color = color;
				yield return new WaitForEndOfFrame();
			}
			m_displayingTextNumber = i;
		}
	}
#endregion
	[System.Serializable]
	class DetailedParameter
	{
		[Tooltip("遅延時間")] public float m_startDelayTime = 0f;
		[Tooltip("フェードにかける時間")] public float m_fadeTime = 1f;
		[Tooltip("表示する時間")] public float m_visibleTime = 2f;
		[Tooltip("非表示にする時間")] public float m_invisibleTime = 0.5f;
		[Tooltip("入力を受けたらスキップするようにする")] public bool m_isSkipInput = false;
	}

#region Botsu Editor
	//#if UNITY_EDITOR
	//	[CustomEditor(typeof(EndrollText))]
	//	public class EndrollTextEditor : Editor
	//	{
	//		EndrollText endrollTextObject;
	//		void Awake()
	//		{
	//			endrollTextObject = target as EndrollText;
	//		}
	//		public override void OnInspectorGUI()
	//		{
	//			endrollTextObject.m_startDelay = EditorGUILayout.DelayedFloatField("Start Delay", endrollTextObject.m_startDelay);
	//		}
	//	}
	//#endif
#endregion
}

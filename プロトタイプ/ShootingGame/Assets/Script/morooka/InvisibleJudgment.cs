//作成日2019/11/29
// 表示中のオブジェクトのスクリプトの使用不使用を決める
// 作成者:諸岡勇樹
/*
 * 2019/11/29　表示されたら使用、非表示で不使用
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleJudgment : MonoBehaviour
{
	[SerializeField, Tooltip("使いたいスクリプト(character_status)")] private character_status character;
	[SerializeField, Tooltip("使いたいスクリプト(MonoBehaviour)")]	private MonoBehaviour monoBehaviour;

	private void Awake()
	{
		Renderer temp;
		//レンダーなしオブジェクトにレンダー付け
		if (gameObject.GetComponent<Renderer>() == null)
		{
			temp = gameObject.AddComponent<SpriteRenderer>();
		}
		else
		{
			temp = GetComponent<Renderer>();
		}

		if (character != null) character.enabled = temp.isVisible;
		if (monoBehaviour != null) monoBehaviour.enabled = temp.isVisible;

	}

	/// <summary>
	/// 表示されるようになった時の処理
	/// </summary>
	void OnBecameVisible()
	{
		if (character != null) character.enabled = true;
		if (monoBehaviour != null) monoBehaviour.enabled = true;
	}

	/// <summary>
	/// 表示されなくなった時の処理
	/// </summary>
	void OnBecameInvisible()
	{
		if (character != null) character.enabled = false;
		if (monoBehaviour != null) monoBehaviour.enabled = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
/*
 * 注意左と右の日本電子、消えるときは。
 * スケールを0.3/sec程度で消失、Y軸が０へ、X軸は右の画面へ最終的には線になる感じで
 * 消える。日電は逆に左へ行く感じ。
 * 昔のブラウン管みたく、電源を消したときの感じです。スケールとアルファ値で簡単に
 * 演出が出来ると思います。最終的にはシェーダーをコールして、適用するんだけど。
 * 基本となる動きは用意しておくといいです。
 */

public class Title_Manager : MonoBehaviour
{
	public bool play_flag;
	private bool one_;
	public PlayableDirector time_line;

	private void Awake()
	{
		time_line.playOnAwake = false;
	}

	private void Update()
	{
		if(play_flag && !one_)
		{
			time_line.Play();
			one_ = true;
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Power
{
	/// <summary>
	/// アイテムクラス
	/// 必ずこのスクリプトをアイテムオブジェクトにアタッチする
	/// </summary>
	public class Item : MonoBehaviour
	{
		[Header("パワーアップタイプ")]
		public PowerType powerType;
	}
}

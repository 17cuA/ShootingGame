using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	Item_Normal,
	Item_KillAllEnemy,
}
/// <summary>
/// アイテムクラス
/// 必ずこのスクリプトをアイテムオブジェクトにアタッチする
/// </summary>
public class Item : MonoBehaviour
{
	[Header("パワーアップタイプ")]
	public ItemType itemType;
}


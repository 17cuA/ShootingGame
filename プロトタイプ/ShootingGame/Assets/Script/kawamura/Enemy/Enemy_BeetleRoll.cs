//作成者：川村良太
//ビートル敵の回転スクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BeetleRoll : MonoBehaviour
{
	public int rollCnt;							//回転した回数を数える
	public float RotaZ;							//代入する角度の値
	public float RotaZ_Max;					//回転する最大値
	[Header("入力用　角度を変える数値の大きさ設定")]
	public float RotaZ_ChangeValue;     //角度を変える値

	public bool isRoll;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

//ビットン（上のビットン）の斜め撃ちでの角度を変えるスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObliquePosTop : MonoBehaviour
{
	float rotaZ;
	float rotaZ_ChangeValue;

	float rotaZ_Mini;			//開いている角度の最小
	float rotaZ_Max;			//開いている角度の最大
    void Start()
    {
        rotaZ = 15.0f;
		rotaZ_ChangeValue = 10.0f;
		rotaZ_Mini = 10.0f;			
		rotaZ_Max = 25.0f;
    }

    void Update()
    {
		//ビットンの角度を小さくさせる
		if(Input.GetKeyDown(KeyCode.J))
		{
			//角度を変化値分小さく
			rotaZ -= rotaZ_ChangeValue;
			//最小値より小さくなったら
			if(rotaZ < rotaZ_Mini)
			{
				//最小値に戻す
				rotaZ = rotaZ_Mini;
			}
		}
		//ビットンの角度を大きくする
		else if(Input.GetKeyDown(KeyCode.K))
		{
			//角度を変化値分大きく
			rotaZ += rotaZ_ChangeValue;
			//最大値より大きくなったら
			if(rotaZ > rotaZ_Max)
			{
				//最大値に戻す
				rotaZ = rotaZ_Max;
			}

		}
		//角度の代入
        transform.rotation=Quaternion.Euler(0,0,rotaZ);
    }
}

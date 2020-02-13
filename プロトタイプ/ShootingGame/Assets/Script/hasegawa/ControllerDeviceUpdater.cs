/*
 * 20200213 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ControllerDeviceを回すためだけのやつ
/// </summary>
public class ControllerDeviceUpdater : MonoBehaviour
{
	void LateUpdate()
	{
		ControllerDevice.Update();
	}
}

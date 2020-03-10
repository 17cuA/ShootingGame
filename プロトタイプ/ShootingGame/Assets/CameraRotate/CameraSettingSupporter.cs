/*
 * 20200303 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]
public class CameraSettingSupporter : MonoBehaviour
{
	CameraRotateAround m_CameraRotateAround;
	void Update()
	{
		// 再生中であれば処理しない
		if (EditorApplication.isPlaying) { return; }
		// コンポーネントの取得、取得できなかった場合は処理しない
		if (m_CameraRotateAround == null)
		{
			m_CameraRotateAround = GetComponent<CameraRotateAround>();
			if (m_CameraRotateAround == null) { return; }
		}
		// カメラの回転
		Vector3 lookVector = m_CameraRotateAround.PointOfGaze.position - transform.position;
		transform.rotation = Quaternion.LookRotation(lookVector, Vector3.up);
		// Rayを表示しない場合は処理を終える
		if (!m_CameraRotateAround.IsVisibleDebugRay) { return; }
		Vector3 axis = Quaternion.Euler(m_CameraRotateAround.Detailed.m_axisAngleX, m_CameraRotateAround.Detailed.m_axisAngleY, m_CameraRotateAround.Detailed.m_axisAngleZ) * Vector3.up;
		if (!m_CameraRotateAround.Detailed.m_isDetailed)
		{
			axis = transform.up;
		}
		Debug.DrawRay(m_CameraRotateAround.PointOfGaze.position, axis * 10f, Color.red);
	}
	void OnGUI()
	{
		if (m_CameraRotateAround == null) { return; }
		if (!m_CameraRotateAround.IsVisibleDebugRay) { return; }
		Vector3 axis = Quaternion.Euler(m_CameraRotateAround.Detailed.m_axisAngleX, m_CameraRotateAround.Detailed.m_axisAngleY, m_CameraRotateAround.Detailed.m_axisAngleZ) * Vector3.up;
		if (!m_CameraRotateAround.Detailed.m_isDetailed)
		{
			axis = transform.up;
		}
		GUI.TextField(new Rect(10f, 10f, 800f, 60f), "axis angle x:" + Mathf.Rad2Deg * axis.x + " y:" + Mathf.Rad2Deg * axis.y + " z:" + Mathf.Rad2Deg * axis.z);
		GUI.skin.textField.fontSize = 35;
	}
}
#endif

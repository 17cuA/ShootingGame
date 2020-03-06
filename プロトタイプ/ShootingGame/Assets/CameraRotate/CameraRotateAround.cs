/*
 * 20200303 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateAround : MonoBehaviour
{
	// 変数宣言-----------------------------------------
	[SerializeField, Tooltip("注視するポイント")] Transform m_pointOfGaze;
	public Transform PointOfGaze { get { return m_pointOfGaze; } }
	[SerializeField, Tooltip("一秒あたりの回転量")] float m_rotateSpeed = 30f;
	[SerializeField, Tooltip("軸を表示するかどうか")] bool m_isVisibleDebugRay = true;
	public bool IsVisibleDebugRay { get { return m_isVisibleDebugRay; } }
	[SerializeField, Tooltip("詳細設定")] DetailedParameter m_detailedParameter;
	public DetailedParameter Detailed { get { return m_detailedParameter; } }
	Vector3 m_rotateAxis = Vector3.up;

	void Start()
	{
		// 初めに注視点に向く
		Vector3 lookVector = m_pointOfGaze.position - transform.position;
		transform.rotation = Quaternion.LookRotation(lookVector, Vector3.up);
		if (!m_detailedParameter.m_isDetailed)
		{
			m_rotateAxis = transform.up;
		}
	}

	void Update()
	{
		// 注視点に向く
		Vector3 lookVector = m_pointOfGaze.position - transform.position;
		transform.rotation = Quaternion.LookRotation(lookVector, Vector3.up);
		// 詳細設定かどうかで回転軸を変え回転させる
		Vector3 axis = m_rotateAxis;
		if (m_detailedParameter.m_isDetailed)
		{
			axis = Quaternion.Euler(m_detailedParameter.m_axisAngleX, m_detailedParameter.m_axisAngleY, m_detailedParameter.m_axisAngleZ) * Vector3.up;
		}
		transform.RotateAround(m_pointOfGaze.position, axis, m_rotateSpeed * Time.deltaTime);
		// 確認用Ray
		if (m_isVisibleDebugRay)
		{
			Debug.DrawRay(m_pointOfGaze.position, axis * 10f, Color.red);
		}
	}
	[System.Serializable]
	public class DetailedParameter
	{
		[Tooltip("詳細設定にするかどうか")] public bool m_isDetailed = false;
		[SerializeField, Range(-90f, 90f), Tooltip("x軸の角度")] public float m_axisAngleX = 0f;
		[SerializeField, Range(-90f, 90f), Tooltip("y軸の角度")] public float m_axisAngleY = 0f;
		[SerializeField, Range(-90f, 90f), Tooltip("z軸の角度")] public float m_axisAngleZ = 0f;
	}
}

﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Line_Beam : MonoBehaviour {

	float range = 57.0f;
    Ray shotRay;
    RaycastHit shotHit;  
    ParticleSystem beamParticle;
    LineRenderer lineRenderer;
	private Renderer Target_Renderer;
	Transform Laser_Size;

	// Use this for initialization
	void Awake () {
        beamParticle = GetComponent<ParticleSystem> ();
        lineRenderer = GetComponent<LineRenderer> ();
        Target_Renderer = GetComponent<Renderer>();
		
	}

    // Update is called once per frame
    void Update ()
	{
		Laser_Size = this.transform;
		if (Input.GetKeyDown(KeyCode.I))
		{
			// ローカル座標を基準にした、サイズを取得
			Vector3 localScale = Laser_Size.localScale;
			localScale.x = 2.0f; // ローカル座標を基準にした、x軸方向へ2倍のサイズ変更
			localScale.y = 2.0f; // ローカル座標を基準にした、y軸方向へ2倍のサイズ変更
			localScale.z = 2.0f; // ローカル座標を基準にした、z軸方向へ2倍のサイズ変更
			Laser_Size.localScale = localScale;
			lineRenderer.startWidth = 2f;
			lineRenderer.endWidth = 2f;
		}
		if (Input.GetKeyDown(KeyCode.U))
		{
			// ローカル座標を基準にした、サイズを取得
			Vector3 localScale = Laser_Size.localScale;
			localScale.x = 1.0f; // ローカル座標を基準にした、x軸方向へ2倍のサイズ変更
			localScale.y = 1.0f; // ローカル座標を基準にした、y軸方向へ2倍のサイズ変更
			localScale.z = 1.0f; // ローカル座標を基準にした、z軸方向へ2倍のサイズ変更
			Laser_Size.localScale = localScale;
			lineRenderer.startWidth = 1f;
			lineRenderer.endWidth = 1f;
		}
		if (Input.GetMouseButton (1))
		{
            shot ();
        }
		else disableEffect();
	}

	private void shot()
	{
		beamParticle.Stop();
		beamParticle.Play();
		lineRenderer.enabled = true;
		lineRenderer.SetPosition(0, transform.position);
		shotRay.origin = transform.position;
		shotRay.direction = transform.forward;

		//int layerMask = 0;
		int layerMask = LayerMask.GetMask("Enemy");
		int Wall_layerMask = LayerMask.GetMask("Wall");

		lineRenderer.SetPosition(1, shotRay.origin + shotRay.direction * range);

		//if (Physics.Raycast(shotRay, out shotHit, range, layerMask))
		//{
		//	Destroy(shotHit.transform.gameObject);
		//}
		if (Physics.Raycast(shotRay, out shotHit, range, Wall_layerMask))
		{
			lineRenderer.SetPosition(1, shotHit.point + shotRay.direction);
		}
		//if (Physics.SphereCast(transform.position, 1, transform.forward, out shotHit, layerMask))
		//{
		//	Destroy(shotHit.transform.gameObject);
		//}
		if (Physics.BoxCast(transform.position, Vector3.one, transform.forward, out shotHit, Quaternion.identity, 100f, layerMask))
		{
			Destroy(shotHit.transform.gameObject);
		}
		Debug.DrawLine(transform.position, shotHit.point, Color.red);
	}

	private void disableEffect()
    {
        beamParticle.Stop();
        lineRenderer.enabled = false;
    }
}
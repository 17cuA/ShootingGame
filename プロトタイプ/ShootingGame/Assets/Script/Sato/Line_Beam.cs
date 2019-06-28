using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Line_Beam : MonoBehaviour {

	float range = 57.0f;	//レーザーの長さ
    Ray shotRay;			//レーザーの判定に使用するray
    RaycastHit shotHit;  //レーザーが当たったかの確認
	ParticleSystem beamParticle;	//パーティクルの格納
    LineRenderer lineRenderer;      //LineRendererの格納
	private Renderer Target_Renderer;	//
	Transform Laser_Size;
	bool isEnable = true;
	float hitstop;
	Collider coll;

	// Use this for initialization
	void Awake () {
        beamParticle = GetComponent<ParticleSystem> ();
        lineRenderer = GetComponent<LineRenderer> ();
        Target_Renderer = GetComponent<Renderer>();
		coll = GetComponent<BoxCollider>();
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

		var radius = transform.lossyScale.x * 0.5f;

		var isHit = Physics.SphereCast(transform.position, radius, transform.forward , out shotHit);

		if (isHit)
		{
			hitstop = shotHit.distance;
			if (Physics.SphereCast(transform.position, radius, transform.forward , out shotHit, LayerMask.GetMask("Wall")))
			{
				hitstop = shotHit.distance;
			}
			if (Physics.SphereCast(shotRay, radius, out shotHit, hitstop, LayerMask.GetMask("Enemy")))
			{
				Destroy(shotHit.transform.gameObject);
			}
		}
	}

	void OnDrawGizmos()
	{
		if (isEnable == false)
			return;

		var radius = transform.lossyScale.x * 0.5f;

		var isHit = Physics.SphereCast(transform.position, radius, transform.forward * 10, out shotHit);
		Gizmos.color = Color.red;
		if (isHit)
		{
			Gizmos.DrawRay(transform.position, transform.forward * shotHit.distance);
			Gizmos.DrawWireSphere(transform.position + transform.forward * (shotHit.distance), radius);
		}
		else
		{
			Gizmos.DrawRay(transform.position, transform.forward * 100);
		}
	}

private void disableEffect()
    {
        beamParticle.Stop();
        lineRenderer.enabled = false;
    }
}
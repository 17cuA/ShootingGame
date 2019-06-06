using UnityEngine;
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
	bool isEnable = true;
	float hitstop=10.0f;

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

		var radius = transform.lossyScale.x * 0.5f;

		var isHit = Physics.SphereCast(transform.position, radius, transform.forward * hitstop, out shotHit);

		if (isHit)
		{
			hitstop = shotHit.distance;
			if (Physics.SphereCast(transform.position, radius, transform.forward * hitstop, out shotHit, Wall_layerMask))
			{
				isHit = Physics.SphereCast(transform.position, radius, transform.forward * hitstop, out shotHit);
				if(Physics.SphereCast(transform.position, radius, transform.forward * hitstop, out shotHit, layerMask))
				{
					Destroy(shotHit.transform.gameObject);
				}
			}
		}


		//lineRenderer.SetPosition(1, shotRay.origin + shotRay.direction * range);

		////if (Physics.Raycast(shotRay, out shotHit, range, layerMask))
		////{
		////	Destroy(shotHit.transform.gameObject);
		////}

		//if (Physics.Raycast(shotRay, out shotHit, range, Wall_layerMask))
		//{
		//	lineRenderer.SetPosition(1, shotHit.point + shotRay.direction);
		//}


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
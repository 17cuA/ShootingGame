using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Line_Beam : MonoBehaviour {

	public float range = 57.0f;	//レーザーの長さ
    Ray shotRay;			//レーザーの判定に使用するray
    RaycastHit shotHit;  //レーザーが当たったかの確認
	ParticleSystem beamParticle;	//パーティクルの格納
    LineRenderer lineRenderer;      //LineRendererの格納
	Transform Laser_Size;　//レーザーの長さを変形
	bool isEnable = true;	//当たり判定の有効化
	float hitstop;	//当たった際の変数
	Collider coll;　//当たり判定

	// Use this for initialization
	void Awake () {
        beamParticle = GetComponent<ParticleSystem> ();
        lineRenderer = GetComponent<LineRenderer> ();
		coll = GetComponent<BoxCollider>();
	}

    // Update is called once per frame
    void Update ()
	{
		Laser_Size = this.transform;

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
			Destroy(shotHit.transform.gameObject);
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
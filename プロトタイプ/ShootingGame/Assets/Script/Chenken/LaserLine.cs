using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class LaserLine : Player_Bullet
{
	public TrailRenderer trailRenderer;

    private void Awake()
    {
        base.shot_speed = 0.8f;
        base.attack_damage = 1;
        base.Travelling_Direction = Vector3.right;

		this.trailRenderer = GetComponent<TrailRenderer>();

    }


	private new void Update()
    {
        if(transform.position.x >= 19.0f || transform.position.x <= -19.0f
			|| transform.position.y >= 5.5f || transform.position.y <= -5.5f)
		{
			this.gameObject.SetActive(false);
		}

        base.Moving_To_Travelling_Direction();

    }

	private new void OnTriggerEnter(Collider col)
	{
		//それぞれのキャラクタの弾が敵とプレイヤーにあたっても消えないようにするための処理
		if ((gameObject.tag == "Enemy_Bullet" && col.gameObject.tag == "Player") || (gameObject.tag == "Player_Bullet" && col.gameObject.tag == "Enemy"))
		{

			this.gameObject.SetActive(false);
			//add:0513_takada 爆発エフェクトのテスト
			//AddExplosionProcess();
			GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
			ParticleSystem particle = effect.GetComponent<ParticleSystem>();
			effect.transform.position = gameObject.transform.position;
			particle.Play();
		}
	}
}

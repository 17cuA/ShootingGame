using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Power;

[RequireComponent(typeof(CapsuleCollider))]
public class LaserLine : Player_Bullet
{
	private TrailRenderer trailRenderer;
	public TrailRenderer TrailRenderer
	{
		get
		{
			return trailRenderer;
		}
	}

	private new Rigidbody rigidbody;
	public bool isPlay1Laser = false;
	public bool isPlay2Laser = false;
	public bool IsPlayer1Laser
	{
		get
		{
			return isPlay1Laser;
		}
		set
		{
			isPlay1Laser = value;
		}
	}

	public bool IsPlayer2Laser
	{
		get
		{
			return isPlay2Laser;
		}
		set
		{
			isPlay2Laser = value;
		}
	}

	public Material redMaterial;
	public Material blueMaterial;
	private void Awake()
    {
		this.trailRenderer = GetComponent<TrailRenderer>();
		this.rigidbody = GetComponent<Rigidbody>();
	}

	private new void Update()
    {
		if(isPlay1Laser)
		{
			if (!transform.GetChild(0).gameObject.activeSelf)
				transform.GetChild(0).gameObject.SetActive(true);
			if (transform.GetChild(1).gameObject.activeSelf)
				transform.GetChild(1).gameObject.SetActive(false);
			if(trailRenderer.material != blueMaterial)
				trailRenderer.material = blueMaterial;
		}

		if (isPlay2Laser)
		{
			if (transform.GetChild(0).gameObject.activeSelf)
				transform.GetChild(0).gameObject.SetActive(false);
			if (!transform.GetChild(1).gameObject.activeSelf)
				transform.GetChild(1).gameObject.SetActive(true);
			if(trailRenderer.material != redMaterial)
				trailRenderer.material = redMaterial;
		}

		if (transform.position.x >= 25.0f || transform.position.x <= -25.0f
		|| transform.position.y >= 8.5f || transform.position.y <= -8.5f)
		{
			gameObject.SetActive(false);
		}

		//rigidbody.velocity = new Vector3(shot_speed * 40, 0, 0);
		base.Moving_To_Travelling_Direction();
	}

    public new void OnTriggerEnter(Collider col)
    {      
		if(gameObject.tag == "Player_Bullet " && col.name == "Bacula")
		{
			character_status obj = col.GetComponent<character_status>();
			if (obj != null)
			{
				obj.Opponent = Player_Number;
			}
			GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
			ParticleSystem particle = effect.GetComponent<ParticleSystem>();
			effect.transform.position = gameObject.transform.position;
			particle.Play();
			gameObject.SetActive(false);
			Player_Bullet_Des();
		}
        else if (gameObject.tag == "Player_Bullet" && col.gameObject.tag == "Enemy" && col.gameObject.name != "One_Boss_LaserLine" && col.gameObject.name != "Two_Boss_Laser")
        {
            //add:0513_takada 爆発エフェクトのテスト
            //AddExplosionProcess();
            character_status obj = col.GetComponent<character_status>();
            if (obj != null)
            {
                obj.Opponent = Player_Number;
            }
            GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
            ParticleSystem particle = effect.GetComponent<ParticleSystem>();
            effect.transform.position = gameObject.transform.position;
            particle.Play();
			Player_Bullet_Des();
			//if (P1 != null) P1.Bullet_cnt--;
			//if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			//{
			//    if (P2 != null) P2.Bullet_cnt--;
			//}
			gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Boss_Gard")
        {
            GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
            ParticleSystem particle = effect.GetComponent<ParticleSystem>();
            effect.transform.position = gameObject.transform.position;
            particle.Play();
            gameObject.SetActive(false);
        }
    }
}

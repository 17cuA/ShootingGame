using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;


/// <summary>
///　陳　弄りますー
/// </summary>

public class Enemy_Manta : character_status
{
	[SerializeField] private Animation animationPlayer;
	[SerializeField] private Boss_One_A111 chargeController;
	[SerializeField] private GameObject laserColliderController;

	public float chargeTime;
	public float emitterTime;
	public ChargeDevice chargeDevice;
	public EmitterDevice emitterDevice;

	public bool isTop;
	public bool isMiddle;
	public bool isBottom;

	public float debutDuration;
	public float waitDuration;
	public float moveDuration;

	public float firstShotDelay;

	public float shotIntervalTime;
	private float shotTimer;

	private bool canFirstShot = true;
	public StateType stateType;

	[SerializeField] private BoxCollider[] hitboxs = new BoxCollider[4];
	private StateManager<StateType> stateManager;

	private void Awake()
	{
		chargeDevice = new ChargeDevice();
		emitterDevice = new EmitterDevice();

		stateManager = new StateManager<StateType>();

		var newState = new StateBase<StateType>(debutDuration, StateType.DEBUT);
		newState.EnterCallBack = Debut_Enter;
		newState.UpdateCallBack = Debut_Update;
		newState.ExitCallBack = Debut_Exit;
		stateManager.Add(newState);

		newState = new StateBase<StateType>(waitDuration, StateType.WAIT);
		newState.EnterCallBack = Wait_Enter;
		newState.UpdateCallBack = Wait_Update;
		newState.ExitCallBack = Wait_Exit;
		stateManager.Add(newState);

		newState = new StateBase<StateType>(moveDuration, StateType.MOVE);
		newState.EnterCallBack = Move_Enter;
		newState.UpdateCallBack = Move_Update;
		newState.ExitCallBack = Move_Exit;
		stateManager.Add(newState);

		newState = new StateBase<StateType>(5f, StateType.DEATH);
		newState.EnterCallBack = Death_Enter;
		newState.UpdateCallBack = Death_Update;
		newState.ExitCallBack = Death_Exit;
		stateManager.Add(newState);

		newState = new StateBase<StateType>(5f, StateType.ESCAPE);
		newState.EnterCallBack = Escape_Enter;
		newState.UpdateCallBack = Escape_Update;
		newState.ExitCallBack = Escape_Exit;
		stateManager.Add(newState);

		hitboxs = GetComponents<BoxCollider>();
	}

	new private void Start()
	{
		base.Start();
		chargeDevice.SetUp(transform.Find("Beam_Mazle").GetChild(1).GetComponent<ParticleSystem>(), this.chargeTime);
		emitterDevice.SetUp(chargeDevice, transform.Find("Beam_Mazle").GetChild(0).GetComponent<ParticleSystem>(), transform.Find("Beam_Mazle").GetChild(0).GetComponent<LaserColliderManager>(),this.emitterTime);
		stateManager.Start(StateType.DEBUT);
	}

	new private void Update()
	{
		chargeDevice.Execute();
		emitterDevice.Execute();
		stateManager.Update();
		stateType = stateManager.Current.StateType;
	}

	private void Debut_Enter()
	{
		for(var i = 0; i < hitboxs.Length; ++i)
		{
			hitboxs[i].enabled = false;
		}
	}

	private void Debut_Update()
	{
		transform.position -= speed * Vector3.right * Time.deltaTime;

		if (stateManager.Current.IsDone)
		{
			stateManager.ChangeState(StateType.WAIT);
			return;
		}
	}

	private void Debut_Exit()
	{

	}

	private void Wait_Enter()
	{
		for (var i = 0; i < hitboxs.Length; ++i)
		{
			hitboxs[i].enabled = true;
		}
	}

	private void Wait_Update()
	{
		shotTimer += Time.deltaTime;
		if(shotTimer >= firstShotDelay && canFirstShot)
		{
			chargeDevice.Charge();
			canFirstShot = false;
		}

		if(stateManager.Current.IsDone)
		{
			stateManager.ChangeState(StateType.MOVE);
			return;
		}
	}

	private void Wait_Exit()
	{
		shotTimer = 0f;
	}

	private void Move_Enter()
	{
		
	}

	private void Move_Update()
	{
		transform.position -= speed / 2f * Vector3.right * Time.deltaTime;
		shotTimer += Time.deltaTime;
		if(shotTimer >= firstShotDelay)
		{
			chargeDevice.Charge();
			shotTimer = 0f;
		}

		if(stateManager.Current.IsDone)
		{
			stateManager.ChangeState(StateType.ESCAPE);
			return;
		}

		if(hp < 1)
		{
			stateManager.ChangeState(StateType.DEATH);
			return;
		}
	}

	private void Move_Exit()
	{

	}

	private void Death_Enter()
	{
		for (var i = 0; i < hitboxs.Length; ++i)
		{
			hitboxs[i].enabled = false;
		}
	}

	private void Death_Update()
	{

	}

	private void Death_Exit()
	{

	}

	private void Escape_Enter()
	{
		this.gameObject.SetActive(false);
	}

	private void Escape_Update()
	{

	}

	private void Escape_Exit()
	{

	}
}

[System.Serializable]
public class ChargeDevice
{
	public float chargeTime;
	private float chargeTimer;
	private ParticleSystem chargeParticleSystem;
	private bool canPlay = false;
	private bool isChargeOver = false;
	private bool isPlaying = false;

	public void SetUp(ParticleSystem chargeParticleSystem,float chargeTime)
	{
		this.chargeParticleSystem = chargeParticleSystem;
		this.chargeTime = chargeTime;
		
	}

	public void Execute()
	{
		if (canPlay)
		{
			chargeTimer += Time.deltaTime;
			if (chargeTimer >= chargeTime)
			{
				chargeTimer = 0;
				chargeParticleSystem.Stop();
				isChargeOver = true;
				canPlay = false;
			}
		}
	}

	public void Charge()
	{
		if (isPlaying)
			return;

		chargeParticleSystem.Play();
		canPlay = true;
		isChargeOver = false;
		isPlaying = true;
	}

	public bool IsChargeOver()
	{
		return isChargeOver;
	}

	public void Close()
	{
		canPlay = false;
		isChargeOver = false;
		isPlaying = false;
	}
}

[System.Serializable]
public class EmitterDevice
{
	public float emitterTime;
	private float emitterTimer;
	private ChargeDevice chargeDevice;
	private ParticleSystem emitterParticleSystem;
	private LaserColliderManager laserColliderManager;

	public void SetUp(ChargeDevice chargeDevice, ParticleSystem emitterParticleSystem, LaserColliderManager laserColliderManager,float emitterTime)
	{
		this.chargeDevice = chargeDevice;
		this.emitterParticleSystem = emitterParticleSystem;
		this.laserColliderManager = laserColliderManager;
		this.emitterTime = emitterTime;
		laserColliderManager.isLoop = true;
		laserColliderManager.enabled = false;
		laserColliderManager.playTime = emitterTime;
	}

	public void Execute()
	{
		if (chargeDevice.IsChargeOver())
		{
			if(emitterParticleSystem.isStopped) emitterParticleSystem.Play();
			if(!laserColliderManager.enabled)laserColliderManager.enabled = true;
			if(!laserColliderManager.GetComponent<BoxCollider>().enabled) laserColliderManager.GetComponent<BoxCollider>().enabled = true;

			emitterTimer += Time.deltaTime;
			if (emitterTimer >= emitterTime)
			{
				emitterParticleSystem.Stop();
				laserColliderManager.elapsedTime = 0f;
				emitterTimer = 0f;

				laserColliderManager.xDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;
				laserColliderManager.yDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;
				laserColliderManager.zDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;

				laserColliderManager.GetComponent<BoxCollider>().enabled = false;
				laserColliderManager.enabled = false;

				chargeDevice.Close();
			}
		}
	}
}


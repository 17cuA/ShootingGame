using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_TwoBossCorePart : MonoBehaviour
{
	private Two_Boss_Parts partScript;
	[SerializeField] private GameObject partInstance;
	[SerializeField] private Texture2D red_baseColorTexture;
	[SerializeField] private Texture2D red_emissiveTexture;
	[SerializeField] private ParticleSystem particleSystem;
	[SerializeField] private int transitionLimitHp = 100;
	private new Renderer renderer;
	private bool change = false;
	public bool hasChanged = false;

	private void Awake()
	{
		partScript = GetComponent<Two_Boss_Parts>();
	
	}

	private void Start()
	{
		renderer = partInstance.GetComponent<Renderer>();
	}

	private void Update()
	{
		if (partScript.hp <= transitionLimitHp)
		{
			change = true;
		}

		//Hp > limitHp || isCHanged  return 
		if (!change || hasChanged)
			return;

		if (particleSystem != null)
		{
			particleSystem.transform.position = transform.position;
			particleSystem?.Stop();
			particleSystem?.Play();
		}

		partScript.self_material[0].SetTexture("_BaseColorMap", red_baseColorTexture);
		partScript.self_material[0].SetTexture("_EmissiveColorMap", red_emissiveTexture);
		hasChanged = true;
	}
}

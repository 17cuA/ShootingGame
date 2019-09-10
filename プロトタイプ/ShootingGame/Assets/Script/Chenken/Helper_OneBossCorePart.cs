using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_OneBossCorePart : MonoBehaviour
{
	private One_Boss_Parts partScript;
	[SerializeField] private GameObject partInstance;
	[SerializeField] private Texture2D red_baseColorTexture;
	[SerializeField] private Texture2D red_emissiveTexture;
	[SerializeField] private ParticleSystem particleSystem;
	[SerializeField] private int transitionLimitHp = 100;
	private new Renderer renderer;
	private bool change = false;
	private bool hasChanged = false;

	private void Awake()
	{
		partScript = GetComponent<One_Boss_Parts>();
		renderer = partInstance.GetComponent<Renderer>();
	}

	private void Update()
    {
        if(partScript.hp <= transitionLimitHp)
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

		renderer.material.SetTexture("_BaseColorMap", red_baseColorTexture);
		renderer.material.SetTexture("_EmissiveColorMap", red_emissiveTexture);
		hasChanged = true;
    }
}

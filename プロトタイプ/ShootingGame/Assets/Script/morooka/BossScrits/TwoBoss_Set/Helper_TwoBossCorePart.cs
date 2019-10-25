using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_TwoBossCorePart : MonoBehaviour
{
	private Two_Boss_Parts partScript;
	[SerializeField] private GameObject partInstance;
	[SerializeField] private Texture2D red_baseColorTexture;		// 赤のテクスチャー
	[SerializeField] private Texture2D red_emissiveTexture;			// 赤のエミッシブ
	[SerializeField] private ParticleSystem particleSystem;			// 色変換時のパーティクル
	[SerializeField] private int transitionLimitHp = 100;				// 色を変えるHPの量
	private new Renderer renderer;											// レンダー
	private bool isChange = false;												// 変更するか
	public bool hasChanged = false;											// 変更が終わっているか

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
			isChange = true;
		}
		if (!isChange || hasChanged) return;

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

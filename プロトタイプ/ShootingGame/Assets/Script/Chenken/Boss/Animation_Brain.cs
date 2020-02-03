using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Brain : MonoBehaviour
{
    [Header("Animation Setting")]
	[Range(0.5f, 0.9f)]
	public float waveScaleIndex = 0.75f;
	[Range(0.75f, 0.9f)]
	public float intervalWaveScaleIndex = 0.825f;
	[Range(1,3)]
	public int waveCount = 2;
	[Range(0.25f,1f)]
	public float waveInterval = 1.5f;
	[Range(0.5f, 1.5f)]
	public float endWaveTime = 3f;
	[Range(0.5f, 1.5f)]
	public float startWaveTime = 3f;

	private int waveCounter = 0;
	private float waveIntervalTimer = 0f;
	private float endWaveTimer = 0f;
	private float startWaveTimer = 0f;

	private bool isRestore = false;
	private bool isEnd = false;

	#region Field
	private float waveScaleMax;
	private float waveScaleMin;
	private float intervalScaleMax;
	private float intervalScaleMin;
	#endregion

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		waveScaleMax = this.transform.localScale.x;
		waveScaleMin = waveScaleMax * waveScaleIndex;
		intervalScaleMax = this.transform.localScale.x;
		intervalScaleMin = intervalScaleMax * intervalWaveScaleIndex;

		this.transform.localScale = new Vector3(waveScaleMin, waveScaleMin, waveScaleMin);
	}

	private void Update()
	{
		if (!isEnd)
		{
			if (startWaveTimer <= startWaveTime)
			{
				var x = Mathf.Lerp(waveScaleMin, intervalScaleMin, startWaveTimer / startWaveTime);
				var y = Mathf.Lerp(waveScaleMin, intervalScaleMin, startWaveTimer / startWaveTime);
				var z = Mathf.Lerp(waveScaleMin, intervalScaleMin, startWaveTimer / startWaveTime);

				this.transform.localScale = new Vector3(x, y, z);
				startWaveTimer += Time.deltaTime;
			}
			else
			{
				if (!isRestore)
				{
					if (waveIntervalTimer <= waveInterval)
					{
						var x = Mathf.Lerp(intervalScaleMin, intervalScaleMax, waveIntervalTimer / waveInterval);
						var y = Mathf.Lerp(intervalScaleMin, intervalScaleMax, waveIntervalTimer / waveInterval);
						var z = Mathf.Lerp(intervalScaleMin, intervalScaleMax, waveIntervalTimer / waveInterval);

						this.transform.localScale = new Vector3(x, y, z);
						waveIntervalTimer += Time.deltaTime;
					}
					else
					{
						isRestore = true;
						waveCounter++;
						waveIntervalTimer = 0;
					}
				}
				else
				{
					if (waveIntervalTimer <= waveInterval)
					{
						var x = Mathf.Lerp(intervalScaleMax, intervalScaleMin, waveIntervalTimer / waveInterval);
						var y = Mathf.Lerp(intervalScaleMax, intervalScaleMin, waveIntervalTimer / waveInterval);
						var z = Mathf.Lerp(intervalScaleMax, intervalScaleMin, waveIntervalTimer / waveInterval);

						this.transform.localScale = new Vector3(x, y, z);
						waveIntervalTimer += Time.deltaTime;
					}
					else
					{
						isRestore = false;
						waveIntervalTimer = 0;
						if (waveCounter == waveCount)
						{
							startWaveTimer = 0f;
							waveCounter = 0;
							isEnd = true;
						}
					}
				}
			}
		}
		else
		{
			if (endWaveTimer <= endWaveTime)
			{
				var x = Mathf.Lerp(intervalScaleMin, waveScaleMin, endWaveTimer / endWaveTime);
				var y = Mathf.Lerp(intervalScaleMin, waveScaleMin, endWaveTimer / endWaveTime);
				var z = Mathf.Lerp(intervalScaleMin, waveScaleMin, endWaveTimer / endWaveTime);

				this.transform.localScale = new Vector3(x, y, z);
				endWaveTimer += Time.deltaTime;
			}
			else
			{
				endWaveTimer = 0f;
				isEnd = false;
			}
		}
	}
}

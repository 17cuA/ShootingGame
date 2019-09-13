using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Helper_SceneLoadAsync : MonoBehaviour
{
	[SerializeField] private bool isAutoLoading = false;
	public bool IsAutoLoading
	{
		get
		{
			return isAutoLoading;
		}
		set
		{
			isAutoLoading = value;
		}
	}

	AsyncOperation async;   
	[SerializeField] private string SceneName;
	[SerializeField] private Slider loadingSlider;
	[SerializeField] private Text loadingText; 
	[SerializeField] private float loadingSpeed = 1;

	private float targetValue;

	private void Start()
	{
		loadingSlider.value = 0.0f;
	}

	private void Update()
	{
		if(isAutoLoading)
		{
			StartCoroutine("LoadScene");
			isAutoLoading = false;
		}

		if (async == null) return;

		targetValue = async.progress;


		if (async.progress >= 0.9f)
		{
			targetValue = 1.0f;
		}

		if (targetValue != loadingSlider.value)
		{
			loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);

			if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
			{

				loadingSlider.value = targetValue;
			}
		}

		loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

		if ((int)(loadingSlider.value * 100) == 100)
			async.allowSceneActivation = true;
	}

	private IEnumerator LoadScene()
	{
		async = SceneManager.LoadSceneAsync(SceneName);

		//async.allowSceneActivation = false;

		print("Loading:" + async);

		yield return async;
	}
}



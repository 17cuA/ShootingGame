using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※これを忘れずに入れる

public class Energ_Bar: MonoBehaviour {

  Slider _slider;
  public Player1 playerScript;
  public float energy;
  public float Max_Energy;
  void Start ()
  {
    Max_Energy=100.0f;
    // スライダーを取得する
    _slider = GameObject.Find("Slider").GetComponent<Slider>();
    Debug.Log(energy); 
  }


  void Update ()
  {
		energy +=0.1f;
		if (Input.GetMouseButton(1))
		{
			if (energy >= 0.0f) energy -= 0.7f;
		}
		if (energy <= 100.0f) energy += 0.3f;
    _slider.maxValue = Max_Energy;
    _slider.value = energy;
  }
}
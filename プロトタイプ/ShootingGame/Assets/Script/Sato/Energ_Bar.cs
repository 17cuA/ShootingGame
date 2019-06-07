using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※これを忘れずに入れる

public class Energ_Bar: MonoBehaviour {

  Slider _slider;
  public Player1 playerScript;
  public float Energy;
  public float Max_Energy;
  public GameObject Object;
  void Start ()
  {
    Max_Energy=100.0f;
    // スライダーを取得する
    _slider = GameObject.Find("Slider").GetComponent<Slider>();
    Debug.Log(Energy);
	Object = GameObject.Find("Player_Item");
  }

  void Update ()
  {
	//Energy = playerScript.energy;
	//Max_Energy += playerScript.energy_Max;
	if (Input.GetMouseButton(1))
	{
		if (Energy >= 0.0f) Energy -= 0.7f;
	}
	if (Energy <= 100.0f) Energy += 0.3f;
    _slider.maxValue = Max_Energy;
    _slider.value = Energy;
  }
}
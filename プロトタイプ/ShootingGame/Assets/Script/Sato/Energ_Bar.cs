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
    energy = playerScript.Energy;
    Debug.Log(energy); 
  }


  void Update ()
  {
    energy = playerScript.Energy;
    _slider.maxValue = Max_Energy;
    _slider.value = energy;
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
	public enum SCENE_NAME
	{
		eROGO,
		eTITLE,
		eMENU,
		eSTAGE,
		eGAME_OVER,
		eGAME_CLEAR,
	}

	[SerializeField,Header("フェード用黒")] private GameObject object_for_fade;
	[SerializeField,Header("フェードスピード")] private float fade_speed;

	private Renderer renderer_for_fade{ get; set; }
	private Color color_for_fade{ get; set; }

	public SCENE_NAME Now_Scene{ get; private set; }
	
    void Start()
    {
		Now_Scene = (SCENE_NAME)SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
    }

	public void screen_move_to_ROGO()
	{

	}
}

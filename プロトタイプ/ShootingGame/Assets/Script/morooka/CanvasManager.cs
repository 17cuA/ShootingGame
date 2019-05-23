using UnityEngine;
using TextDisplay;

public class CanvasManager : MonoBehaviour
{
    //public Numeric_Display Score_Display{private set; get;}
	public Character_Display Score_Display { private set; get; }

	// Start is called before the first frame update
	void Start()
    {
		//Score_Display = transform.GetChild(0).GetComponent<Numeric_Display>();
		//Score_Display.Digits_Preference(10);
		Score_Display = new Character_Display(10, "morooka/SS",transform,new Vector3(0.0f,480.0f,0.0f));
		Score_Display.Character_Preference("0000000000");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

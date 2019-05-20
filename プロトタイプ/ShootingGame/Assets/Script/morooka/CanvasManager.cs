using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Numeric_Display Score_Display{private set; get;}

    // Start is called before the first frame update
    void Start()
    {
        Score_Display = transform.GetChild(0).GetComponent<Numeric_Display>();
        Score_Display.Digits_Preference(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

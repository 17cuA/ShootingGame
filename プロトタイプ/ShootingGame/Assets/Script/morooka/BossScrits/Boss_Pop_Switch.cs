using UnityEngine;
using System.Collections;

public class Boss_Pop_Switch : MonoBehaviour
{
    float speed;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f / 60.0f;
        //boss = GameObject.Find("Boss_test(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        switch (Game_Master.MY.Management_In_Stage)
        {
            case Game_Master.CONFIGURATION_IN_STAGE.eNORMAL:
                Normal_Update();
                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eBOSS_CUT_IN:
                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eBOSS_BUTLE:
                boss.SetActive(true);
                boss.transform.position = transform.position;
                Destroy(gameObject);

                break;
            case Game_Master.CONFIGURATION_IN_STAGE.eCLEAR:
                break;
            default:
                break;
        }

    }

    private void Normal_Update()
    {
        Vector3 vector = transform.position;
        vector.x -= speed;
        transform.position = vector;

        if (transform.position.x <= 9.0f)
        {
            Game_Master.MY.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.eBOSS_CUT_IN;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Transform midPoint;
    public Transform movePoint;
    [Range(9.8f,90f)] public float gravity = 9.8f;
    [Range(1,10f)]public float friction = 0.05f;
    [Range(0.1f,0.5f)]public float theltaLimit = 0.5f;
    [Range(2f,25f)]public float force = 5;
	[Range(2f, 15f)] public float afterForce = 3f;
    [Range(0.005f,0.5f)] public float invaildTime = 0.25f;
    private bool canHit = true;
    private float invaildTimer;
    private Vector3 rotateAxis;
    private float w;

	[Header("判定検索名")]
	public List<string> checkName = new List<string>();

    public bool isStepOne = false;
    public bool isStepTwo = false;
    private bool isHit = false;
	public bool hasHitOnce = false;

	[Header("位置設定")]
	public bool isTop = false;

    private void Start()
    {
		rotateAxis = (isTop) ? Vector3.Cross (movePoint.position - midPoint.position, Vector3.down) : Vector3.Cross(movePoint.position - midPoint.position, Vector3.up); 
	}

    private void Update()
    {  
        if (!canHit)
        {
            invaildTimer += Time.deltaTime;
            if(invaildTimer >= invaildTime)
            {
                invaildTimer = 0f;
                canHit = true;
            }
        }

        if(isHit)
        {
            float r = Vector3.Distance(midPoint.position,movePoint.position);
			w = !hasHitOnce ? force / r : afterForce / r; ;
			if (hasHitOnce == false)
				hasHitOnce = true;
            isStepOne = true;
            isStepTwo = false;
            isHit = false;
           
        }

        if(isStepOne)
        {
             float r = Vector3.Distance(midPoint.position,movePoint.position);
            w -= (gravity - friction) / r * Time.deltaTime;
            float thelta = w * Time.deltaTime * 180f / Mathf.PI;
			transform.RotateAround(midPoint.position, transform.up, thelta);
			if (w <= 0.1f)
            {
				rotateAxis = (isTop) ? Vector3.Cross(movePoint.position - midPoint.position, Vector3.down) : Vector3.Cross(movePoint.position - midPoint.position, Vector3.up); ;
				isStepTwo = true;
                isStepOne = false;
            }
        }
        if (isStepTwo)
        {
             float r = Vector3.Distance(midPoint.position,movePoint.position);
             float l = Vector3.Distance(new Vector3(midPoint.position.x,movePoint.position.y,midPoint.position.z),movePoint.position);
			 Vector3 axis = (isTop) ? Vector3.Cross(movePoint.position - midPoint.position, Vector3.down) : Vector3.Cross(movePoint.position - midPoint.position, Vector3.up);
			if (Vector3.Dot(axis,rotateAxis) < 0)
             {
                 l *= -1;
             }
             float cosAlpha = l / r;     //cos値
             float alpha =  (cosAlpha * gravity - friction) / r;
             w += alpha * Time.deltaTime;
             float thelta = w * Time.deltaTime * 180f / Mathf.PI;
             transform.RotateAround(midPoint.position,rotateAxis,thelta);
			if (Mathf.Abs(thelta) <= theltaLimit)
             {
				rotateAxis = (isTop) ? Vector3.Cross(movePoint.position - midPoint.position, Vector3.down) : Vector3.Cross(movePoint.position - midPoint.position, Vector3.up); ;
				if (Mathf.Abs(l) <= 0.5f)
                 {
                     rotateAxis = Vector3.zero;
                     isStepTwo = false;
                     w = 0;
					 hasHitOnce = false;
                     Debug.Log("Over");
                 }
             }
        }

    }

	private void OnTriggerEnter(Collider col)
	{
		for(var i = 0; i < checkName.Count; ++i)
		{
			if (col.name == checkName[i])
			{
				if (canHit)
				{
					isHit = true;
					canHit = false;
				}
			}
		}
	}
}

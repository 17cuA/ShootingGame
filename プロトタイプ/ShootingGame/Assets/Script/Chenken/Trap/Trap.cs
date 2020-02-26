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
    [Range(0.5f,2f)] public float invaildTime = 0.25f;
    private bool canHit = true;
    private float invaildTimer;
    private Vector3 rotateAxis;
    private float w;

    public bool isStepOne = false;
    public bool isStepTwo = false;
    public bool isHit = false;

    private void Start()
    {
        rotateAxis = Vector3.Cross (movePoint.position - midPoint.position, Vector3.down);
    }

    private void Update()
    {
      
        if (Input.GetMouseButtonDown(0) && canHit)
        {
            isHit = true;
            canHit = false;
        }

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
            w = force / r;
            isStepOne = true;
            isStepTwo = false;
            isHit = false;
           
        }

        if(isStepOne)
        {
             float r = Vector3.Distance(midPoint.position,movePoint.position);
            w -= (gravity - friction) / r * Time.deltaTime;
            float thelta = w * Time.deltaTime * 180f / Mathf.PI;
            transform.RotateAround(midPoint.position,Vector3.forward,thelta);
            if(w <= 0.1f)
            {
                 rotateAxis = Vector3.Cross (movePoint.position - midPoint.position, Vector3.down);
                isStepTwo = true;
                isStepOne = false;
            }
        }
        if (isStepTwo)
        {
             float r = Vector3.Distance(midPoint.position,movePoint.position);
             float l = Vector3.Distance(new Vector3(midPoint.position.x,movePoint.position.y,midPoint.position.z),movePoint.position);
             Vector3 axis = Vector3.Cross(movePoint.position - midPoint.position,Vector3.down);
             if(Vector3.Dot(axis,rotateAxis) < 0)
             {
                 l *= -1;
             }
             float cosAlpha = l / r;     //cos値
             float alpha =  (cosAlpha * gravity - friction) / r;
             w += alpha * Time.deltaTime;
             float thelta = w * Time.deltaTime * 180f / Mathf.PI;
             transform.RotateAround(midPoint.position,rotateAxis,thelta);
             if(Mathf.Abs(thelta) <= theltaLimit)
             {
                 rotateAxis = Vector3.Cross (movePoint.position - midPoint.position, Vector3.down);
                 if(Mathf.Abs(l) <= 0.5f)
                 {
                     rotateAxis = Vector3.zero;
                     isStepTwo = false;
                     w = 0;
                     Debug.Log("Over");
                 }
             }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLaserController : MonoBehaviour
{
    private Transform parent;
    private GameObject[] arrowArray = new GameObject[20];
    public Sprite sprite;
    public Vector3 target;

    private void Awake()
    {
        parent   = this.transform;

        for(var i = 0; i < arrowArray.Length; ++i)
        {
            var image = new GameObject().AddComponent<SpriteRenderer>();
            image.sprite  = sprite;
            image.transform.SetParent(parent);
            image.gameObject.name  = "laser_node";
            arrowArray[i] = image.gameObject;
        }
    }

    public void SetArrow(Vector2 startPos, Vector2 endPos)
    {
        var controlPointA = Vector2.zero;
        var controlPointB = Vector2.zero;
        controlPointA.x = startPos.x + (startPos.x - endPos.x) * 0.1f;
        controlPointA.y = endPos.y - (endPos.y - startPos.y) * 0.2f;
        controlPointB.y = endPos.y + (endPos.y - startPos.y) * 0.3f;
        controlPointB.x = startPos.x - (startPos.x - endPos.x) * 0.3f;

        for(var i = 0; i < arrowArray.Length; ++i)
        {
            var t = (float) i / 19f;
            var pos = startPos * (1 - t) * (1 - t) * (1 - t) +
                      3 * controlPointA * t * (1 - t) * (1 - t) + 
                      3 * controlPointB * t * t * (1 - t) +
                      endPos * t * t * t;
            arrowArray[i].transform.position = pos;
        }

        UpdateAngle();
    }

    private void UpdateAngle()
    {
        for(var i = 0; i < arrowArray.Length; ++i)
        {
            if(i == 0)
            { 
                arrowArray[i].transform.localEulerAngles = new Vector3(0, 0, 90);
                continue;
            }
            var current   = arrowArray[i];
            var last      = arrowArray[i - 1];
            var direction = current.transform.position - last.transform.position;
            var sign      =  (current.transform.position.y < last.transform.position.y) ? -1 : 1;
            var angle     = Vector3.Angle(direction,Vector3.right) * sign;
            current.transform.localEulerAngles = new Vector3(0, 0, angle);
        }
    }

    public void Update()
    {
        SetArrow(transform.position,target);
    }
}

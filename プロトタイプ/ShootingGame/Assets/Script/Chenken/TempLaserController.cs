using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLaserController : MonoBehaviour
{
    public class Controller
    {
        public Vector2 pushPos;
        public float theta;

        public Controller(Vector2 pushPos)
        {
            this.pushPos = pushPos;
            this.theta = 0;
        }

        public void Rotate(float angle)
        {
            this.theta += angle;
            this.pushPos.x = Mathf.Cos(this.theta);
            this.pushPos.y = Mathf.Sin(this.theta);
        }
    }

    public class Point
    {
        public Vector2 direction;
        public float speed;
        public Vector2 pos;
        
        public Point(Vector2 pos, Vector2 direction, float speed)
        {
            this.pos = pos;
            this.direction = direction;
            this.speed = speed;
        }

        public void Move()
        {
            pos += direction * speed * Time.deltaTime;
        }
    }

    private Dictionary<LineRenderer,List<Point>> datas;
    private LineRenderer currentLine;
    private Controller controller;
    [Range(0,  0.03f)] public float laserShotInterval;
    [Range(0.2f,0.5f)]public float waitTime = 0.2f;
    public Material lineMaterial;
    public Material trailMaterial;
    public float lineWidth;
    public float laserSpeed;
    public int waitLaserLimit = 80;
    public bool rotateLaserControl;
    private float laserShotTime;
    private int currentPointsCount;
    private int index;
    private int laserCount;
    
    private void Awake()
    {
        datas = new Dictionary<LineRenderer, List<Point>>();
        controller = new Controller(new Vector2(0.1f,0));
    }

    private void Update()
    {
        if(rotateLaserControl)
        { 
            HandleInput();
        }

        if((Input.GetKeyDown(KeyCode.Space) && Time.time >= laserShotTime))
        {
            CreateNewLaserLine();
        }

                
        if(Input.GetKey(KeyCode.Space) &&  Time.time >= laserShotTime)
        {         
            DynamicChangeLine();

            laserCount++;

            laserShotTime = Time.time + laserShotInterval;
        }

        if(laserCount >= waitLaserLimit)
        {
            CreateNewLaserLine();
            laserShotTime = Time.time + waitTime;
            laserCount = 0;
        }


        UpdatePoints();                   
    }

    private void HandleInput()
    {
         if(Input.GetKey(KeyCode.UpArrow))
         {
             controller.Rotate(-Mathf.PI / 12.0f);
         }
         if(Input.GetKey(KeyCode.DownArrow))
         {
             controller.Rotate(Mathf.PI / 12.0f);
         }
    }

    private void CreateNewLaserLine()
    {
         var go = new GameObject("LaserLine");
         var lineRenderer        = go.AddComponent<LineRenderer>();
         lineRenderer.material   = lineMaterial;
         lineRenderer.startWidth = lineWidth;
         lineRenderer.endWidth   = lineWidth;
         currentLine = lineRenderer;
         datas.Add(currentLine, new List<Point>());
    }

    private void DynamicChangeLine()
    {
         var tempX = 0f;
         var tempY = 0f;

         controller.pushPos.Normalize();
         tempX += controller.pushPos.x;
         tempY += controller.pushPos.y;
         
         datas[currentLine].Add(new Point(new Vector2(transform.position.x + tempX, transform.position.y * tempY), controller.pushPos, laserSpeed));
      
         var temp = datas[currentLine].Count;
         currentLine.positionCount = temp;
    }

    private void UpdatePoints()
    {
        foreach(var data in datas)
        {
            var outScreenCount = 0;
            for(var i = 0; i < data.Value.Count; ++i)
            {
                if(data.Value[i].pos.x >= 20 || data.Value[i].pos.x <= -20 || data.Value[i].pos.y >= 7 || data.Value[i].pos.y <= -7)
                {
                    outScreenCount++;
                }

                data.Value[i].Move();
                data.Key.SetPosition(i, data.Value[i].pos);
            }
            if(outScreenCount == data.Value.Count && outScreenCount > 0)
            {
                var uselessLineRenderer = data.Key;
                datas.Remove(data.Key);
                Destroy(uselessLineRenderer.gameObject);

                break;
            }
        }        
    }
}

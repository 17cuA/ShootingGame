using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLaserController : MonoBehaviour
{
    public class Controller
    {

        public Vector2 pushPos;
        public float theta;

        public Controller(Vector2 pushPos, float moveSpeed)
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
        public Vector2 pos;
        public Vector2 direction;
        public float speed;
        
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


    private LineRenderer lineRenderer;
    private List<Point> points;
    private Controller controller;

    
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        points = new List<Point>();
        controller = new Controller(new Vector2(1,0), 5);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            controller.Rotate(-Mathf.PI / 12.0f);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            controller.Rotate(-Mathf.PI / 12.0f);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            var tempX = 0f;
            var tempY = 0f;

            for(var i = 0; i < 4; ++i)
            {
                points.Add(new Point( new Vector2(transform.position.x + tempX, transform.position.y * tempY), controller.pushPos , 10));
                tempX += controller.pushPos.x;
                tempY += controller.pushPos.y;
            }
        }

        for(var i = 0; i < points.Count; ++i)
            points[i].Move();

        for(var i = 0; i < points.Count; ++i)
            lineRenderer.SetPosition(i,points[i].pos);
    }
}

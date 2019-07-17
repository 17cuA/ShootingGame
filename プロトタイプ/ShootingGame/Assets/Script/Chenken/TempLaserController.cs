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

    public class Point : MonoBehaviour
    {
        public Vector3 direction;
        public float speed;
		private LineRenderer line;
		public void Start()
		{
			this.line = GetComponent<LineRenderer>();
		}

		public void Update()
        {
			transform.position += direction * speed * Time.deltaTime;
			line.SetPosition(0, transform.position);
			line.SetPosition(1, transform.position - Vector3.right * speed * Time.fixedDeltaTime);
		}
	}
    private Controller controller;
    [Range(0,  0.03f)] public float laserShotInterval;
    [Range(0.2f,0.5f)] public float waitTime = 0.2f;
	public float lineWidth;
	public float laserSpeed;
	public int waitLaserLimit = 80;
	public Material lineMaterial;
	public int laserAttack;

    public bool rotateLaserControl;
    private float laserShotTime;
    private int laserCount;
    
    private void Awake()
    {
        controller = new Controller(new Vector2(0.1f,0));
    }

    private void Update()
    {
        if(rotateLaserControl)
        { 
            HandleInput();
        }

                
        if(Input.GetKey(KeyCode.Space) &&  Time.time >= laserShotTime)
        {         
            DynamicChangeLine();

            laserCount++;

            laserShotTime = Time.time + laserShotInterval;
        }

        if(laserCount >= waitLaserLimit)
        {
            laserShotTime = Time.time + waitTime;
            laserCount = 0;
        }

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


    private void DynamicChangeLine()
    {
         var tempX = 0f;
         var tempY = 0f;

        controller.pushPos.Normalize();
        tempX += controller.pushPos.x;
        tempY += controller.pushPos.y;

		var pointGo = new GameObject("Point");

		var point = pointGo.AddComponent<Point>();
		point.transform.position = new Vector3(transform.position.x + tempX, transform.position.y * tempY, 0);
		point.direction = controller.pushPos;
		point.speed = laserSpeed;

		var pointCollider = point.gameObject.AddComponent<CapsuleCollider>();
		pointCollider.direction = 0;
		pointCollider.height = laserSpeed * Time.fixedDeltaTime;

		var bullet = pointGo.AddComponent<Player_Bullet>();
		bullet.tag = "Player_Bullet";
		bullet.attack_damage = laserAttack;

		var line = pointGo.AddComponent<LineRenderer>();
		line.startWidth = lineWidth;
		line.endWidth = lineWidth;
		line.material = lineMaterial;
    }
}

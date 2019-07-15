using System;
using System.Collections.Generic;
using UnityEngine;

public class LaserMeshGenerator : MonoBehaviour
{
    public class Point
    {
        public Vector3 direction;
        public Vector3 position;
        public float speed;

        public Point(Vector3 direction, Vector3 position, float speed)
        {
            this.direction = direction;
            this.position = position;
            this.speed = speed;
        }

        public void Move()
        {
            this.position += direction * speed * Time.deltaTime;
        }
    }

    public class Triangles
    {
        public Vector3[] vertices;
        public int[] trianglesVerticesID;

        public Triangles(Vector3[] tempVertices)
        {

            Debug.Log(tempVertices.Length);
            vertices = new Vector3[tempVertices.Length];

            trianglesVerticesID = new int[(tempVertices.Length - 2) * 3];


            for(var i = 0; i < tempVertices.Length; ++i)
            {
                this.vertices[i] = tempVertices[i];
            }

            for(int i = 0; i <tempVertices.Length - 2; ++i)
		    {
			    for(int j = 0; j < 3; j++)
			    {
				    if( i%2 ==0)
				    {
					    trianglesVerticesID[3*i + j] = i +j;
				    }
                    else
				    {
					    trianglesVerticesID[3*i + j] = i + 2-j;
				    }
 
			    }
		    }
        }

        public Mesh MakeMesh()
        {
            var mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = trianglesVerticesID;
            return mesh;
        }
    }

    public class MeshInfo
    {
        public Vector3 start;
        public List<Point> points;
        public List<Point> midPoints;
        public Mesh mesh;

        public MeshInfo(Vector3 start)
        {
            this.start = start;
            points = new List<Point>();
            midPoints = new List<Point>();
            mesh = new Mesh();
        }

        public void Update()
        {
            for(var i = 0; i < points.Count; ++i)
            {
                points[i].Move();

                midPoints[i].position = (points[i].position + start) / 2f;
            }
        }
    }

    public class Controller
    {
        public Vector3 pushPosition;
        public float theta;

        public Controller(Vector3 pushPosition)
        {
            this.pushPosition = pushPosition;
            this.theta = 0;
        }

        public void Rotate(float angle)
        {
            this.theta += angle;
            this.pushPosition.x = Mathf.Cos(this.theta);
            this.pushPosition.y = Mathf.Sin(this.theta);
        }
    }

    public float LaserSpeed = 10f;
    public Material laserMaterial;
    private MeshFilter currentMeshFilter;
    private MeshInfo currentMeshInfo;
    private Controller controller;
    private List<MeshInfo> hasCreatedMeshs;
    private List<MeshFilter> hasCreatedLasers;
    private bool isDrawTrueClockRotate = false;

    private void Awake()
    {
        controller = new Controller(new Vector3(0.1f,0,0));
        hasCreatedMeshs = new List<MeshInfo>();
        hasCreatedLasers = new List<MeshFilter>();
    }

    public void Update()
    {
         if(Input.GetKey(KeyCode.UpArrow))
         {
             controller.Rotate(-Mathf.PI / 12.0f);
            isDrawTrueClockRotate = true;
         }
         if(Input.GetKey(KeyCode.DownArrow))
         {
             controller.Rotate(Mathf.PI / 12.0f);
            isDrawTrueClockRotate = false;
         }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateLaserInstance();
        }

        if(Input.GetKey(KeyCode.Space))
        {
            CreateNewMeshPoint();
        }

        
        for(var i = 0; i < hasCreatedMeshs.Count; ++i)
        {
            hasCreatedMeshs[i].Update();
            hasCreatedLasers[i].mesh = hasCreatedMeshs[i].mesh;
        }     
    }

    private void CreateLaserInstance()
    {
        var instance = new GameObject("Laser");
        currentMeshFilter = instance.AddComponent<MeshFilter>();
        var renderer = instance.AddComponent<MeshRenderer>();
        renderer.material = laserMaterial;
        

        var meshInfo = new MeshInfo(transform.position);

        var tempX = 0f;
        var tempY = 0f;
        controller.pushPosition.Normalize();
        tempX += controller.pushPosition.x;
        tempY += controller.pushPosition.y;
        var newPos = new Vector3(transform.position.x + tempX, transform.position.y + tempY);
        var newDirection = controller.pushPosition;
        var newPoint = new Point(newDirection,newPos,LaserSpeed);
        meshInfo.points.Add(newPoint);

        newPoint = new Point(newDirection,newPos, LaserSpeed);
        meshInfo.midPoints.Add(newPoint);

        var triangles = new Triangles(new Vector3[3]{ transform.position ,newPoint.position, newPoint.position });
        meshInfo.mesh = triangles.MakeMesh();
        currentMeshInfo = meshInfo;
        currentMeshFilter.mesh = currentMeshInfo.mesh;
        hasCreatedMeshs.Add(currentMeshInfo);
        hasCreatedLasers.Add(currentMeshFilter);
    }

    private void CreateNewMeshPoint()
    {
        var tempX = 0f;
        var tempY = 0f;

        controller.pushPosition.Normalize();
        tempX += controller.pushPosition.x;
        tempY += controller.pushPosition.y;
        var newPos = new Vector3(transform.position.x + tempX, transform.position.y + tempY);
        var newDirection = controller.pushPosition;
        var newPoint = new Point(newDirection,newPos,LaserSpeed);

        currentMeshInfo.points.Add(newPoint);

        newPoint = new Point(newDirection,newPos, LaserSpeed);
        currentMeshInfo.midPoints.Add(newPoint);

        var posArray = new Vector3[currentMeshInfo.points.Count + currentMeshInfo.midPoints.Count];
        for(int i = 0,j = 0,k = 0; i < currentMeshInfo.points.Count + currentMeshInfo.midPoints.Count; ++i)
        {
            if(i % 2 != 0)
            { 
                if(isDrawTrueClockRotate) posArray[i] = currentMeshInfo.points[j].position;
                else posArray[i] = currentMeshInfo.midPoints[j].position;
                j++;
            }
            else
            {
                if(isDrawTrueClockRotate) posArray[i] = currentMeshInfo.midPoints[k].position;
                else posArray[i] = currentMeshInfo.points[k].position;
                k++;
            }
        }
        var triangles = new Triangles(posArray);
        currentMeshInfo.mesh = triangles.MakeMesh();
        currentMeshFilter.mesh = currentMeshInfo.mesh;
    }
}


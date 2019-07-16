using System;
using System.Collections.Generic;
using UnityEngine;

public class LaserMeshGenerator : MonoBehaviour
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class Point
    {
        public Vector3 direction;
        public Vector3 position;
        public float speed;
		public float intervalSpeed;

		public Point(Vector3 direction, Vector3 position, float speed, float intervalSpeed)
        {
            this.direction = direction;
            this.position = position;
            this.speed = speed;
			this.intervalSpeed = intervalSpeed;
        }

        public void Move()
        {
            this.position += direction * speed * Time.deltaTime;
			this.speed += intervalSpeed * Time.deltaTime;
        }
    }

	/// <summary>
	/// 
	/// </summary>
    public class Triangles
    {
        public Vector3[] vertices;
		public Vector2[] uvs;
        public int[] trianglesVerticesID;

		public Triangles(Vector3 startPos, Vector3[] tempVertices)
        {

            vertices = new Vector3[tempVertices.Length + 1];
            trianglesVerticesID = new int[(vertices.Length - 2) * 3];
			uvs = new Vector2[vertices.Length];

			this.vertices[0] = startPos;

            for(var i = 0; i < tempVertices.Length; ++i)
            {
                this.vertices[i + 1] = tempVertices[i];
            }

			for (var i = 0; i < this.vertices.Length - 2; ++i)
		    {
				uvs[0]     = new Vector2(1, 0);
				uvs[i + 1] = new Vector2(0, 1);
				uvs[i + 2] = new Vector2(1,1);

				trianglesVerticesID[3 * i + 0] = 0;
				trianglesVerticesID[3 * i + 1] = i + 2;
				trianglesVerticesID[3 * i + 2] = i + 1;
			}
        }

        public Mesh MakeMesh()
        {
            var mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = trianglesVerticesID;
			mesh.uv = uvs;
            return mesh;
        }
    }

	/// <summary>
	/// 
	/// </summary>
    public class MeshInfo
    {
        public List<Point> points;
        public Mesh mesh;

        public MeshInfo()
        {
            points = new List<Point>();
            mesh = new Mesh();
        }

        public void Update()
        {
            for(var i = 0; i < points.Count; ++i)
            {
                points[i].Move();
            }
        }
    }

	/// <summary>
	/// 
	/// </summary>
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
	public float laserintervalSpeed = 60f;

	public Material laserMaterial;
	public Material laserMaterial1;
	private MeshFilter currentMeshFilter;
    private MeshInfo currentMeshInfo;
    private Controller controller;
    private List<MeshInfo> hasCreatedMeshs;
    private List<MeshFilter> hasCreatedLasers;

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
         }
         if(Input.GetKey(KeyCode.DownArrow))
         {
             controller.Rotate(Mathf.PI / 12.0f);
         }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateLaserInstance();
        }

        if(Input.GetKey(KeyCode.Space))
        {
            CreateNewMeshPoints();
        }

        
        for(var i = 0; i < hasCreatedMeshs.Count; ++i)
        {
            hasCreatedMeshs[i].Update();        
            DynamicChangeMeshPoints(hasCreatedMeshs[i]);
            hasCreatedLasers[i].mesh = hasCreatedMeshs[i].mesh;
        }     
    }

    private void CreateLaserInstance()
    {
        var instance = new GameObject("Laser");
        currentMeshFilter = instance.AddComponent<MeshFilter>();
        var renderer = instance.AddComponent<MeshRenderer>();

		//renderer.materials = new Material[2];
		//renderer.materials[0] = laserMaterial;
		//renderer.materials[1] = laserMaterial1;
		renderer.material = laserMaterial;


		var meshInfo = new MeshInfo();

        var tempX = 0f;
        var tempY = 0f;
        controller.pushPosition.Normalize();
        tempX += controller.pushPosition.x;
        tempY += controller.pushPosition.y;
        var newPos = new Vector3(transform.position.x + tempX, transform.position.y + tempY);
        var newDirection = controller.pushPosition;
        var newPoint = new Point(newDirection,newPos,LaserSpeed, laserintervalSpeed);
        meshInfo.points.Add(newPoint);
		meshInfo.points.Add(newPoint);

		var triangles = new Triangles(transform.position, new Vector3[2]{newPoint.position, newPoint.position });
        meshInfo.mesh = triangles.MakeMesh();
        currentMeshInfo = meshInfo;
        currentMeshFilter.mesh = currentMeshInfo.mesh;
        hasCreatedMeshs.Add(meshInfo);
        hasCreatedLasers.Add(currentMeshFilter);
    }

    private void CreateNewMeshPoints()
    {
        var tempX = 0f;
        var tempY = 0f;

        controller.pushPosition.Normalize();
        tempX += controller.pushPosition.x;
        tempY += controller.pushPosition.y;
        var newPos = new Vector3(transform.position.x + tempX, transform.position.y + tempY);
        var newDirection = controller.pushPosition;
        var newPoint = new Point(newDirection,newPos,LaserSpeed, laserintervalSpeed);

        currentMeshInfo.points.Add(newPoint);

        DynamicChangeMeshPoints(currentMeshInfo);
    }

    private void DynamicChangeMeshPoints(MeshInfo meshInfo)
    {
        var posArray = new Vector3[meshInfo.points.Count ];
        for(int i = 0; i < meshInfo.points.Count; ++i)
        {
            posArray[i] = meshInfo.points[i].position;
        }
        var triangles = new Triangles(transform.position, posArray);
        meshInfo.mesh = triangles.MakeMesh();
        currentMeshFilter.mesh = meshInfo.mesh;
    }
}


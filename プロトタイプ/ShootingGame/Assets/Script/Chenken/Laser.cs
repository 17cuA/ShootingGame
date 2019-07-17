using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChenkenLaser
{ 
     [RequireComponent(typeof(LineRenderer))]
     public class Laser : MonoBehaviour
     {
         public bool IsFixedPos{ get; set;}

         private float lineWidth;
         public float LineWidth
         {
             get
             {
                 return lineWidth;
             }
             set
             {
                 lineWidth = value;
             }
         }

         private Material laserMaterial;
         public Material LaserMaterial
         {
             get
             {
                 return laserMaterial;
             }
             set
             {
                 laserMaterial = value;
             }
         }

         private GameObject laserNode;
         public GameObject LaserNode
         {
             get
             {
                 return laserNode;
             }
             set
             {
                 laserNode = value;
             }
         }
         private LineRenderer lineRenderer;
         private List<GameObject> laserNodes;

         private void Awake()
         {
             this.lineRenderer  = GetComponent<LineRenderer>();
             this.laserNodes   = new List<GameObject>();
         }

        private void Start()
        {
             this.lineRenderer.material   = this.laserMaterial;
             this.lineRenderer.startWidth = this.lineWidth;
             this.lineRenderer.endWidth   = this.lineWidth;
        }

        private void Update()
         {

             if(laserNodes.Count > 2)
             { 
                if(this.IsFixedPos)
                { 
                     this.SetLaserRenderer (new Vector3(laserNodes[0].transform.position.x,transform.position.y, 0)
                                          , new Vector3(laserNodes[laserNodes.Count - 1].transform.position.x,transform.position.y,0));
                }
                else
                {
                    this.SetLaserRenderer (new Vector3(laserNodes[0].transform.position.x,laserNodes[0].transform.position.y, 0)
                                         , new Vector3(laserNodes[laserNodes.Count - 1].transform.position.x,laserNodes[laserNodes.Count - 1].transform.position.y,0));
                }
             }

             for(var i = 0; i < this.laserNodes.Count; ++i)
             {
                 if(!this.laserNodes[i].gameObject.activeSelf)
                 {
                     this.laserNodes.Remove(laserNodes[i]);
                     if(this.laserNodes.Count == 0)
                     {
                          this.gameObject.SetActive(false);
                     }
                 }
                 else
                 {
                    if(this.IsFixedPos)
                         this.laserNodes[i].transform.position = new Vector3(this.laserNodes[i].transform.position.x,transform.position.y, 0);
                 }
             }
           
         }

         public void Launch()
         {
             var node = GameObject.Instantiate(laserNode, this.transform.position , Quaternion.identity);
             this.laserNodes.Add(node);
         }

         public void AddLaserNode(GameObject node)
         {
             this.laserNodes.Add(node);
         }

         public void SetLaserRenderer(Vector3 start, Vector3 end)
         {
             this.lineRenderer.SetPosition(0, start);
             this.lineRenderer.SetPosition(1, end);
         }

         public void ResetLineRenderer()
         {
             this.lineRenderer.positionCount = 2;       
         }
     }
}

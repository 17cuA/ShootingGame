using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Line_Beam : MonoBehaviour {

    float timer = 0.0f;
    float effectDisplayTime = 0.2f;
    float range = 100.0f;
    Ray shotRay;
    RaycastHit shotHit;  
    ParticleSystem beamParticle;
    LineRenderer lineRenderer;
    private Renderer Target_Renderer;
    // Use this for initialization
    void Awake () {
        beamParticle = GetComponent<ParticleSystem> ();
        lineRenderer = GetComponent<LineRenderer> ();
        Target_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        if (Input.GetMouseButton (1)) {
            shot ();
        }
        if (timer >= effectDisplayTime)
        {
            disableEffect();
        }
    }

    private void shot(){
        timer = 0f;
        beamParticle.Stop ();
        beamParticle.Play ();
        lineRenderer.enabled = true;
        lineRenderer.SetPosition (0, transform.position);
        shotRay.origin = transform.position;
        shotRay.direction = transform.forward;

        //int layerMask = 0;
        int layerMask = LayerMask.GetMask("Enemy");
        if(Physics.Raycast(shotRay , out shotHit , range , layerMask))
        {
             //Destroy(shotHit.transform.gameObject);

        }
        lineRenderer.SetPosition(1 , shotRay.origin + shotRay.direction * range);


    }

    private void disableEffect()
    {
        beamParticle.Stop();
        lineRenderer.enabled = false;
    }



}
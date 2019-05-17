using UnityEngine;
using System.Collections;

public class Beam_Hit: MonoBehaviour {

    float timer = 0.0f;
    float effectDisplayTime = 0.1f;
    float range = 100.0f;
    Ray shotRay;
    RaycastHit shotHit;  
    ParticleSystem beamParticle;

    // Use this for initialization
    void Awake () {
        beamParticle = GetComponent<ParticleSystem> ();
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        if (Input.GetMouseButton (1)) {
            shot ();
        }
        if (timer >= effectDisplayTime) {
            disableEffect ();
        }
    }

    private void shot(){
        timer = 0f;
        beamParticle.Stop ();
        beamParticle.Play ();
        shotRay.origin = transform.position;
        shotRay.direction = transform.forward;

        int layerMask = 0;
        if(Physics.Raycast(shotRay , out shotHit , range , layerMask)){
            // hit 
        }
    }

    private void disableEffect(){
        beamParticle.Stop ();
    }
}
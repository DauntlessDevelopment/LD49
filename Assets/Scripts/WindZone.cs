using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    private Vector3 force = Vector3.right * 0.05f;
    //public Vector3 wind_direction;
    // Start is called before the first frame update
    void Start()
    {
        force = transform.rotation * force;
        //GetComponentInChildren<ParticleSystem>().startRotation =  transform.rotation.y - 90;
        //GetComponentInChildren<ParticleSystem>().main
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().wind_force = force;
        }
    }

}

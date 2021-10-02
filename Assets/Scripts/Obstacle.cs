using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float lifespan = 8f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StuckObjectCleanup());
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.normalized * 0.01f);
                
        }
    }

    IEnumerator StuckObjectCleanup()
    {
        yield return new WaitForSeconds(1);
        float life_remaining = lifespan;
        while(true)
        {
            if(GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
            {
                Destroy(gameObject);
            }
            float interval = 0.5f;
            yield return new WaitForSeconds(interval);
            life_remaining -= interval;
            if(life_remaining <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

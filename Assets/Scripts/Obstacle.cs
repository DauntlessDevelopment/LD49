using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
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
        while(true)
        {
            if(GetComponent<Rigidbody>().velocity.magnitude < 0.2f)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}

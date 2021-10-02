using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
    private float lifespan = 1.5f;
    private bool degraded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -50f)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Degrade()
    {
        degraded = true;
        yield return new WaitForSeconds(lifespan/3);
        GetComponent<MeshRenderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(lifespan/ 3 );
        GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(lifespan / 3);
        GetComponent<MeshRenderer>().material.color = Color.grey;

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && !degraded)
        {
            StartCoroutine(Degrade());
        }
    }
}

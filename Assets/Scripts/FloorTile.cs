using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
    private float lifespan = 1.5f;
    private bool degraded = false;
    private Vector3 init_pos;
    // Start is called before the first frame update
    void Start()
    {
        init_pos = transform.position;
        lifespan += Random.Range(-0.5f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -100f)
        {
            //Destroy(gameObject);
            //perhaps reset instead of destroy
            degraded = false;
            GetComponent<MeshRenderer>().material.color = Color.white;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.position = init_pos;

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

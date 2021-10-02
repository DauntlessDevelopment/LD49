using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnd : MonoBehaviour
{
    public GameObject next_stage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            float player_height = collision.transform.position.y;
            collision.transform.position = next_stage.transform.position;
            collision.transform.position = new Vector3(collision.transform.position.x, player_height, collision.transform.position.z);
        }
    }
}

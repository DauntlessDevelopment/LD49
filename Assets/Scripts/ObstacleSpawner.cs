using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    private float spawn_timer = 3f;
    public Obstacle obstacle_prefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            Instantiate(obstacle_prefab.gameObject, transform.position, obstacle_prefab.transform.rotation);
            yield return new WaitForSeconds(spawn_timer);
        }
    }
}

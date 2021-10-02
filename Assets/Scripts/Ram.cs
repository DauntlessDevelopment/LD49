using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ram : MonoBehaviour
{
    private float ram_speed = 1f;
    private float ram_rate = 1f;
    private bool ram_ready = true;
    private bool ramming = false;
    private bool retracting = false;

    private float distance_moved = 0f;
    private Vector3 init_pos;

    [SerializeField] private float max_distance = 2f;
    // Start is called before the first frame update
    void Start()
    {
        init_pos = transform.position;
        ram_speed = Random.Range(0.1f, 10f);
        ram_rate = Random.Range(0.3f, 3f);
        max_distance = Random.Range(1, max_distance);
        StartCoroutine(Run());
    }

    // Update is called once per frame
    void Update()
    {
        if(ramming)
        {
            if(distance_moved >= max_distance)
            {
                ramming = false;
                retracting = true;
            }
            else
            {
                transform.Translate(-transform.right * Time.deltaTime * ram_speed);
                distance_moved += Time.deltaTime * ram_speed;
            }
        }
        if(retracting)
        {
            if(distance_moved <= 0)
            {
                transform.position = init_pos;
                distance_moved = 0;
                retracting = false;
                ram_ready = true;
            }
            else
            {
                transform.Translate(transform.right * Time.deltaTime * ram_speed);
                distance_moved -= Time.deltaTime * ram_speed;
            }
        }
    }

    IEnumerator Run()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            if(ram_ready)
            {
                yield return new WaitForSeconds(1 / ram_rate);
                Push();
            }

        }
    }

    void Push()
    {
        ram_ready = false;
        ramming = true;
    }


}

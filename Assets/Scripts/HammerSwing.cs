using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSwing : MonoBehaviour
{
    [SerializeField]bool returning = false;
    float swing_speed = 90f;
    float swing_distance = 0f;
    bool running = false;
    public bool left_right = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        if (!running) { return; }
        if(!returning)
        {
            if(swing_distance >= 90)
            {
                returning = true;
            }
            else
            {
                if(!left_right)
                {
                    transform.Rotate(Vector3.right, swing_speed * Time.deltaTime);
                }
                else
                {
                    transform.Rotate(Vector3.forward, swing_speed * Time.deltaTime);
                }
                swing_distance += swing_speed * Time.deltaTime;
            }
        }
        else
        {
            if (swing_distance <= -90)
            {
                returning = false;
                
            }
            else
            {
                if(!left_right)
                {
                    transform.Rotate(Vector3.right, -swing_speed * Time.deltaTime);
                }
                else
                {
                    transform.Rotate(Vector3.forward, -swing_speed * Time.deltaTime);

                }
                swing_distance -= swing_speed * Time.deltaTime;


            }
        }

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(Random.Range(0, 3));
        running = true;
    }
}

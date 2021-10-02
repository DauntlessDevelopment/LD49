using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public bool clockwise = true;
    [SerializeField] private float speed = 45f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(clockwise)
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.up, -speed * Time.deltaTime);
        }
    }
}

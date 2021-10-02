using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private float move_speed = 10f;
    [SerializeField] private float turn_speed = 360f;
    [SerializeField] private GameObject head;

    public Vector3 wind_force = new Vector3();

    private float timer = 0;
    private Vector3 init_pos;

    [SerializeField] private Text timer_text;
    // Start is called before the first frame update
    void Start()
    {
        init_pos = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        if(transform.position.y < -10f)
        {
            //SceneManager.LoadScene(1);
            transform.position = init_pos;
        }
        
 
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        timer_text.text = timer.ToString();
        //GetComponent<Rigidbody>().velocity += wind_force;
        wind_force *= 0.8f;
        transform.Translate(wind_force, Space.World);
    }

    private void HandleInput()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            transform.Translate(transform.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime * move_speed, Space.World);
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.Translate(transform.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * move_speed, Space.World);
        }
        if (Input.GetAxisRaw("Vertical2") != 0)
        {
            head.transform.Rotate(Vector3.right, Input.GetAxisRaw("Vertical2") * Time.deltaTime * turn_speed);
        }
        if (Input.GetAxisRaw("Horizontal2") != 0)
        {
            transform.Rotate(transform.up, Input.GetAxisRaw("Horizontal2") * Time.deltaTime * turn_speed);
        }
        if(Input.GetButtonDown("Jump") && Physics.Raycast(transform.position, -transform.up, 1.5f))
        {
            GetComponent<Rigidbody>().AddForce(transform.up * 5f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            //SceneManager.LoadScene(0);
        }
    }


}

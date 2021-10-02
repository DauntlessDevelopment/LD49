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

    private PlayerStats data;

    public Vector3 wind_force = new Vector3();

    private float timer = 0;
    private Vector3 init_pos;

    [SerializeField] private Text timer_text;
    [SerializeField] private Text stats_text;

    private int death_count = 0;
    private Transform last_checkpoint;

    public GameObject end_popup;

    [SerializeField] int checkpoint = 0;
    private bool ironman;

    private void Awake()
    {
        Time.timeScale = 1;
        data = new PlayerStats();
    }
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

        if (transform.position.y < -10f)
        {
            //SceneManager.LoadScene(1);
            transform.position = last_checkpoint.position + new Vector3(0,transform.localScale.y, 0);
            death_count++;
            data.total_deaths++;
        }
        
 
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        string time_string = timer.ToString();
        int i = 0;
        for(int j = 0; j < time_string.Length; j++)
        {
            char c = time_string[j];
            if (c == '.')
            {
                i = j;
                break;
            }
        }

        time_string = time_string.Substring(0, Mathf.Min(i + 3, time_string.Length));

        timer_text.text = time_string;
        //GetComponent<Rigidbody>().velocity += wind_force;
        wind_force *= 0.8f;
        transform.Translate(wind_force, Space.World);
    }

    private void HandleInput()
    {
        //Vector3 new_vel = GetComponent<Rigidbody>().velocity;
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            transform.Translate(transform.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime * move_speed, Space.World);
            
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.Translate(transform.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * move_speed, Space.World);
        }
        //new_vel.x = Input.GetAxisRaw("Horizontal")  * move_speed;
        //new_vel.z = Input.GetAxisRaw("Vertical")  * move_speed;
        //new_vel = transform.rotation * new_vel;
        //Debug.Log(new_vel);
        //GetComponent<Rigidbody>().velocity = new_vel;
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
        if(collision.gameObject.tag == "Checkpoint" && collision.transform != last_checkpoint)
        {
            last_checkpoint = collision.transform;
            checkpoint++;
        }
        if (collision.gameObject.tag == "Bounce")
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 3f);
            GetComponentInChildren<AudioSource>().Play();
        }

        //if(collision.gameObject.tag == "Bounce")
        //{
        //    if(Time.timeSinceLevelLoad > last_bounce + 3f)
        //    {
        //        bounce_count = 0;
        //    }
        //    if(bounce_count == 0)
        //    {
        //        GetComponent<Rigidbody>().AddForce(Vector3.Reflect(GetComponent<Rigidbody>().velocity, Vector3.up) * 10f);
        //    }
        //    else
        //    {
        //        GetComponent<Rigidbody>().AddForce((transform.forward + Vector3.up) * 10f);
        //    }
        //    last_bounce = Time.timeSinceLevelLoad;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish" && checkpoint > 1)
        {
            //play victory sound
            //freeze timer
            //save time
            //show menu
            stats_text.text = $"{timer}\n{death_count}\nN/A";
            if (ironman)
            {
                if (timer < data.fastest_ironman)
                {
                    data.fastest_ironman = timer;
                    data.perfect_runs++;
                }
            }
            else
            {
                if (timer < data.fastest_time)
                {
                    data.fastest_time = timer;
                }
            }

            Debug.LogError("DONE");
            end_popup.SetActive(true);
            Time.timeScale = 0;
        }
    }


    //Sound Effects Needed:
    //Soft Impact
    //Hard/Stone Impact
    //Jump
    //Step (soft&hard)
    //Victory sound
    //Checkpoint sound
    //Death sound
    //Falling sound


    //Additional Needs:
    //Choice of two gamemodes
    //Saving and loading of data
    //Music?
    //Polish UI/Menu
    //Navigate Menus with controller????????
    //Add pause menu
}

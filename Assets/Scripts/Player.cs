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

    public Selectable end_screen_default;
    public Selectable pause_screen_default;

    [SerializeField] int checkpoint = 0;
    private bool ironman;


    [SerializeField] private List<AudioClip> jump_sounds = new List<AudioClip>(6);
    [SerializeField] private List<AudioClip> soft_impact_sounds = new List<AudioClip>(3);
    [SerializeField] private List<AudioClip> hard_impact_sounds = new List<AudioClip>(4);
    [SerializeField] private AudioClip victory_sound;
    [SerializeField] private AudioClip checkpoint_sound;
    [SerializeField] private AudioClip death_sound;
    [SerializeField] private AudioClip bounce_sound;
    [SerializeField] private Canvas pause_menu;

    private float moved_distance = 0f;

    private void Awake()
    {
        Time.timeScale = 1;
        data = GameObject.Find("DataContainer").GetComponent<DataContainer>().player_data;
        ironman = GameObject.Find("DataContainer").GetComponent<DataContainer>().ironman;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Awake");
    }
    // Start is called before the first frame update
    void Start()
    {
        init_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        if (transform.position.y < -7f && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(death_sound);
        }
        if(transform.position.y < -15f)
        {
            if(ironman)
            {
                transform.position = init_pos;
            }
            else
            {
                transform.position = last_checkpoint.position + new Vector3(0, transform.localScale.y, 0);
            }
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

    public void Pause()
    {
        if(!end_popup.activeSelf)
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pause_menu.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Time.timeScale = 0;
                pause_menu.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                pause_screen_default.Select();
            }
        }


    }

    private void HandleInput()
    {
        if(Input.GetButtonDown("Pause"))
        {
            Pause();
        }

        if (Input.GetAxisRaw("Vertical") != 0)
        {
            transform.Translate(transform.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime * move_speed, Space.World);
            moved_distance += Input.GetAxisRaw("Vertical") * Time.deltaTime * move_speed;
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.Translate(transform.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * move_speed, Space.World);
            moved_distance += Input.GetAxisRaw("Horizontal") * Time.deltaTime * move_speed;
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

            int i = Random.Range(0, jump_sounds.Count);
            GetComponent<AudioSource>().PlayOneShot(jump_sounds[i]);
        }

        bool hard_surface = true;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, 1.5f, 2))
        {
            if(hit.transform.tag == "Hard")
            {
                hard_surface = true;
            }
            else
            {
                hard_surface = false;
            }


            if (moved_distance > 2)
            {
                if (hard_surface)
                {
                    int i = Random.Range(0, hard_impact_sounds.Count);
                    GetComponentInChildren<AudioSource>().PlayOneShot(hard_impact_sounds[i]);
                }
                else
                {
                    int i = Random.Range(0, soft_impact_sounds.Count);
                    GetComponentInChildren<AudioSource>().PlayOneShot(soft_impact_sounds[i]);
                }

                moved_distance = 0;

            }
        }



    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Checkpoint" && collision.transform != last_checkpoint)
        {
            last_checkpoint = collision.transform;
            checkpoint++;
            if(checkpoint > 1)
            {
                GetComponent<AudioSource>().PlayOneShot(checkpoint_sound);
            }

        }
        if (collision.gameObject.GetComponent<Collider>().material.bounciness == 1)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 3f);
            GetComponentInChildren<AudioSource>().PlayOneShot(bounce_sound);
        }

        if(collision.gameObject.tag == "Hard")
        {
            int i = Random.Range(0, hard_impact_sounds.Count);
            GetComponentInChildren<AudioSource>().PlayOneShot(hard_impact_sounds[i]);
        }
        else
        {
            int i = Random.Range(0, soft_impact_sounds.Count);
            GetComponentInChildren<AudioSource>().PlayOneShot(soft_impact_sounds[i]);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish" && checkpoint > 1)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(victory_sound);
            Debug.Log("GameEnd");
            if (ironman)
            {
                stats_text.text = $"{timer}\n\n{death_count}\n\n{data.fastest_ironman}";
                Debug.Log("Ironman Enabled");
                Debug.Log($"Previous Time : {data.fastest_ironman} | Current Time : {timer}");
                if (timer < data.fastest_ironman || data.fastest_ironman == 0)
                {
                    Debug.Log("Ironman Time Beaten");
                    data.fastest_ironman = timer;
                    data.perfect_runs++;
                }

            }
            else
            {
                stats_text.text = $"{timer}\n\n{death_count}\n\n{data.fastest_time}";
                Debug.Log("Ironman Disabled");
                Debug.Log($"Previous Time : {data.fastest_time} | Current Time : {timer}");
                if (timer < data.fastest_time || data.fastest_time == 0)
                {
                    Debug.Log("Normal Time Beaten");
                    data.fastest_time = timer;
                }
            }

            Debug.LogError("DONE");
            end_popup.SetActive(true);
            end_screen_default.Select();
            SaveSystem.Save(data);
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
        }
    }

    //Additional Needs:
    //Music?
    //Polish UI/Menu
    //4th stage?

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class GameManager : MonoBehaviour
{


    public AudioMixer music_mixer;
    public AudioMixer sfx_mixer;
    // Start is called before the first frame update
    void Start()
    {

        
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetMusicVolume(float val)
    {
        music_mixer.SetFloat("MusicVolume", Mathf.Log10(val) * 20);
    }
    public void SetSFXVolume(float val)
    {
        sfx_mixer.SetFloat("MusicVolume", Mathf.Log10(val) * 20);
    }
}

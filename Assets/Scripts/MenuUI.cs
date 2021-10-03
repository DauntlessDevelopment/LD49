using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public DataContainer data_container;
    [SerializeField] private Text stats_text;
    [SerializeField] private Text ironman_button_text;



    public Selectable play;
    public Canvas help_canvas;

    public Selectable help_return;
    // Start is called before the first frame update
    void Start()
    {
        RefreshUI();
        play.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshUI()
    {
        stats_text.text = $"{data_container.player_data.fastest_time}\n{data_container.player_data.fastest_ironman}\n{data_container.player_data.total_deaths}\n{data_container.player_data.perfect_runs}";
        if(data_container.ironman)
        {
            ironman_button_text.text = "Enabled";
        }
        else
        {
            ironman_button_text.text = "Disabled";
        }
    }

    public void ClearData()
    {
        data_container.ClearData();
        RefreshUI();
    }

    public void ToggleIronman()
    {
        data_container.ironman = !data_container.ironman;
        RefreshUI();
    }

    public void HelpToggle()
    {
        help_canvas.gameObject.SetActive(!help_canvas.gameObject.activeSelf);
        if(help_canvas.gameObject.activeSelf)
        {
            help_return.Select();
        }
        else
        {
            play.Select();
        }
    }
}

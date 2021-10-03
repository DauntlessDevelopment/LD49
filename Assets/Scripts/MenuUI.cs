using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public DataContainer data_container;
    [SerializeField] private Text stats_text;
    [SerializeField] private Text ironman_button_text;

    //public List<Selectable> buttons = new List<Selectable>();
    //int button_index = 0;

    public Selectable play;

    // Start is called before the first frame update
    void Start()
    {
        RefreshUI();
        play.Select();
    }

    // Update is called once per frame
    void Update()
    {
        //if(buttons.Count > 0)
        //{
        //    if (Input.GetAxisRaw("Vertical") > 0)
        //    {
        //        if (button_index == 0)
        //        {
        //            button_index = buttons.Count - 1;
        //        }
        //        else
        //        {
        //            button_index -= 1;
        //        }
        //        buttons[button_index].Select();
        //    }
        //    if (Input.GetAxisRaw("Vertical") < 0)
        //    {
        //        if (button_index == buttons.Count - 1)
        //        {
        //            button_index = 0;
        //        }
        //        else
        //        {
        //            button_index += 1;
        //        }
        //        buttons[button_index].Select();

        //    }
        //}
        
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
}

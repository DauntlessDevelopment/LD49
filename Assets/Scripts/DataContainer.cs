using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{

    public PlayerStats player_data = new PlayerStats();
    public bool ironman = false;

    private void Awake()
    {
        player_data = SaveSystem.Load();
        DontDestroyOnLoad(gameObject);
        if(FindObjectsOfType<DataContainer>().Length > 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void ClearData()
    {
        player_data = new PlayerStats();
        SaveSystem.Save(player_data);
    }
}

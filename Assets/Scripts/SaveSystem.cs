using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static void Save(PlayerStats data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/unstable.l49";
        FileStream fs = new FileStream(path, FileMode.Create);

        bf.Serialize(fs, data);
        fs.Close();
    }

    public static PlayerStats Load()
    {
        string path = Application.persistentDataPath + "/unstable.l49";
        if(File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            PlayerStats data = bf.Deserialize(fs) as PlayerStats;
            fs.Close();
            return data;
        }
        else
        {
            Debug.Log("Could not find save file at location: " + path);
            return new PlayerStats();
        }
    }
}

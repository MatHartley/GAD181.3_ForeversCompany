using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 lastCheckPointPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveData()
    {
        Vector2 checkpointPos = lastCheckPointPos;
        string checkpointPosString = checkpointPos.x + "," + checkpointPos.y;
        PlayerPrefs.SetString("LastCheckpoint", checkpointPosString);
        Debug.Log("DataSaved" + "Last save point is" + checkpointPosString);
    }

    public void LoadSavedData()
    {
        string checkpointPosString = PlayerPrefs.GetString("LastCheckpoint");
        string[] components = checkpointPosString.Split(',');

        if (components.Length == 2)
        {
            float x = float.Parse(components[0]);
            float y = float.Parse(components[1]);
            lastCheckPointPos = new Vector2(x, y);
        }
    }
}

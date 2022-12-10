using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : ControllerBase
{
    private string SAVE_PATH = "";
    private readonly string SAVE_FILENAME = "/SaveFile.txt";

    private User user;
    public User CurrentUser { get { return user; } }

    public override void OnAwake()
    {
        FirstData();
    }

    private void FirstData()
    {
        SAVE_PATH = Application.dataPath + "/Save";

        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }

        LoadFromJson();
    }

    private void LoadFromJson()
    {
        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            string json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
        }
        else
        {
            SaveToJson();
            LoadFromJson();
        }
    }

    public void SaveToJson()
    {
        SAVE_PATH = Application.dataPath + "/Save";

        if (user == null)
        {
            UserSetting();
        }

        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }

    private void UserSetting()
    {
        //user.deck = 
    }
}

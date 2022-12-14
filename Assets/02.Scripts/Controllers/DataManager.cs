using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : ControllerBase
{
    private string SAVE_PATH = "";
    private readonly string SAVE_FILENAME = "/SaveFile.txt";

    private User user;
    public User CurrentUser { get { if (user == null) FirstData(); return user; } }

    public override void OnAwake()
    {
        FirstData();
    }

    private void FirstData()
    {
        SAVE_PATH = Application.dataPath + "/Save";

        if (!Directory.Exists(SAVE_PATH))
        {
            Debug.Log("Create");
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
            user = new User();
            UserSetting();
        }

        string json = JsonUtility.ToJson(user, true);

        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }

    private void UserSetting()
    {
        user.inventory = Resources.Load<Dices>("Inventory").dices;
        user.deck = Resources.Load<Dices>("Deck").dices;
    }
}
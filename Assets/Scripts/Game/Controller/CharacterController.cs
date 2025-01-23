using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
    [SerializeField] GameObject[] character;

    private void Awake()
    {
        User currentUser = UserManager.instance.currentUser;
        int islandType = 0;

        if (currentUser != null)
            islandType = currentUser.JobType;

        for (int i = 0; i < character.Length; i++)
            character[i].SetActive(i == islandType);

        Load(currentUser.Costume, islandType);
        GetPlayerCustomizer().LoadPlayerCostume();
    }

    public PlayerController GetPlayerController()
    {
        User currentUser = UserManager.instance.currentUser;
        int idx = 0;

        if (currentUser != null)
            idx = currentUser.JobType;

        return character[idx].GetComponent<PlayerController>();
    }

    public PlayerCustomizer GetPlayerCustomizer()
    {
        User currentUser = UserManager.instance.currentUser;
        int idx = 0;

        if (currentUser != null)
            idx = currentUser.JobType;

        return character[idx].GetComponent<PlayerCustomizer>();
    }

    public Player GetPlayer()
    {
        User currentUser = UserManager.instance.currentUser;
        int idx = 0;

        if (currentUser != null)
            idx = currentUser.JobType;

        return character[idx].GetComponent<Player>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Keypad0))
            Save();

        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            string costume = "0,0-1:1-0:2-0:3-0:4-0:5-0:6-0:7-0:8-0,0-1:1-0:2-0:3-0:4-0:5-0:6-0:7-0:8-0:9-0,0-1:1-0:2-0,0-1:1-0:2-0:3-0/1,0-1:1-0:2-0:3-0:4-0:5-0:6-0:7-0,0-1:1-0:2-0:3-0:4-0:5-0:6-0:7-0:8-0:9-0,0-1:1-0:2-0,0-1:1-0:2-0:3-0/2,0-1:1-0:2-0:3-0:4-0:5-0,0-1:1-0:2-0:3-0:4-0:5-0:6-0:7-0:8-0,0-1:1-0:2-0,0-1:1-0:2-0:3-0/3,0-1:1-0:2-0:3-0:4-0:5-0,0-1:1-0:2-0:3-0:4-0:5-0:6-0:7-0:8-0:9-0:10-0:11-0,0-1:1-0:2-0,0-1:1-0:2-0:3-0";
            Load(costume, 0);
        }
#endif
    }

    public void Save()
    {
        string costume = string.Empty;

        for(int i = 0; i < character.Length; i++)
        {
            PlayerCustomizer customizer = character[i].GetComponent<PlayerCustomizer>();
            costume += i.ToString() + ",";

            SaveParseData(ref costume, customizer.avatarData);

            costume += ",";

            SaveParseData(ref costume, customizer.headGearData);

            costume += ",";

            SaveParseData(ref costume, customizer.skinColorData);

            costume += ",";

            SaveParseData(ref costume, customizer.clotesStyleData);

            if (i < character.Length - 1)
                costume += "/";
        }

        Debug.LogError("Costume " + costume);

        UserManager.instance.SaveCostume(costume);
    }

    public void Load(string costume, int idx)
    {
        Debug.LogError("Load Costume " + costume);
        if (string.IsNullOrEmpty(costume))
            return;

        string[] jobtype = costume.Split("/");

        PlayerCustomizer customizer = character[idx].GetComponent<PlayerCustomizer>();

#if UNITY_EDITOR
        // Test costume string
        //string costume = "0,0-1:1-0:2-0:3-0:4-0:5-0:6-0:7-0:8-0,0-1:1-0:2-0:3-0:4-0:5-0:6-0:7-0:8-0:9-0,0-1:1-0:2-0,0-1:1-0:2-0:3-0/1,0-1:1-0:2-0:3-0:4-0:5-0:6-0:7-0,0-1:1-0:2-0:3-0:4-0:5-0:6-0:7-0:8-0:9-0,0-1:1-0:2-0,0-1:1-0:2-0:3-0/2,0-1:1-0:2-0:3-0:4-0:5-0,0-1:1-0:2-0:3-0:4-0:5-0:6-0:7-0:8-0,0-1:1-0:2-0,0-1:1-0:2-0:3-0/3,0-1:1-0:2-0:3-0:4-0:5-0,0-1:1-0:2-0:3-0:4-0:5-0:6-0:7-0:8-0:9-0:10-0:11-0,0-1:1-0:2-0,0-1:1-0:2-0:3-0";

        /* string student = jobtype[0];
         string professional = jobtype[1];
         string farm = jobtype[2];
         string business = jobtype[3];*/
#endif

        string[] jobTypeData = jobtype[idx].Split(",");
        string[] avatarData = jobTypeData[1].Split(":");
        string[] headGearData = jobTypeData[2].Split(":");
        string[] skinColorData = jobTypeData[3].Split(":");
        string[] styleData = jobTypeData[4].Split(":");

        LoadParseData(avatarData, customizer.avatarData);
        LoadParseData(headGearData, customizer.headGearData);
        LoadParseData(skinColorData, customizer.skinColorData);
        LoadParseData(styleData, customizer.clotesStyleData);
    }

    void SaveParseData(ref string costume, CustomItemSO[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            string idx = "0";

            if (data[i].isLock)
                idx = "0";
            else
            {
                if (data[i].isSelected)
                    idx = "1";
                else
                    idx = "2";
            }

            costume += i.ToString() + "-" + idx;

            if (i < data.Length - 1)
                costume += ":";
        }
    }

    void LoadParseData(string[] stringdata, CustomItemSO[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            string[] value = stringdata[i].Split("-");

            string idx = value[0];
            string val = value[1];

            if (int.Parse(idx) == i)
            {
                int valval = int.Parse(val);
                data[i].isLock = valval == 0 ? true : false;
                if (valval > 0)
                    data[i].isSelected = valval == 1 ? true : false;
                else
                    data[i].isSelected = false;
            }
        }
    }
}

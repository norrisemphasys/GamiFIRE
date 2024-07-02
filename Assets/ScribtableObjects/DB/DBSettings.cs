using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/Settings/DBSetting", order = 1)]
public class DBSettings : ScriptableObject
{
    public string DB_URL;
    public string USER;
}

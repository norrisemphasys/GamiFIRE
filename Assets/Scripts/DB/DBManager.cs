using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.Events;

public class DBManager : MonoSingleton<DBManager>
{
    public DBSettings setting;

    public static void GetUser(User user, UnityAction<User> callback = null)
    {
        string DB_USER_URL = DBManager.instance.setting.DB_URL + 
            DBManager.instance.setting.USER;

        RestClient.Get<User>(DB_USER_URL + user.Username + ".json").Then(res =>
        {
            Debug.Log("Get user response");
            callback(res);
        });
    }

    public static void AddNewUser(User user) 
    {
        string DB_USER_URL = DBManager.instance.setting.DB_URL + 
            DBManager.instance.setting.USER;

        RestClient.Put<User>(DB_USER_URL + user.Username + ".json", user).Then(res=>
        {
            Debug.Log("New user added");
        });
    }
}

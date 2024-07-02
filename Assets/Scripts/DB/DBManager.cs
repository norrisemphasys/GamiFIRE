using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.Events;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using Unity.VisualScripting.FullSerializer;

public class DBManager : MonoSingleton<DBManager>
{
    public static fsSerializer serializer = new fsSerializer();
    public static void GetUserByObject(User user, UnityAction<User> callback = null)
    {
        RestClient.Get<User>(GameConstants.USERS_DB_URL + "/" + user.Username + ".json").Then(res =>
        {
            Debug.Log("Get user response");
            callback?.Invoke(res);
        }).Catch(err => 
        {
            Debug.Log("Error " + err.Message);
            callback?.Invoke(null);
        });
    }

    public static void GetUserByName(string name, UnityAction<User> callback = null)
    {
        RestClient.Get<User>(GameConstants.USERS_DB_URL + "/" + name + ".json").Then(res =>
        {
            Debug.Log("Get user response");
            callback?.Invoke(res);
        }).Catch(err =>
        {
            Debug.Log("Error " + err.Message);
            callback?.Invoke(null);
        });
    }

    public static void GetUserByEmail(string email , UnityAction<User> callback = null)
    {
        GetAllUsers((res) =>
        {
            if(res != null)
            {
                Debug.Log("User by email ");
                User user = Array.Find(res, x => x.Email.Equals(email));
                callback?.Invoke(user);
            }
        });
    }

    public static void GetAllUsers(UnityAction<User[]> callback = null)
    {
        RestClient.Get(GameConstants.USERS_DB_URL + ".json").Then(res =>
        {
            fsData userData = fsJsonParser.Parse(res.Text);

            Dictionary<string, User> users = null;
            serializer.TryDeserialize(userData, ref users);

            Debug.Log("All users count " + users.Count);
            callback?.Invoke(users.Values.ToArray());

        }).Catch(err =>
        {
            Debug.Log("Error " + err.Message);
            callback?.Invoke(null);
        });
    }

    public static void AddEditUser(User user, UnityAction<User> callback = null) 
    {
        RestClient.Put<User>(GameConstants.USERS_DB_URL + "/" + user.Username + ".json", user).Then(res=>
        {
            Debug.Log("New user added");
            callback?.Invoke(res);
        }).Catch(err =>
        {
            Debug.Log("Error " + err.Message);
            callback?.Invoke(null);
        });
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System;

public class DBController : MonoBehaviour
{
    [SerializeField] DBSettings setting;

    private string user = "users";

    // Start is called before the first frame update
    void Start()
    {
        /*User userTest = new User
        {
            ID = "0",
            Username = "Test",
            Email = "test@test.com",
            Password = "1233546"
        };

        RestClient.Put<User>(setting.DB_URL + userTest.Username + ".json", userTest);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

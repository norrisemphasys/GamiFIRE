using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoSingleton<UserManager>
{
    private User _currentUser;
    public User currentUser { get { return _currentUser; } }
    public void SetCurrentUser(User user) { _currentUser = user; }

    public void SaveUser()
    {

    }

    public void LoadUser()
    {
       
    }
}

public enum JobType
{
    STUDENT,
    PROFESSIONAL,
    AGRICULTRIST,
    BUSINESSMAN
}
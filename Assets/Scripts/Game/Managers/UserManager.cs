using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserManager : MonoSingleton<UserManager>
{
    private User _currentUser;
    public User currentUser { get { return _currentUser; } set { _currentUser = value; } }
    public void SetCurrentUser(User user) { _currentUser = user; }
    public void ResetUserPoints()
    {
        currentUser.Coin = 0;
        currentUser.Score = 0;
        currentUser.GrowthPoint = 0;
        currentUser.InnovationPoint = 0;
        currentUser.CurrencyPoint = 0;
        currentUser.SatisfactionPoint = 0;
    }

    public void SaveUser(UnityAction callback = null)
    {
        if (_currentUser == null)
        {
            callback?.Invoke();
            return;
        }

        DBManager.AddEditUser(_currentUser, (res) => 
        {
            SetCurrentUser(res);
            callback?.Invoke();
        });
    }

    public void LoadUser()
    {
       
    }

    public void SetGender(int idx)
    {
        _currentUser.Gender = idx;
    }

    public void SetJobType(int idx)
    {
        _currentUser.JobType = idx;
    }
}

public enum JobType
{
    STUDENT = 0,
    PROFESSIONAL,
    AGRICULTRIST,
    BUSINESSMAN
}

public enum Gender
{
    MALE = 0,
    FEMAL,
    NON_BINARY
}
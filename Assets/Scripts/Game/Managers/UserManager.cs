using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserManager : MonoSingleton<UserManager>
{
    private User _currentUser;
    public User currentUser { get { return _currentUser; } set { _currentUser = value; } }

    public UserBadge userBadge { get; set; }

    public void SetCurrentUser(User user) 
    {
        if (user == null && _currentUser != null)
            return;

        _currentUser = user; 
    }
    public void ResetUserPoints()
    {
        currentUser.GrowthPoint = 0;
        currentUser.InnovationPoint = 0;
        currentUser.CurrencyPoint = 0;
        currentUser.SatisfactionPoint = 0;

        currentUser.isAnExistingAccount = false;
    }

    public void ResetPoints()
    {
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

        DBManager.AddEditUserLocalID(_currentUser, (res) => 
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

    public static string GetJobName(JobType type) 
    {
        switch(type)
        {
            case JobType.STUDENT:
                return "HIGHER EDUCATION STUDENT";
            case JobType.PROFESSIONAL:
                return "INDEPENDENT PROFESSIONAL";
            case JobType.AGRICULTRIST:
                return "AGRICULTURIST";
            case JobType.BUSINESSMAN:
                return "COMPANY EMPLOYEE";
        }

        return "HIGHER EDUCATION STUDENT";
    }

    public void LoadCostume()
    {

    }

    public void SaveCostume(string costume)
    {
        _currentUser.Costume = costume;
    }

    void ParseCostume()
    {

    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad8))
            CreateAndEditUserData();
    }
#endif
    public void CreateAndEditUserData()
    {
        if(_currentUser != null)
        {
            currentUser.Coin = 100;
            currentUser.Score = 100;
            currentUser.GrowthPoint = 100;
            currentUser.InnovationPoint = 100;
            currentUser.CurrencyPoint = 100;
            currentUser.SatisfactionPoint = 100;

            Debug.LogError("Add points to existing user");
        }
        else
        {
            User newUser = new User
            {
                ID = System.Guid.NewGuid().ToString(),
                Username = "Test User",
                Email = "test@test.com",
                Password = Utils.GetMD5Hash("1234567890"),
                isAnExistingAccount = false,

                JobType = 0,
                Gender = 0,
                Coin = 100,
                Score = 100,
                GrowthPoint = 100,
                InnovationPoint = 100,
                CurrencyPoint = 100,
                SatisfactionPoint = 100,
                Costume = "",
                HasBadge = false
            };

            SetCurrentUser(newUser);

            Debug.LogError("Add points to new user");
        }
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
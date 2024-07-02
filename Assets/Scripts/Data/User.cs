using System;

[Serializable]
public class User
{
    public string ID;
    public string Username;
    public string Email;
    public string Password;

    public User()
    {

    }

    public User(User user)
    {
        ID = user.ID;
        Username = user.Username;
        Email = user.Email;
        Password = user.Password;
    }
}

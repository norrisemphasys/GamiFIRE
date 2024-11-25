using System;

[Serializable]
public class SignUpInfo
{
    public string idToken;
    public string localId;
}

[Serializable]
public class EmailVerificationInfo
{
    public UserInfo[] users;
}

[Serializable]
public class UserInfo
{
    public string localId;
    public string email;
    public bool emailVerified;
}
using System;
using System.Collections.Generic;

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

[Serializable]
public class Token
{
    public string token;
}

public class CredentialRequest
{
    public string offerId;
    public string userID;
    public string templateID;
    public string emailTemplateID;
    public string badgeID;

    public CredentialPayload credentialPayload;
}

public class CredentialPayload
{
    public string credentialName;
    public string earnerName;
    public string emailAddress;
}

public class Achievement
{
    public string id;
    public string type;
    public string name;

    public AchievementImage image;
    public AchievementCriteria criteria;

    public string description;
}

public class AchievementImage
{
    public string id;
    public string type;
}

public class AchievementCriteria
{
    public string narrative;
}

public class UserBadge
{
    public string userID;
    public string templateID;
    public string emailTemplateID;

    public Badge badges;
}

public class Badge
{
    public string id;
    public string name;
    public string imgURL;

    public string criteria;
    public string description;

    public string claimLink;
}

public class Survey
{
    public string id;
    public string title;
    public string answer;
}
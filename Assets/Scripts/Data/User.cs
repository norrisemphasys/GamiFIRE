using System;


[Serializable]
public class User
{
    public string ID;

    public string Username;

    public string Email;

    public string Password;

    public bool isAnExistingAccount;

    public int JobType;

    public int Gender;

    // Depricated
    public int Coin;

    public int Score;

    public int GrowthPoint;

    public int InnovationPoint;

    public int CurrencyPoint;

    public int SatisfactionPoint;

    public string Costume;

    public bool HasBadge;

    public bool IsAdministrator;

    public bool IsNewsletterSubscriber;
}
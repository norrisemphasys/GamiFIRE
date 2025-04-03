using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine.Events;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

public class BadgeManager : MonoSingleton<BadgeManager>
{
    public static string BadgeToken = "";

    public static string email = "norris@emphasyscentre.com";
    public static string password = "zkx5TJB3beb_zpj4vrh";

    public static CredentialRequest credentialRequest;
    public static List<Badge> badgeList = new List<Badge>();

    public static void AddUserBadge(Badge badge)
    {
        if (!badgeList.Contains(badge))
            badgeList.Add(badge);
    }

    public static void ClearUsersBadge()
    {
        badgeList.Clear();
    }

    public static bool HasUserBadge(string badgeID)
    {
        for (int i = 0; i < badgeList.Count; i++)
            if (badgeList[i].id == badgeID)
                return true;

        return false;
    }

    public override void Init()
    {
        base.Init();
        GetToken();
    }

    public static void GetToken(UnityAction<string> callback = null)
    {
        string userData = "{\"email\": \"" + email + "\", \"password\" : \"" + password + "\"}";

        RestClient.Post<Token>(GameConstants.BADGE_TOKEN_URL, userData).Then((res) =>
        {
            if (res != null)
            { 
                BadgeToken = res.token;
                callback?.Invoke(res.token);

                Debug.Log("New Token Generated");
            }
        })
        .Catch(error =>
        {
            Debug.LogError(error);
            callback?.Invoke(string.Empty);
        });
    }

    public static void GetBadge(string badgeID, UnityAction<bool> callback = null)
    {
        try
        {
            if (credentialRequest != null)
                credentialRequest.badgeID =  string.IsNullOrEmpty(badgeID) ? "66cdde692d72d391079b4faa" : badgeID;
            else
            {
                CreateCredentialRequest(UserManager.instance.currentUser);
                credentialRequest.badgeID = string.IsNullOrEmpty(badgeID) ? "66cdde692d72d391079b4faa" : badgeID;
            }
                
            string data = JsonConvert.SerializeObject(credentialRequest);

            Debug.Log(data);

            RequestHelper request = new RequestHelper
            {
                Uri = GameConstants.BADGE_CREDENTIAL_URL,
                Method = "POST",
                Headers = new Dictionary<string, string> 
                {
                    { "Authorization", $"Bearer {BadgeToken}" }
                },
                BodyString = data,
                ContentType = "application/json"
            };

            RestClient.Request(request).Then((res) =>
            {
                if (res != null)
                {
                    var achievement = JObject.Parse(res.Text)["credential"]
                    ["batch"]["template"]["credentialTemplateJson"]
                    ["credentialSubject"]["achievement"];

                    var claimLink = JObject.Parse(res.Text)["claimLink"];

                    Debug.Log(claimLink.ToString());

                    Achievement achievment = JsonConvert.DeserializeObject<Achievement>(achievement.ToString());

                    Badge newBadge = new Badge
                    {
                        id = credentialRequest.badgeID,
                        name = achievment.name,
                        imgURL = achievment.image.id,
                        criteria = achievment.criteria.narrative,
                        description = achievment.description,
                        claimLink = claimLink.ToString()
                    };

                    if(!HasUserBadge(badgeID))
                    {
                        // Add new badge.
                        DBManager.AddNewBadge(credentialRequest.userID, newBadge, (sucess)=> 
                        {
                            if(sucess)
                                AddUserBadge(newBadge);

                            Debug.Log("Successfully added badge " + sucess);

                            callback?.Invoke(sucess);
                        });
                    } 
                }
            })
            .Catch(error =>
            {
                Debug.LogError(error);
                callback?.Invoke(false);
            });
        }
        catch
        {
            Debug.LogError("Error Getting Badge");
            callback?.Invoke(false);
        }
    }

    public static void CreateCredentialRequest(User user)
    {
        credentialRequest = new CredentialRequest();

        if (user != null)
            credentialRequest.userID = user.ID;
        else
            credentialRequest.userID = "66d87eaa5465196d8f2c67a9";

        credentialRequest.templeteID = "67b8a1ded984f83708f57577";
        credentialRequest.emailTempleteID = "67b8a2c3d984f83708f57655";

        CredentialPayload payload = new CredentialPayload();

        payload.credentialName = "Certification";
        payload.earnerName = user != null ? user.Username : "affan";
        payload.emailAddress = user != null ? user.Email : "affanashraf120@gmail.com";

        credentialRequest.credentialPayload = payload;
    }
}

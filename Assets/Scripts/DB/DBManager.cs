using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.Events;
using System;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

public class DBManager : MonoSingleton<DBManager>
{
    public static List<User> allUsers = new List<User>();
    public static List<Survey> allUsersSurvey = new List<Survey>();
    public static List<UserBadge> allUsersBadge = new List<UserBadge>();

    public static List<UserSurvey> allUsersWithSurvey = new List<UserSurvey>();

    private static string _idToken;
    private static string _localID;

    public static fsSerializer serializer = new fsSerializer();

    #region OLD LOGIN IMPLEMENTATION

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

    #endregion

    #region USER AUTHENTICATION
    public static void AddEditUserLocalID(User user, UnityAction<User> callback = null)
    {
        RestClient.Put<User>(GameConstants.USERS_DB_URL + "/" + user.ID + ".json?auth=" + _idToken, user).Then(res =>
        {
            Debug.Log("New user added");
            callback?.Invoke(res);
        }).Catch(err =>
        {
            Debug.Log("Error " + err.Message);
            callback?.Invoke(null);
        });
    }

    public static void GetUserByLocalID(UnityAction<User> callback = null)
    {
        RestClient.Get<User>(GameConstants.USERS_DB_URL + "/" + _localID + ".json?auth=" + _idToken).Then(res =>
        {
            Debug.Log("Get user response");
            callback?.Invoke(res);
        }).Catch(err =>
        {
            Debug.Log("Error " + err.Message);
            callback?.Invoke(null);
        });
    }

    public static void GetAllUsersByToken(UnityAction<User[]> callback = null)
    {
        RestClient.Get(GameConstants.USERS_DB_URL + ".json?auth=" + _idToken).Then(res =>
        {
            Dictionary<string, User> users = JsonConvert.DeserializeObject<Dictionary<string, User>>(res.Text);
            Debug.Log("All users count " + users.Count);
            callback?.Invoke(users.Values.ToArray());

            allUsers.Clear();
            allUsers = users.Values.ToList();

#if UNITY_EDITOR
            foreach(User u in allUsers)
            {
                Debug.Log("Email " + u.Email + " ID " + u.ID + "\n");
                if (u.Email.Equals("gerard@gear-up.nl"))
                    Debug.Log("User ID " + u.ID);
            }
#endif

        }).Catch(err =>
        {
            Debug.Log("Error " + err.Message);
            callback?.Invoke(null);
        });
    }

    public static void SignUpUser(string username, string email, string password, UnityAction<User> callback = null)
    {
        string signUpLink = GameConstants.SIGN_UP_URL + GameConstants.WEB_API_KEY;
        string userData = "{\"email\": \""+ email + "\", \"password\" : \"" + password + "\", \"returnSecureToken\":true}";

        Debug.Log(userData);

        RestClient.Post<SignUpInfo>(signUpLink, userData).Then( (res)=> 
        {
            _idToken = res.idToken;
            _localID = res.localId;

            string emailVerification = "{\"requestType\": \"" + "VERIFY_EMAIL" + "\", \"idToken\" : \"" + res.idToken + "\"}";
            RestClient.Post(GameConstants.EMAIL_VETIFICATION_URL + GameConstants.WEB_API_KEY, emailVerification);

            string encryptedPassword = Utils.GetMD5Hash(password);

            User newUser = new User
            {
                ID = _localID,
                Username = username,
                Email = email,
                Password = encryptedPassword,
                isAnExistingAccount = false,

                JobType = 0,
                Gender = 0,
                Coin = 0,
                Score = 0,
                GrowthPoint = 0,
                InnovationPoint = 0,
                CurrencyPoint = 0,
                SatisfactionPoint = 0,
                Costume = "",
                HasBadge = false,
                IsAdministrator = false,
                IsNewsletterSubscriber = false,
            };

            AddEditUserLocalID(newUser, callback);

        })
        .Catch(error => 
        {
            Debug.LogError(error);
        });
    }

    public static void SignInUser(string email, string password, UnityAction<User> callback = null)
    {
        //string encryptedPassword = Utils.GetMD5Hash(password);
        string signUpLink = GameConstants.SIGN_IN_URL + GameConstants.WEB_API_KEY;
        string userData = "{\"email\": \"" + email + "\", \"password\" : \"" + password + "\", \"returnSecureToken\":true}";

        RestClient.Post<SignUpInfo>(signUpLink, userData).Then((res) =>
        {
            string emailVerification = "{\"idToken\" : \"" + res.idToken + "\"}";
            RestClient.Post<EmailVerificationInfo>(GameConstants.GET_USER_DATA_URL + GameConstants.WEB_API_KEY, emailVerification).Then( emailRes => 
            {
                if (emailRes.users[0].emailVerified)
                {
                    _idToken = res.idToken;
                    _localID = res.localId;

                    GetUserByLocalID((user) =>
                    {
                        if(Utils.VerifyMD5Hash(password, user.Password))
                        {
                            callback?.Invoke(user);
                            UserManager.instance.SetCurrentUser(user);
                        }
                        else
                        {
                            user.Password = Utils.GetMD5Hash(password);
                            AddEditUserLocalID(user, (updatedUser) => 
                            {
                                callback?.Invoke(updatedUser);
                                UserManager.instance.SetCurrentUser(updatedUser);
                            });
                        }
                    });
                }
                else
                {
                    callback?.Invoke(null);
                }
            });

            Debug.LogError("Sign In Successful");
        })
        .Catch(error =>
        {
            Debug.LogError(error);
            callback?.Invoke(null);
        });
    }

    public static void RequestForgotPassword(string email, UnityAction<bool> callback = null)
    {
        string req = "{\"requestType\": \"" + "PASSWORD_RESET" + "\", \"email\" : \"" + email + "\"}";
        RestClient.Post(GameConstants.SEND_PASSWORD_RESET_EMAIL_URL + GameConstants.WEB_API_KEY, req).Then((res) => 
        {
            callback?.Invoke(res != null);
        });
    }

    public static void RequestChangePassword(string code, string newPassword, UnityAction<bool> callback = null)
    {
        string req = "{\"oobCode\": \"" + code + "\", \"newPassword\" : \"" + newPassword + "\"}";
        RestClient.Post(GameConstants.CONFIRM_PASSWORD_RESET_URL + GameConstants.WEB_API_KEY, req).Then((res) =>
        {
            callback?.Invoke(res != null);
        });
    }

    #endregion

    #region BADGE INTEGRATION
    public static void CreateUserBadge(UnityAction<UserBadge> callback = null)
    {
        UserBadge userBadge = new UserBadge
        {
            userID = BadgeManager.credentialRequest.userID,
            templateID = BadgeManager.credentialRequest.templateID,
            emailTemplateID = BadgeManager.credentialRequest.emailTemplateID,

            badges = new Badge()
        };

        RestClient.Put<UserBadge>(GameConstants.USERS_BADGE_URL + "/" + userBadge.userID + ".json?auth=" + _idToken, userBadge).Then(res =>
        {
            if(res != null)
            {
                UserManager.instance.userBadge = res;
                callback?.Invoke(res);

                Debug.Log("User Badge Created");
            }
        })
        .Catch(err =>
        {
            Debug.LogError("Error " + err.Message);
            callback?.Invoke(null);
        });
    }

    public static void GetAllUsersBadge(User user, UnityAction<bool> callback = null)
    {
        RestClient.Get(GameConstants.USERS_BADGE_URL + "/" + user.ID + "/badges" + ".json?auth=" + _idToken).Then(res =>
        {
            if (!string.IsNullOrEmpty(res.Text))
            {
                BadgeManager.ClearUsersBadge();

                Dictionary<string, Badge> badges = JsonConvert.DeserializeObject<Dictionary<string, Badge>>(res.Text);

                foreach (var badge in badges)
                    BadgeManager.AddUserBadge(badge.Value);

                callback?.Invoke(true);

                Debug.Log("Get All Users Badge " + badges.Count);
            }
        }).Catch(err =>
        {
            Debug.Log("Error " + err.Message);
            callback?.Invoke(false);
        });
    }

    public static void AddNewBadge(string userID, Badge badge, UnityAction<bool> callback = null)
    {
        RestClient.Put<Badge>(GameConstants.USERS_BADGE_URL + "/" + userID + "/badges/" 
            + badge.id + ".json?auth=" + _idToken, badge).Then(res =>
        {
            if (res != null)
            {
                callback?.Invoke(true);
                Debug.Log("Added New Badge");
            }
        }).Catch(err =>
        {
            Debug.Log("Error " + err.Message);
            callback?.Invoke(false);
        });
    }

    public static void GetAllUsersWithBadge(UnityAction<string> callback = null)
    {
        RestClient.Get(GameConstants.USERS_BADGE_URL + ".json?auth=" + _idToken).Then(res =>
        {
            StringBuilder sb = new StringBuilder();
            //Debug.Log("Badge " + res.Text);
            foreach (User user in allUsers)
            {
                var userID = JObject.Parse(res.Text)[user.ID];
                if(userID != null)
                {
                    var badges = JObject.Parse(userID.ToString())["badges"];
                    if (badges != null)
                    {
                        Dictionary<string, Badge> userBadges = JsonConvert.DeserializeObject<Dictionary<string, Badge>>(badges.ToString());

                        int badgeCount = 1;
                        string row = user.Email + ",";

                        foreach (var badge in userBadges)
                        {
                            //Debug.Log("user id " + user.ID + " badge id " + badge.Value.id);
                            row += badge.Value.claimLink + (badgeCount < userBadges.Count ? "," : "");
                            badgeCount++;
                        }
                        sb.AppendLine(row);
                    }
                }
            }

            Debug.Log(sb.ToString());
            callback?.Invoke(sb.ToString());
           
            //Dictionary<string, UserBadge> users = JsonConvert.DeserializeObject<Dictionary<string, UserBadge>>(res.Text);
            //Debug.Log("All users with badge count " + users.Count);
            //callback?.Invoke(users.Values.ToArray());
            //allUsersBadge = users.Values.ToList();

        }).Catch(err =>
        {
            Debug.Log("Error " + err.Message);
            callback?.Invoke(null);
        });
    }

    #endregion

    #region SURVEY

    public static void GetAllUsersWithSurvey(UnityAction<UserSurvey[]> callback = null)
    {
        RestClient.Get(GameConstants.USERS_SURVEY_URL + ".json?auth=" + _idToken).Then(res =>
        {
            Debug.Log("Survey " + res.Text);
            //Dictionary<string, UserSurvey> users = JsonConvert.DeserializeObject<Dictionary<string, UserSurvey>>(res.Text);
            //Debug.Log("All users with survey count " + users.Count);
            //callback?.Invoke(users.Values.ToArray());

            //allUsersWithSurvey = users.Values.ToList();
        }).Catch(err =>
        {
            Debug.Log("Error " + err.Message);
            callback?.Invoke(null);
        });
    }

    public static void GetAllUsersSurvey(User user, UnityAction<Survey[]> callback = null)
    {
        allUsersSurvey.Clear();

        RestClient.Get(GameConstants.USERS_SURVEY_URL + "/" + user.ID + "/surveys" + ".json?auth=" + _idToken).Then(res =>
        {
            if (!string.IsNullOrEmpty(res.Text))
            {
                BadgeManager.ClearUsersBadge();

                Dictionary<string, Survey> surveys = JsonConvert.DeserializeObject<Dictionary<string, Survey>>(res.Text);

                /*  foreach (var badge in badges)
                      BadgeManager.AddUserBadge(badge.Value);*/

                foreach (var survey in surveys)
                {
                    allUsersSurvey.Add(survey.Value);
                    Debug.LogError("ID " + survey.Value.id);
                }

                callback?.Invoke(surveys.Values.ToArray());

                Debug.Log("Get All Users Survey " + surveys.Count);
            }
        }).Catch(err =>
        {
            Debug.Log("Error " + err.Message);
            callback?.Invoke(null);
        });
    }

    public static void AddNewSurvey(User user, Survey survey, UnityAction<bool> callback = null)
    {
        RestClient.Put<Survey>(GameConstants.USERS_SURVEY_URL + "/" + user.ID + "/surveys/"
            + survey.id + ".json?auth=" + _idToken, survey).Then(res =>
            {
                if (res != null)
                {
                    callback?.Invoke(true);
                    Debug.Log("Added New Badge");
                }
            }).Catch(err =>
            {
                Debug.Log("Error " + err.Message);
                callback?.Invoke(false);
            });
    }
    #endregion

    #region CSV_IMPORTER
    public static void SaveToCSFString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Header1,Header2,Header3");
        sb.AppendLine("Value1,Value2,Value3");
        sb.AppendLine("MoreDataA,MoreDataB,MoreDataC");

        Debug.Log(sb.ToString());

    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.Events;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using Unity.VisualScripting.FullSerializer;

public class DBManager : MonoSingleton<DBManager>
{
    private static string _idToken;
    private static string _localID;

    public static fsSerializer serializer = new fsSerializer();
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

    public static void GetAllUsersByLocalId(UnityAction<User[]> callback = null)
    {
        RestClient.Get(GameConstants.USERS_DB_URL + ".json?auth=" + _idToken).Then(res =>
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
                Costume = ""
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
}
using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginController : MonoBehaviour
{
    [SerializeField] private LoginViews view;

    // Start is called before the first frame update
    void Start()
    {
        view.Init();
        view.Show(true, () => 
        {
            view.ShowSignIn(true);

            AddListener();
        });
    }

    private void OnDestroy()
    {
        RemoveListener();
    }

    void AddListener()
    {
        view.signUpButtonSignIn.onClick.AddListener(OnClickShowSignUp);
        view.signInButtonSignIn.onClick.AddListener(OnClickLogin);
        view.forgotPasswordButton.onClick.AddListener(OnClickForgotPassword);

        view.signInButtonSignUp.onClick.AddListener(OnClickShowSignIn);
        view.signUpButtonSignUp.onClick.AddListener(OnClickSignUpNewUser);

        view.confirmEmailButton.onClick.AddListener(OnClickConfirmEmail);
        view.confirmPasswordButton.onClick.AddListener(OnClickNewPassword);

        view.closeButton.onClick.AddListener(OnClickCloseForgotPassword);
    }

    void RemoveListener()
    {
        view.signUpButtonSignIn.onClick.RemoveListener(OnClickShowSignUp);
        view.signInButtonSignIn.onClick.RemoveListener(OnClickLogin);
        view.forgotPasswordButton.onClick.RemoveListener(OnClickForgotPassword);

        view.signInButtonSignUp.onClick.RemoveListener(OnClickShowSignIn);
        view.signUpButtonSignUp.onClick.RemoveListener(OnClickSignUpNewUser);

        view.confirmEmailButton.onClick.RemoveListener(OnClickConfirmEmail);
        view.confirmPasswordButton.onClick.RemoveListener(OnClickNewPassword);

        view.closeButton.onClick.RemoveListener(OnClickCloseForgotPassword);
    }

    void OnClickSignUpNewUser()
    {
        if (view.signUpEmail == string.Empty ||
            view.signUpPassword == string.Empty ||
            view.signUpUsername == string.Empty)
        {
            PopupManager.instance.ShowPopup(PopupMessage.ErrorPopup
                        ("You are missing an information. Make sure to complete the necessary data."));
            return; // Show popup Error
        }

        // Add new user to database.
        User newUser = new User
        {
            ID = System.Guid.NewGuid().ToString(),
            Username = view.signUpUsername,
            Email = view.signUpEmail,
            Password = Utils.GetMD5Hash(view.signUpPassword),

            JobType = 0,
            Coin = 0,
            Score = 0,
            GrowthPoint = 0,
            InnovationPoint = 0,
            CurrencyPoint = 0,
            SatisfactionPoint = 0,
            Costume = ""
        };

        LoadingManager.instance.ShowLoader(true);

        DBManager.GetUserByObject(newUser, (res)=>
        {
            if (res == null)
            {
                DBManager.AddEditUser(newUser, (res) => { 
                    LoadingManager.instance.ShowLoader(false);

                    PopupManager.instance.ShowPopup(PopupMessage.InfoPopup("New user successfuly created.",
                    ()=> 
                    {
                        view.ShowSignUp(false, () =>
                        {
                            view.ShowSignIn(true);
                        });
                    }));
                });
            } 
            else
            {
                LoadingManager.instance.ShowLoader(true);
                PopupManager.instance.ShowPopup(PopupMessage.InfoPopup("User already exist."));
            }
        });
    }

    void OnClickLogin()
    {
        if (view.signInUsername == string.Empty ||
            view.signInPassword == string.Empty)
        {
            PopupManager.instance.ShowPopup(PopupMessage.ErrorPopup
                ("You are missing an information. Make sure to complete the necessary data."));
            return; // Show popup Error
        }

        LoadingManager.instance.ShowLoader(true);

        DBManager.GetUserByName(view.signInUsername, (res) =>
        {
            if (res == null)
            {
                // Show popup wrong username or password
                Debug.Log("Wrong username or password.");
                LoadingManager.instance.ShowLoader(false);

                PopupManager.instance.ShowPopup(PopupMessage.ErrorPopup
                    ("Wrong username or password. Please try again."));
            }
            else
            {
                if (Utils.VerifyMD5Hash(view.signInPassword, res.Password))
                {
                    // Login user account
                    Debug.Log("Login user account.");
                    UserManager.instance.SetCurrentUser(res);

                    view.ShowSignIn(false, () => 
                    {
                        LoadSceneManager.instance.LoadSceneLevel(1, 
                            UnityEngine.SceneManagement.LoadSceneMode.Single ,
                        () => 
                        {
                            LoadingManager.instance.ShowLoader(false);
                        });
                    });
                }
                else
                {
                    // Show popup wrong username or password
                    Debug.Log("Wrong username or password.");
                    LoadingManager.instance.ShowLoader(false);

                    PopupManager.instance.ShowPopup( PopupMessage.ErrorPopup
                        ("Wrong username or password. Please try again.") );
                }
            }
        });
    }

    void OnClickForgotPassword()
    {
        view.ShowSignIn(false, () =>
        {
            view.ShowForgotPassword(true);
        });
    }

    void OnClickShowSignUp()
    {
        view.ShowSignIn(false, ()=> 
        {
            view.ShowSignUp(true);
        });
    }

    void OnClickShowSignIn()
    {
        view.ShowSignUp(false, () =>
        {
            view.ShowSignIn(true);
        });
    }

    void OnClickConfirmEmail()
    {
        // Add email verification
        LoadingManager.instance.ShowLoader(true);

        DBManager.GetUserByEmail(view.forgotPasswordEmail, res => 
        {
            LoadingManager.instance.ShowLoader(false);
            if (res == null)
            {
                PopupManager.instance.ShowPopup(PopupMessage.ErrorPopup
                        ("Email does not exist. Please try again."));
            }
            else
            {
                view.ShowConfirmEmail(false);
                view.ShowNewPassword(true);

                UserManager.instance.SetCurrentUser(res);
            }
        });
    }

    void OnClickNewPassword()
    {
        // Update user password
        User currentUser = UserManager.instance.currentUser;
        currentUser.Password = Utils.GetMD5Hash(view.newPassword);

        LoadingManager.instance.ShowLoader(true);

        DBManager.AddEditUser(currentUser, res => 
        {
            Debug.Log("User password updated");

            PopupManager.instance.ShowPopup(PopupMessage.InfoPopup("Your password has been updated.", ()=> 
            {
                view.ShowForgotPassword(false, () =>
                {
                    view.ShowSignIn(true);
                });
            }));

            UserManager.instance.SetCurrentUser(res);
            LoadingManager.instance.ShowLoader(false);
        });
    }

    void OnClickCloseForgotPassword()
    {
        view.ShowForgotPassword(false, () =>
        {
            view.ShowSignIn(true);
        });
    }
}

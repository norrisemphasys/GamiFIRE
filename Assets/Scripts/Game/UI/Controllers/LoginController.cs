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
            PopupManager.instance.ShowPopup(PopupView.CreatePopupData("ERROR", 
                "You are missing an information. Make sure to complete the necessary data.", 
                showClose: false, showCancel: false));
            return; // Show popup Error
        }

        // Add new user to database.
        User newUser = new User
        {
            ID = System.Guid.NewGuid().ToString(),
            Username = view.signUpUsername,
            Email = view.signUpEmail,
            Password = Utils.GetMD5Hash(view.signUpPassword)
        };

        LoadingManager.instance.ShowLoader(true);

        DBManager.GetUserByObject(newUser, (res)=>
        {
            if (res == null)
            {
                DBManager.AddEditUser(newUser, (res) => { 
                    LoadingManager.instance.ShowLoader(false);

                    PopupManager.instance.ShowPopup(PopupView.CreatePopupData("INFO",
                    "New user successfuly created.",
                    showClose: false, showCancel: false, onClickOk: ()=> 
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
                PopupManager.instance.ShowPopup(PopupView.CreatePopupData("INFO",
                "User already exist.",
                showClose: false, showCancel: false));
            }
        });
    }

    void OnClickLogin()
    {
        if (view.signInUsername == string.Empty ||
            view.signInPassword == string.Empty)
        {
            PopupManager.instance.ShowPopup(PopupView.CreatePopupData("ERROR",
                "You are missing an information. Make sure to complete the necessary data.",
                showClose: false, showCancel: false));

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

                PopupManager.instance.ShowPopup(PopupView.CreatePopupData("ERROR",
                "Wrong username or password. Please try again.",
                showClose: false, showCancel: false));
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

                    PopupManager.instance.ShowPopup(PopupView.CreatePopupData("ERROR",
                    "Wrong username or password. Please try again.",
                    showClose: false, showCancel: false));
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
                Debug.Log("Email does not exist");
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

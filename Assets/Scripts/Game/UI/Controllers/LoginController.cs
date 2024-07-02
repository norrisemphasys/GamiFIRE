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

    // Update is called once per frame
    void Update()
    {
        
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
        if (view.signUpEmail == string.Empty)
            return; // Show popup Error
        if (view.signUpPassword == string.Empty)
            return; // Show popup Error
        if (view.signUpUsername == string.Empty)
            return; // Show popup Error

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
                DBManager.AddEditUser(newUser, (res)=> { LoadingManager.instance.ShowLoader(false); });
            else
                Debug.Log("User already exist.");
        });
    }

    void OnClickLogin()
    {
        if (view.signInUsername == string.Empty)
            return; // Show popup Error
        if (view.signInPassword == string.Empty)
            return; // Show popup Error

        LoadingManager.instance.ShowLoader(true);

        DBManager.GetUserByName(view.signInUsername, (res) =>
        {
            LoadingManager.instance.ShowLoader(false);

            if (res == null)
            {
                // Show popup wrong username or password
                Debug.Log("Wrong username or password.");
            }
            else
            {
                if (Utils.VerifyMD5Hash(view.signInPassword, res.Password))
                {
                    // Login user account
                    Debug.Log("Login user account.");
                    UserManager.instance.SetCurrentUser(res);
                }
                else
                {
                    // Show popup wrong username or password
                    Debug.Log("Wrong username or password.");
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

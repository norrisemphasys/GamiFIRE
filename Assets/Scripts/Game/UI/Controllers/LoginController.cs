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
    }

    void RemoveListener()
    {
        view.signUpButtonSignIn.onClick.RemoveListener(OnClickShowSignUp);
        view.signInButtonSignIn.onClick.RemoveListener(OnClickLogin);
        view.forgotPasswordButton.onClick.RemoveListener(OnClickForgotPassword);

        view.signInButtonSignUp.onClick.RemoveListener(OnClickShowSignIn);
        view.signUpButtonSignUp.onClick.RemoveListener(OnClickSignUpNewUser);
    }

    void OnClickSignUpNewUser()
    {
        if (view.signUpEmail == string.Empty)
            return;
        if (view.signUpPassword == string.Empty)
            return;
        if (view.signUpUsername == string.Empty)
            return;

        // Add new user to database.
        User newUser = new User
        {
            ID = System.Guid.NewGuid().ToString(),
            Username = view.signUpUsername,
            Email = view.signUpEmail,
            Password = Utils.GetMD5Hash(view.signUpPassword)
        };

        DBManager.GetUser(newUser, (res)=>
        {
            if (res == null)
                DBManager.AddNewUser(newUser);
            else
                Debug.Log("User already exist");
        });
    }

    void OnClickLogin()
    {
        // Login user account 
        // Add user verification
    }

    void OnClickForgotPassword()
    {

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
}

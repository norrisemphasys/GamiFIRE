//#define OLD_DB_IMPLEMENTATION

using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginController : BasicController
{
    private LoginViews view;

    [SerializeField] int loadSceneIndex;
    [SerializeField] bool resetUserPoints = false;
    [SerializeField] bool isOffline = false;

    [SerializeField] bool isAutoLogin;
    [SerializeField] bool isNewsLetter;

    private string email;
    private string password;

    void Awake()
    {
        view = GetComponent<LoginViews>();
        view.Init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        view.Show(()=> 
        {
            view.ShowSignIn(true);
        });

        AddListener();
        Initialize();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.P))
            Audio.PlayBGMLogin();

        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            if(!PopupManager.instance.IsPopupShowing)
            {
                if (view.IsSignInVisible)
                    OnClickLogin();
                else if (view.IsSignUpVisible)
                    OnClickSignUpNewUser();
                else if (view.IsForgotPasswordVisible)
                {
                    if (view.IsEmailVisible)
                        OnClickConfirmEmail();
                    else if (view.IsNewPasswordVisible)
                        OnClickNewPassword();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            OnClickDefault(UIState.SURVEY_MENU);
        }
#endif
    }

    public override void OnExit()
    {
        RemoveListener();
        view.Hide(ShowNextMenu);
    }

    public override void Initialize()
    {
        isAutoLogin = PlayerPrefs.GetInt("AutoSignIn", 0) == 1;
        view.toggleAutoSignIn.isOn = isAutoLogin;

        isNewsLetter = PlayerPrefs.GetInt("NewsLetter", 0) == 1;
        view.toggleNewsletter.isOn = isNewsLetter;

        if (isAutoLogin)
        {
            email = PlayerPrefs.GetString("Email");
            password = PlayerPrefs.GetString("Password");

            view.SetEmailLoginText(email);
            view.SetPasswordLoginText(password);

            if(!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                OnClickLogin();
        }

       /* PopupManager.instance.ShowPopup(PopupMessage.InfoPopup("Press Left Shift Key and V Key (Left Shift + V) to turn on or off the Sound Volume.", () =>
        {

        }));*/
    }

    public void ShowNextMenu()
    {
        uiController.Show(nextState);
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
        view.buttonShowPassword.onClick.AddListener(OnClickShowPassword);

        view.toggleAutoSignIn.onValueChanged.AddListener(OnToggleAutoLogin);
        view.toggleNewsletter.onValueChanged.AddListener(OnToggleNewsLetter);

        view.buttonShowPasswordSign.onClick.AddListener(OnClickShowPasswordSign);
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
        view.buttonShowPassword.onClick.RemoveListener(OnClickShowPassword);

        view.toggleAutoSignIn.onValueChanged.RemoveListener(OnToggleAutoLogin);
        view.toggleNewsletter.onValueChanged.RemoveListener(OnToggleNewsLetter);

        view.buttonShowPasswordSign.onClick.RemoveListener(OnClickShowPasswordSign);
    }

    void OnToggleNewsLetter(bool isOn) 
    {
        isNewsLetter = isOn;
        PlayerPrefs.SetInt("NewsLetter", isNewsLetter ? 1 : 0);

        Debug.LogError("Is NewsLetter " + isNewsLetter);
    }

    void OnToggleAutoLogin(bool isOn)
    {
        isAutoLogin = isOn;
        PlayerPrefs.SetInt("AutoSignIn", isAutoLogin ? 1 : 0);

        Debug.LogError("IS AUTO LOGIN " + isAutoLogin);
    }

    void OnClickShowPassword()
    {
        view.ToggleShowPassword();
    }

    void OnClickShowPasswordSign()
    {
        view.ToggleShowPasswordSignIn();
    }

    void OnClickSignUpNewUser()
    {
        Audio.PlaySFXClick();

        if (view.signUpEmail == string.Empty ||
            view.signUpPassword == string.Empty ||
            view.signUpUsername == string.Empty)
        {
            PopupManager.instance.ShowPopup(PopupMessage.ErrorPopup
                        ("You are missing an information. Make sure to complete the necessary data."));
            return; // Show popup Error
        }
#if OLD_DB_IMPLEMENTATION
        // Add new user to database.
        User newUser = new User
        {
            ID = System.Guid.NewGuid().ToString(),
            Username = view.signUpUsername,
            Email = view.signUpEmail,
            Password = Utils.GetMD5Hash(view.signUpPassword),
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

        LoadingManager.instance.ShowLoader(true);

        DBManager.GetUserByObject(newUser, (res)=>
        {
            if (res == null)
            {
                DBManager.AddEditUser(newUser, (res) => { 
                    LoadingManager.instance.ShowLoader(false);

                    PopupManager.instance.ShowPopup(PopupMessage.InfoPopup("New user successfuly created. Login your account to get started.",
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
#else
        LoadingManager.instance.ShowLoader(true);
        DBManager.SignUpUser(view.signUpUsername, view.signUpEmail, view.signUpPassword, 
        (res) => 
        {
            LoadingManager.instance.ShowLoader(false);

            string popupMessage = "Your account has been successfully created. Please login to your email app and click on the activation link to enable your new Gamifire Player Account.";
            PopupManager.instance.ShowPopup(PopupMessage.InfoPopup(popupMessage,
            () =>
            {
                view.ShowSignUp(false, () =>
                {
                    view.ShowSignIn(true);
                });
            }));
        });
#endif
    }

    void OnClickLogin()
    {
        Audio.PlaySFXClick();

        if(isOffline)
        {
            User newUser = new User
            {
                ID = System.Guid.NewGuid().ToString(),
                Username = "Test User",
                Email = "test@test.com",
                Password = Utils.GetMD5Hash("1234567890"),
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

            UserManager.instance.SetCurrentUser(newUser);

            if (resetUserPoints)
                UserManager.instance.ResetUserPoints();

            view.ShowSignIn(false, () =>
            {
                LoadSceneManager.instance.LoadSceneLevel(loadSceneIndex,
                    UnityEngine.SceneManagement.LoadSceneMode.Single,
                () =>
                {
                    LoadingManager.instance.ShowLoader(false);
                });
            });

            return;
        }

        if (view.signInUsername == string.Empty ||
            view.signInPassword == string.Empty)
        {
            PopupManager.instance.ShowPopup(PopupMessage.ErrorPopup
                ("You are missing an information. Make sure to complete the necessary data."));
            return; // Show popup Error
        }

#if OLD_DB_IMPLEMENTATION
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
                    Debug.Log("Login user account." + res.isAnExistingAccount);
                    UserManager.instance.SetCurrentUser(res);
                    if(resetUserPoints)
                        UserManager.instance.ResetUserPoints();

                    if(res.isAnExistingAccount)
                    {
                        view.ShowSignIn(false, () =>
                        {
                            LoadSceneManager.instance.LoadSceneLevel(loadSceneIndex,
                                UnityEngine.SceneManagement.LoadSceneMode.Single,
                            () =>
                            {
                                LoadingManager.instance.ShowLoader(false);
                            });
                        });
                    }
                    else
                    {
                        OnClickDefault(UIState.GENDER_MENU);
                        LoadingManager.instance.ShowLoader(false);
                    }  
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
#else
        LoadingManager.instance.ShowLoader(true);
        DBManager.SignInUser(view.signInUsername, view.signInPassword, (res) => 
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
                if (!res.IsNewsletterSubscriber)
                    res.IsNewsletterSubscriber = isNewsLetter;

                UserManager.instance.SetCurrentUser(res);
                if (resetUserPoints)
                    UserManager.instance.ResetUserPoints();

                if (res.isAnExistingAccount)
                {
                    view.ShowSignIn(false, () =>
                    {
                        LoadSceneManager.instance.LoadSceneLevel(loadSceneIndex,
                            UnityEngine.SceneManagement.LoadSceneMode.Single,
                        () =>
                        {
                            LoadingManager.instance.ShowLoader(false);
                        });
                    });
                }
                else
                {

#if UNITY_EDITOR

                    DBManager.GetAllUsersByToken((users) => 
                    {
                        if(users.Length > 0)
                        {
                            // Temp
                            //DBManager.GetAllUsersWithBadge();
                            DBManager.GetAllUsersWithSurvey();
                        }
                    });
#endif

                    DBManager.GetAllUsersSurvey(res, (surveys) =>
                    {
                        SurveySO surveySO = gameManager.surveyController.GetCurrentSurvey();
                        Survey survey = DBManager.allUsersSurvey.Find((x) => x.id == surveySO.id);

                        if (survey != null)
                            OnClickDefault(UIState.GENDER_MENU);
                        else
                            OnClickDefault(UIState.SURVEY_MENU);

                        LoadingManager.instance.ShowLoader(false);
                    });
                }

                if(isAutoLogin)
                {
                    PlayerPrefs.SetString("Email", view.signInUsername);
                    PlayerPrefs.SetString("Password", view.signInPassword);
                }
            }
        });
#endif
    }

    void OnClickForgotPassword()
    {
        Audio.PlaySFXClick();

        view.ShowSignIn(false, () =>
        {
            view.ShowForgotPassword(true);
        });
    }

    void OnClickShowSignUp()
    {
        Audio.PlaySFXClick();

        view.ShowSignIn(false, ()=> 
        {
            view.ShowSignUp(true);
        });
    }

    void OnClickShowSignIn()
    {
        Audio.PlaySFXClick();

        view.ShowSignUp(false, () =>
        {
            view.ShowSignIn(true);
        });
    }

    void OnClickConfirmEmail()
    {
        Audio.PlaySFXClick();
#if OLD_DB_IMPLEMENTATION
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
#else
        LoadingManager.instance.ShowLoader(true);

        DBManager.RequestForgotPassword(view.forgotPasswordEmail, (res) =>
        {
            LoadingManager.instance.ShowLoader(false);

            if(res)
            {
                PopupManager.instance.ShowPopup(PopupMessage.InfoPopup
                ("Check your email to change your password.", ()=> 
                {
                    view.ShowForgotPassword(false, () =>
                    {
                        view.ShowSignIn(true);
                    });
                }));
            }
            else
            {
                PopupManager.instance.ShowPopup(PopupMessage.ErrorPopup
                        ("Email does not exist. Please try again."));
            }
        });
#endif
    }

    void OnClickNewPassword()
    {
        Audio.PlaySFXClick();

        // Update user password
        User currentUser = UserManager.instance.currentUser;
        currentUser.Password = Utils.GetMD5Hash(view.newPassword);

#if OLD_DB_IMPLEMENTATION
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
#else
        LoadingManager.instance.ShowLoader(true);

        DBManager.RequestChangePassword(view.verificationCode, currentUser.Password, (res) =>
        {
            if (res)
            {
                DBManager.AddEditUserLocalID(currentUser, (user) => 
                {
                    PopupManager.instance.ShowPopup(PopupMessage.InfoPopup("Your password has been updated.", () =>
                    {
                        view.ShowForgotPassword(false, () =>
                        {
                            view.ShowSignIn(true);
                        });
                    }));

                    UserManager.instance.SetCurrentUser(user);
                    LoadingManager.instance.ShowLoader(false);
                });
            }
            else
            {
                PopupManager.instance.ShowPopup(PopupMessage.ErrorPopup
                       ("Failed to change password."));
            }
        });

#endif
    }

    void OnClickCloseForgotPassword()
    {
        Audio.PlaySFXClick();

        view.ShowForgotPassword(false, () =>
        {
            view.ShowSignIn(true);
        });
    }
}

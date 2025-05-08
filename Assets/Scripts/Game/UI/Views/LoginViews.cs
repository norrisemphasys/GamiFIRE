using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class LoginViews : BasicView
{
    [Header("Sign Up")]
    [Space(5)]

    [SerializeField] RectTransform signUpRect;

    [SerializeField] TMP_InputField emailInputSignUp;
    [SerializeField] TMP_InputField usernameInputSignUp;
    [SerializeField] TMP_InputField passwordInputSignUp;

    [SerializeField] Image imgShowIcon;

    public Button signUpButtonSignUp;
    public Button signInButtonSignUp;

    public Button buttonShowPassword;

    public bool IsSignUpVisible => signUpRect.localScale == Vector3.one;

    [Header("Sign In")]
    [Space(5)]

    [SerializeField] RectTransform signInRect;

    [SerializeField] TMP_InputField usernameInputSignIn;
    [SerializeField] TMP_InputField passwordInputSignIn;

    public Button signUpButtonSignIn;
    public Button signInButtonSignIn;
    public Button forgotPasswordButton;

    public Toggle toggleAutoSignIn;
    public Toggle toggleNewsletter;

    [SerializeField] Image imgShowIconSign;
    public Button buttonShowPasswordSign;

    public bool IsSignInVisible => signInRect.localScale == Vector3.one;

    [Header("Forgot Password")]
    [Space(5)]

    [SerializeField] RectTransform forgotPasswordRect;

    [SerializeField] GameObject typeEmailRect;
    [SerializeField] GameObject typeNewPasswordRect;

    [SerializeField] TMP_InputField confirmEmailInput;
    [SerializeField] TMP_InputField newPasswordInput;
    [SerializeField] TMP_InputField verificationCodeInput;

    public Button confirmEmailButton;
    public Button confirmPasswordButton;
    public Button closeButton;

    public bool IsForgotPasswordVisible => forgotPasswordRect.localScale == Vector3.one;
    public bool IsEmailVisible => typeEmailRect.activeSelf;
    public bool IsNewPasswordVisible => typeNewPasswordRect.activeSelf;

    public string signUpEmail { get { return emailInputSignUp?.text; } }
    public string signUpUsername { get { return usernameInputSignUp?.text; } }
    public string signUpPassword { get { return passwordInputSignUp?.text; } }

    public string signInUsername { get { return usernameInputSignIn?.text; } }
    public string signInPassword { get { return passwordInputSignIn?.text; } }

    public string forgotPasswordEmail { get { return confirmEmailInput?.text; } }
    public string newPassword { get { return newPasswordInput?.text; } }
    public string verificationCode { get { return verificationCodeInput?.text; } }

    bool showPassword = false;

    bool showPasswordSignIn = false;

    public void SetEmailLoginText(string value)
    {
        usernameInputSignIn.text = value;
    }
    
    public void SetPasswordLoginText(string value)
    {
        passwordInputSignIn.text = value;
    }

    public void Init()
    {
        signInRect.DOScale(Vector3.zero, 0).SetUpdate(true);
        signUpRect.DOScale(Vector3.zero, 0).SetUpdate(true);
        forgotPasswordRect.DOScale(Vector3.zero, 0).SetUpdate(true);

        ShowConfirmEmail(true);
        ShowNewPassword(false);
    }

    public void ShowSignIn(bool show, UnityAction callback = null)
    {
        signInRect.DOScale(show ? Vector3.one : Vector3.zero, 0.2f)
        .OnComplete(() =>
        {
            callback?.Invoke();
        }).SetUpdate(true);

        if (!show)
            ResetSignInView();
    }

    public void ShowSignUp(bool show, UnityAction callback = null)
    {
        signUpRect.DOScale(show ? Vector3.one : Vector3.zero, 0.2f)
        .OnComplete(()=> 
        {
            callback?.Invoke();
        }).SetUpdate(true);

        if (!show)
            ResetSigUpView();
    }

    public void ShowForgotPassword(bool show, UnityAction callback = null)
    {
        forgotPasswordRect.DOScale(show ? Vector3.one : Vector3.zero, 0.2f)
        .OnComplete(() =>
        {
            callback?.Invoke();
        }).SetUpdate(true);

        if (!show)
            ResetForgotPasswordView();
    }

    public void ShowConfirmEmail(bool show)
    {
        typeEmailRect.SetActive(show);
    }

    public void ShowNewPassword(bool show)
    {
        typeNewPasswordRect.SetActive(show);
    }

    public void ResetSignInView()
    {
        usernameInputSignIn.text = string.Empty;
        passwordInputSignIn.text = string.Empty;
    }

    public void ResetSigUpView()
    {
        emailInputSignUp.text = string.Empty;
        usernameInputSignUp.text = string.Empty;
        passwordInputSignUp.text = string.Empty;
    }

    public void ResetForgotPasswordView()
    {
        confirmEmailInput.text = string.Empty;
        newPasswordInput.text = string.Empty;
    }

    public void ToggleShowPassword()
    {
        showPassword = !showPassword;

        Color c = imgShowIcon.color;
        c.a = showPassword ? 1 : 0.6f;
        imgShowIcon.color = c;

        passwordInputSignUp.contentType = showPassword ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        passwordInputSignUp.ForceLabelUpdate();
    }

    public void ToggleShowPasswordSignIn()
    {
        showPasswordSignIn = !showPasswordSignIn;

        Color c = imgShowIconSign.color;
        c.a = showPasswordSignIn ? 1 : 0.6f;
        imgShowIconSign.color = c;

        passwordInputSignIn.contentType = showPasswordSignIn ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        passwordInputSignIn.ForceLabelUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class LoginViews : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    [Header("Sign Up")]
    [Space(5)]

    [SerializeField] RectTransform signUpRect;

    [SerializeField] TMP_InputField emailInputSignUp;
    [SerializeField] TMP_InputField usernameInputSignUp;
    [SerializeField] TMP_InputField passwordInputSignUp;

    public Button signUpButtonSignUp;
    public Button signInButtonSignUp;

    [Header("Sign In")]
    [Space(5)]

    [SerializeField] RectTransform signInRect;

    [SerializeField] TMP_InputField usernameInputSignIn;
    [SerializeField] TMP_InputField passwordInputSignIn;

    public Button signUpButtonSignIn;
    public Button signInButtonSignIn;
    public Button forgotPasswordButton;

    [Header("Forgot Password")]
    [Space(5)]

    [SerializeField] RectTransform forgotPasswordRect;

    [SerializeField] GameObject typeEmailRect;
    [SerializeField] GameObject typeNewPasswordRect;

    [SerializeField] TMP_InputField confirmEmailInput;
    [SerializeField] TMP_InputField newPasswordInput;

    public Button confirmEmailButton;
    public Button confirmPasswordButton;
    public Button closeButton;

    public string signUpEmail { get { return emailInputSignUp?.text; } }
    public string signUpUsername { get { return usernameInputSignUp?.text; } }
    public string signUpPassword { get { return passwordInputSignUp?.text; } }

    public string signInUsername { get { return usernameInputSignIn?.text; } }
    public string signInPassword { get { return passwordInputSignIn?.text; } }

    public string forgotPasswordEmail { get { return confirmEmailInput?.text; } }
    public string newPassword { get { return newPasswordInput?.text; } }


    public void Init()
    {
        signInRect.DOScale(Vector3.zero, 0);
        signUpRect.DOScale(Vector3.zero, 0);
        forgotPasswordRect.DOScale(Vector3.zero, 0);

        ShowConfirmEmail(true);
        ShowNewPassword(false);
    }

    public void Show(bool show, UnityAction callback = null)
    {
        if(!show)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            canvasGroup.gameObject.SetActive(false);
        }
        else
            canvasGroup.gameObject.SetActive(true);

        canvasGroup.DOFade(show ? 1 : 0, 0.5f).OnComplete(()=> 
        {
            if (show)
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }

            callback?.Invoke();
        });
    }

    public void ShowSignIn(bool show, UnityAction callback = null)
    {
        signInRect.DOScale(show ? Vector3.one : Vector3.zero, 0.2f)
        .OnComplete(() =>
        {
            callback?.Invoke();
        });
    }

    public void ShowSignUp(bool show, UnityAction callback = null)
    {
        signUpRect.DOScale(show ? Vector3.one : Vector3.zero, 0.2f)
        .OnComplete(()=> 
        {
            callback?.Invoke();
        });
    }

    public void ShowForgotPassword(bool show, UnityAction callback = null)
    {
        forgotPasswordRect.DOScale(show ? Vector3.one : Vector3.zero, 0.2f)
        .OnComplete(() =>
        {
            callback?.Invoke();
        });
    }

    public void ShowConfirmEmail(bool show)
    {
        typeEmailRect.SetActive(show);
    }

    public void ShowNewPassword(bool show)
    {
        typeNewPasswordRect.SetActive(show);
    }
}

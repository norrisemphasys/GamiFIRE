using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupView : MonoBehaviour
{
    public RectTransform popupRect;

    [SerializeField] TextMeshProUGUI textTitle;
    [SerializeField] TextMeshProUGUI textDescription;
    [SerializeField] TextMeshProUGUI textOk;
    [SerializeField] TextMeshProUGUI textCancel;

    [SerializeField] Button buttonOk;
    [SerializeField] Button buttonCancel;
    [SerializeField] Button buttonClose;

    public static PopupView Create(Transform parent, PopupData data)
    {
        GameObject viewPrefab = Resources.Load<GameObject>("Prefabs/UI/Views/PopupView");
        GameObject viewGO = GameObject.Instantiate(viewPrefab);
        viewGO.transform.SetParent(parent);

        // Reset position
        viewGO.transform.localPosition = Vector3.zero;
        viewGO.transform.localRotation = Quaternion.identity;
        viewGO.transform.localScale = Vector3.one;

        PopupView view = viewGO.GetComponent<PopupView>();
        view.popupRect.DOScale(Vector3.zero, 0);
        view.Create(data);

        return view;
    }

    public static PopupData CreatePopupData(string title, string description, string textOk = "OK", string textCancel = "Cancel", 
        bool showOk = true, bool showCancel = true, bool showClose = true, 
        UnityAction onClickOk = null, UnityAction onClickCancel = null, UnityAction onClickClose = null, UnityAction onClosePopup = null)
    {
        PopupData data = new();

        data.Title = title;
        data.Description = description;
        data.TxtButtonOK = textOk;
        data.TxtButtonCancel = textCancel;

        data.ShowOk = showOk;
        data.ShowCancel = showCancel;
        data.ShowClose = showClose;

        data.OnClickOk += onClickOk;
        data.OnClickCancel += onClickCancel;
        data.OnClickClose += onClickClose;

        data.OnClosePopup += onClosePopup;

        return data;
    }

    public void Show(bool show, UnityAction onClose = null)
    {
        popupRect.DOScale(show ? Vector3.one : Vector3.zero, 0.2f).OnComplete(()=> 
        {
            if (!show) onClose?.Invoke();
        });
    }

    public void Create(PopupData data)
    {
        textTitle.text = data.Title;
        textDescription.text = data.Description;

        textOk.text = data.TxtButtonOK;
        textCancel.text = data.TxtButtonCancel;

        buttonOk.onClick.AddListener(() =>
        {
            data.OnClickOk?.Invoke();
            Show(false, () =>
            {
                data.OnClosePopup?.Invoke();
                DestroyPopup();
            });
        });

        buttonCancel.onClick.AddListener(() =>
        {
            data.OnClickCancel?.Invoke();
            Show(false, () =>
            {
                data.OnClosePopup?.Invoke();
                DestroyPopup();
            });
        });
    
        buttonClose.onClick.AddListener(()=>
        {
            data.OnClickClose?.Invoke();
            Show(false, ()=> 
            {
                data.OnClosePopup?.Invoke();
                DestroyPopup(); 
            });
        });

        buttonOk.gameObject.SetActive(data.ShowOk);
        buttonCancel.gameObject.SetActive(data.ShowCancel);
        buttonClose.gameObject.SetActive(data.ShowClose);
    }

    public void DestroyPopup()
    {
        buttonOk.onClick.RemoveAllListeners();
        buttonCancel.onClick.RemoveAllListeners();
        buttonClose.onClick.RemoveAllListeners();

        GameObject.Destroy(this.gameObject);
    }
}


public struct PopupData
{
    public string Title;
    public string Description;
    public string TxtButtonOK;
    public string TxtButtonCancel;

    public bool ShowOk;
    public bool ShowCancel;
    public bool ShowClose;

    public UnityAction OnClickOk;
    public UnityAction OnClickCancel;
    public UnityAction OnClickClose;

    public UnityAction OnClosePopup;
}
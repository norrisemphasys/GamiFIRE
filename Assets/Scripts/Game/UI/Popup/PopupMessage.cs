using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class PopupMessage
{
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

    public static PopupData ErrorPopup(string message, UnityAction OnClickOk = null)
    {
        return CreatePopupData("ERROR", message, showCancel:false, showClose: false, onClickOk: OnClickOk);
    }

    public static PopupData InfoPopup(string message, UnityAction OnClickOk = null)
    {
        return CreatePopupData("INFORMATION", message, showCancel: false, showClose: false, onClickOk: OnClickOk);
    }

    public static PopupData OptionPopup(string message, string ok, string cancel , UnityAction OnClickOk)
    {
        return CreatePopupData("SELECT", message, ok, cancel, showClose: false, onClickOk: OnClickOk);
    }

    public static PopupData ClaimPopup(string message, UnityAction OnClickOk = null)
    {
        return CreatePopupData("INFORMATION", message,textOk: "CLAIM", showCancel: false, showClose: false, onClickOk: OnClickOk);
    }
}


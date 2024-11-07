using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMessage;

    public static NotificationView Create(Transform parent, string message)
    {
        GameObject viewPrefab = Resources.Load<GameObject>("Prefabs/UI/Views/NotificationView");
        GameObject viewGO = GameObject.Instantiate(viewPrefab);
        viewGO.transform.SetParent(parent);

        // Reset position
        viewGO.transform.localPosition = Vector3.zero;
        viewGO.transform.localRotation = Quaternion.identity;
        viewGO.transform.localScale = Vector3.one;

        NotificationView view = viewGO.GetComponent<NotificationView>();
        view.SetTextMessage(message);

        viewGO.SetActive(false);

        return view;
    }

    public void SetTextMessage(string message)
    {
        textMessage.text = message;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Utils.Delay(this, () => 
        {
            DestroyView();
        }, 5f);
    }

    public void DestroyView()
    {
        GameObject.Destroy(this.gameObject);
    }
}

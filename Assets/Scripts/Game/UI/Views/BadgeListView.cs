using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Proyecto26;
using UnityEngine.Networking;

public class BadgeListView : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI textTitle;
    [SerializeField] TextMeshProUGUI textDescription;

    public void SetBadgeData(Badge badge)
    {
        /* RestClient.Request(new RequestHelper { Uri = badge.imgURL, Method = "GET",
             DefaultContentType = false
         }).Then(res=> 
         {
             Texture2D texture = ((DownloadHandlerTexture)res.Request.downloadHandler).texture;
             Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 100, 100), new Vector2());

             icon.sprite = sprite;
             icon.preserveAspect = true;
         });*/

        StartCoroutine(DownloadTextureEnun(badge.imgURL));

        textTitle.text = badge.name;
        textDescription.text = badge.description;
    }

    IEnumerator DownloadTextureEnun(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 1024, 1024), new Vector2(0.5f, 0.5f));

            icon.sprite = sprite;
            icon.preserveAspect = true;
        }
        else
        {
            Debug.LogError("Error Download");
        }
    }
}

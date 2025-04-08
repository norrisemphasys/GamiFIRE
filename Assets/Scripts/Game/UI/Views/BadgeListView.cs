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

    [SerializeField] GameObject lockedGO;
    [SerializeField] GameObject unlockedGO;

    public Sprite sprite { get { return icon.sprite; } }
    public string title { get { return textTitle.text; } }
    public string description { get { return content; } }

    private string content;

    public void SetBadgeData(BadgeInfoSO info)
    {
        textTitle.text = info.title;
        textDescription.text = info.description;

        if(info.badgeID.Equals("67ed253c4dc5c989a8cf5cf5"))
            content = info.description;
        else
            content = info.description + "\n\n" + "<b>Point of Attention : </b>" + info.pointOfAttention;

        icon.sprite = info.icon;

        lockedGO.SetActive(info.locked);
        unlockedGO.SetActive(!info.locked);
    }

    public void SetBadgeData(Badge badge)
    {
       /* RestClient.Request(new RequestHelper
        {
            Uri = badge.imgURL,
            Method = "GET",
            Headers = new Dictionary<string, string> {
            {
                "Authorization", $"Bearer {BadgeManager.BadgeToken}"}
            },
            DownloadHandler = new DownloadHandlerFile(badge.imgURL),
        }).Then(res =>
        {
            Texture2D texture = ((DownloadHandlerTexture)res.Request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 1024, 1024), new Vector2());

            icon.sprite = sprite;
            icon.preserveAspect = true;
        }).Catch(error =>
        {
            Debug.LogError(error);
        });*/
        GetComponent<Button>().enabled = false;

        StartCoroutine(DownloadTextureEnun(badge.imgURL));

        textTitle.text = badge.name;
        textDescription.text = badge.description;
    }

    IEnumerator DownloadTextureEnun(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

       /* www.SetRequestHeader("Access-Control-Allow-Origin", "*");
        www.SetRequestHeader("Access-Control-Allow-Headers", "Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers");
        www.SetRequestHeader("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");*/

        /*www.SetRequestHeader("Authorization", $"Bearer {BadgeManager.BadgeToken}");*/

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            icon.sprite = sprite;
            icon.preserveAspect = true;

            GetComponent<Button>().enabled = true;
        }
        else
        {
            Debug.LogError("Error Download");
        }
    }
}

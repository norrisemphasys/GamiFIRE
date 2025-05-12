using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;
using DG.Tweening;

public class QRCodeManager : MonoSingleton<QRCodeManager>
{
    [SerializeField] RawImage _QRCodeImage;
    [SerializeField] Button buttonClose;
    [SerializeField] CanvasGroup canvasGroup;

    private Texture2D _storeEncodedTexture;

    private void Start()
    {
        _storeEncodedTexture = new Texture2D(256, 256);
    }

    private Color32 [] Encode(string text, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new ZXing.Common.EncodingOptions { Height = height, Width = width}
        };

        return writer.Write(text);
    }

    void GenerateQRCode(string text)
    {
        Color32[] convertPixelToTexture = Encode(text, _storeEncodedTexture.width, _storeEncodedTexture.height);
        _storeEncodedTexture.SetPixels32(convertPixelToTexture);
        _storeEncodedTexture.Apply();

        _QRCodeImage.texture = _storeEncodedTexture;
    }

    public void ShowQRCode(bool show, string text = "")
    {
        if(show)
        {
            canvasGroup.DOKill();
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            canvasGroup.gameObject.SetActive(true);

            canvasGroup.DOFade(1f, 0.2f)
           .OnComplete(() =>
           {
               GenerateQRCode(text);
               buttonClose.onClick.AddListener(OnClickQR);
           }).SetUpdate(true);
        }
        else
        {
            buttonClose.onClick.RemoveListener(OnClickQR);

            canvasGroup.DOKill();
            canvasGroup.interactable = false;

            canvasGroup.DOFade(0f, 0.2f)
           .OnComplete(() =>
           {
               canvasGroup.blocksRaycasts = false;
               canvasGroup.gameObject.SetActive(false);
           }).SetUpdate(true);
        }
    }

    void OnClickQR()
    {
        ShowQRCode(false);
    }
}

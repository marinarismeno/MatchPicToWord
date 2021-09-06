using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class ImageButtonScript : MonoBehaviour
{
    public GameObject MasterCanvas;
    public Button imageButton;
    private Image imageUI;
    public string answer;
    public bool selected;
    public GameObject background;


    public void Init(GameObject prefab, string text, string imageUrl)
    {
        answer = text;
        imageUI = imageButton.image;
        LoadImage(imageUrl);
        selected = false;
        SetBackgroundColor("transparent");
        MasterCanvas = GenerateButtonsScript.MasterCanvas;
    }

    public void Selected()
    {
        MasterCanvas.GetComponent<SelectMatchChoice>().ButtonPressed(this.gameObject);
    }

    public void SetBackgroundColor(string colorChoice)
    {
        Image image = background.GetComponent<Image>();
        var tempcolor = image.color;
        if (colorChoice.Equals("green"))
        {
            tempcolor.a = 255f;
        }
        else if (colorChoice.Equals("transparent"))
        {
            tempcolor.a = 0;
        }
        else
        {
            tempcolor = new Color(248, 196, 32);
        }
        image.color = tempcolor;
    }

    public void LoadImage(string URL)
    {

        if (!string.IsNullOrEmpty(URL))
        {
            imageUI.enabled = true;
            StartCoroutine(LoadImageFromURL(URL, this.imageUI));
        }
        else
        {
            //if there is no url the image gets disabled.
            imageUI.enabled = false;
        }
    }

    public IEnumerator LoadImageFromURL(string URL, Image cell)
    {
        // Check internet connection
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            yield return null;
        }
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(URL);
        yield return www.SendWebRequest();

        Texture2D loadedTexture = DownloadHandlerTexture.GetContent(www);
        cell.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
    }
}

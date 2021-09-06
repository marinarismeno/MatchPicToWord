using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WordButtonScript : MonoBehaviour
{
    public GameObject MasterCanvas;
    public TextMeshProUGUI wordObject;
    public string answer;
    public bool selected;
    public GameObject background;

    public void Init(GameObject prefab, string word)
    {
        wordObject.text = word;
        answer = word;
        selected = false;
        SetBackground(false);
        MasterCanvas = GenerateButtonsScript.MasterCanvas;
    }

    public void Selected()
    {
        MasterCanvas.GetComponent<SelectMatchChoice>().ButtonPressed(gameObject);
    }

    public void SetBackground(bool correct)
    {
        Image image = background.GetComponent<Image>();
        var tempcolor = image.color;
        if (correct)
        {
            tempcolor.a = 255f;
        }
        else
        {           
            tempcolor.a = 0;
        }
        image.color = tempcolor;
    }


}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadDictionary : MonoBehaviour
{
    public GameObject importButton;
    public GameObject wordGroup;
    public GameObject imageGroup;
    public Dictionary<string, string> InputWords;

    private void Start()
    {
        DeactivateStartUI();
    }
    public void ImportFile()
    {
        InputWords = new Dictionary<string, string>();

        string path = "Assets/Resources/input.txt";
        //string path = Application.persistentDataPath + "input.txt";
        StreamReader reader = new StreamReader(path);
        string textFile = reader.ReadToEnd();
        reader.Close();

        string[] textLines = textFile.Split('\n');
        foreach ( string line in textLines)
        {
            string[] temp = line.Split(':');
            InputWords[temp[0]] = temp[1];

            for (int i= 2; i< temp.Length; i++)
            {
                InputWords[temp[0]] += ":" + temp[i];

            }
        }
        if (InputWords.Count == 0)
        {
            this.GetComponent<SelectMatchChoice>().ShowResultText("No data to display, please place a input.txt file into the path: " + path +" and try again", 3);
        }
        else
        {
            ActivateMainUI();
        }
        
    }

    public void ActivateMainUI()
    {
        importButton.gameObject.SetActive(false);
        this.GetComponent<GenerateButtonsScript>().GenerateButtons();

    }

    public void DeactivateStartUI()
    {
        importButton.gameObject.SetActive(true);
    }

    public Dictionary<string, string> getDictionary()
    {
        return InputWords;
    }
}

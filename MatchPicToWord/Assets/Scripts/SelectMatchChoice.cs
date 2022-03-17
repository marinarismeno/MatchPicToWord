using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectMatchChoice : MonoBehaviour
{
    public GameObject WordGroup;
    public GameObject ImageGroup;
    public GameObject ResultPanel;
    public GameObject ScreenshotButton;

    public string imageChoice;
    public GameObject imageObj;
    public string wordChoice;
    public GameObject wordObj;
    public int correctMatches;

    public GameObject linePrefab;
    public bool isDrawing;
    GameObject drawLine;

    bool isImageObjectClicked;
    private void Start()
    {
        InitLocalChoices();
        isDrawing = false;
        correctMatches = 0;
    }

    private void Update()
    {
        if (isDrawing)
        {
            drawLine.GetComponent<LineController>().KeepDrawing();
        }
    }
    public void ButtonPressed(GameObject buttonObj)
    {
        if (imageObj == null && wordObj == null)
        {
            //Start Drawing Line
            isDrawing = true;
            StartLine();
        }
        else
        {
            // final stop of the line - connect the buttons
            isDrawing = false;
            EndLine();
        }
        WasSameGroupSelected();
        isImageObjectClicked = (buttonObj.GetComponent<WordButtonScript>() == null);
        //was it pressed on an image button?
        if (isImageObjectClicked)
        {
            // set selected image object
            buttonObj.GetComponent<ImageButtonScript>().selected = true;
            imageChoice = buttonObj.GetComponent<ImageButtonScript>().answer;
            imageObj = buttonObj;
            //Debug.Log("The image " + imageChoice + " got selected");
        }
        else
        {
            // set selected word object
            buttonObj.GetComponent<WordButtonScript>().selected = true;
            wordChoice = buttonObj.GetComponent<WordButtonScript>().answer;
            wordObj = buttonObj;
            //Debug.Log("The word " + wordChoice + " got selected");

        }



        CheckCorrectness();
    }

    public void StartLine()
    {
        drawLine = Instantiate(linePrefab, WordGroup.transform);
        drawLine.GetComponent<LineController>().StartDrawingLine(Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9f));

        //drawLine.GetComponent<LineController>().StartDrawingLine(buttonObj.transform);
    }
    public void EndLine()
    {
        //GameObject drawLine = Instantiate(linePrefab, WordGroup.transform);
        drawLine.GetComponent<LineController>().EndDrawingLine(Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9f));
    }

    /**
     * Check if only one group has been selected
     * and if yes then
     * Check if there is another one selected in the same group and deselect it
     * 
     */
    public bool WasSameGroupSelected()
    {
        if (imageObj == null && wordObj == null)
        {
            return true;
        }
        else if (imageObj != null && wordObj == null)
        {
            imageObj.GetComponent<ImageButtonScript>().selected = false;
            return true;
        }
        else if (wordObj != null && imageObj == null)
        {
            wordObj.GetComponent<WordButtonScript>().selected = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void CheckCorrectness()
    {

        if (imageChoice.Equals(wordChoice))
        {
            ShowResultText("Correct Match!", 1f);
            correctMatches++;
            //turn correct buttons green
            wordObj.GetComponent<WordButtonScript>().SetBackground(true);
            imageObj.GetComponent<ImageButtonScript>().SetBackgroundColor("green");

            // toggle interactable button
            imageObj.GetComponent<Button>().interactable = false;
            wordObj.GetComponent<Button>().interactable = false;

            ResetChoices();
            IsGameCompleted();
        }
        else if (imageChoice.Equals("") || wordChoice.Equals(""))
        {
            return;
        }
        else
        {
            ResetChoices();
            ShowResultText("Oops, This is not right, try again", 1f);
            drawLine.GetComponent<LineController>().DeleteLine();
        }

    }

    public void ShowResultText(string resultText, float seconds)
    {
        ResultPanel.SetActive(true);
        ResultPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = resultText;
        Invoke("HideResultText", seconds);
    }

    public void HideResultText()
    {
        ResultPanel.SetActive(false);
    }

    public void InitLocalChoices()
    {
        wordObj = null;
        imageObj = null;
        wordChoice = "";
        imageChoice = "";
    }
    public void ResetChoices()
    {
        //reset the selected tags
        wordObj.GetComponent<WordButtonScript>().selected = false;
        imageObj.GetComponent<ImageButtonScript>().selected = false;
        InitLocalChoices();
    }

    public bool IsGameCompleted()
    {
        if(this.GetComponent<LoadDictionary>().InputWords.Count == correctMatches)
        {
            ShowResultText("The game is complete! Congratulations!", 1.5f);
            ScreenshotButton.SetActive(true);
            return true;
        }
        return false;
    }


}

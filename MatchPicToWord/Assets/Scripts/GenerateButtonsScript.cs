using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateButtonsScript : MonoBehaviour
{
    public GameObject WordButtonPrefab;
    public GameObject ImageButtonPrefab;
    public GameObject WordGroup;
    public GameObject ImageGroup;

    public static GameObject MasterCanvas;


    private void Awake()
    {
        MasterCanvas = gameObject;
    }
    public void GenerateButtons()
    {
        Dictionary<string, string> wordict = this.GetComponent<LoadDictionary>().getDictionary();
        //ClearPanel();

        foreach (KeyValuePair<string, string> pair in wordict)
        {
            GameObject wordInstance = Instantiate(WordButtonPrefab, WordGroup.transform);
            GameObject imageInstance = Instantiate(ImageButtonPrefab, ImageGroup.transform);

            wordInstance.transform.GetChild(0).GetComponent<WordButtonScript>().Init(WordButtonPrefab, pair.Key);
            imageInstance.transform.GetChild(0).GetComponent<ImageButtonScript>().Init(ImageButtonPrefab, pair.Key, pair.Value);
        }
        ShuffleWordButtons();
    }

    private void ShuffleWordButtons()
    {
        int numOfItems = WordGroup.transform.childCount;
        for (int i=0; i< numOfItems; i++)
        {
            int newpos = Random.Range(0, numOfItems - 1);
            WordGroup.transform.GetChild(i).SetSiblingIndex(newpos);
        }

    }

    /*
    public void ClearPanel()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy((this.transform.GetChild(i).gameObject));
        }
    } 
    */
}

using UnityEngine;
using TMPro;
using System.Text;
using System;

public class KanjiLookup : MonoBehaviour
{
    [SerializeField] TMP_InputField kanjiInputField;

    [Header("Screen Views")]
    [SerializeField] GameObject inputMenu;
    [SerializeField] GameObject searchingForKanjiScreen;
    [SerializeField] GameObject kanjiDetailsMenu;

    [Header("Kanji Details UI")]
    [SerializeField] TextMeshProUGUI kanjiCharacterText;
    [SerializeField] TextMeshProUGUI meaningsColumn1Text;
    [SerializeField] TextMeshProUGUI meaningsColumn2Text;
    [SerializeField] TextMeshProUGUI onYomiText;
    [SerializeField] TextMeshProUGUI kunYomiText;

    Kanji currentKanji;

    public void FindKanjiCharacter()
    {
        if (kanjiInputField.text == "") return;
        KanjiService.GetInstance().RequestSingleKanji(kanjiInputField.text, SetKanjiDetails, FailedToFindKanji);
        inputMenu.SetActive(false);
        searchingForKanjiScreen.SetActive(true);
    }

    public void SetKanjiDetails(Kanji kanji)
    {
        kanjiCharacterText.text = kanji.kanji;

        string[] meanings = kanji.meanings;
        int meaningsMid = meanings.Length / 2;
        string[] meaningsFirstHalf = new string[meaningsMid];
        string[] meaningsSecondHalf = new string[meanings.Length - meaningsMid];

        Array.Copy(meanings, 0, meaningsFirstHalf, 0, meaningsMid);
        Array.Copy(meanings, meaningsMid, meaningsSecondHalf, 0, meanings.Length - meaningsMid);

        meaningsColumn1Text.text = ReturnAppendedString(meaningsFirstHalf); 
        meaningsColumn2Text.text = ReturnAppendedString(meaningsSecondHalf); 
        onYomiText.text = ReturnAppendedString(kanji.on_readings);
        kunYomiText.text = ReturnAppendedString(kanji.kun_readings);
        searchingForKanjiScreen.SetActive(false);
        kanjiDetailsMenu.SetActive(true);
        currentKanji = kanji;
    }

    public void AddKanjiToStudyCards()
    {
        KanjiService.GetInstance().AddKanjiToStudyCards(currentKanji);
        //Change the button state to say: "Added" or "Remove from study list" or something
    }

    public void GoBackToLookupMenu()
    {
        searchingForKanjiScreen.SetActive(false);
        kanjiDetailsMenu.SetActive(false);
        kanjiInputField.text = "";
        inputMenu.SetActive(true);
        currentKanji = null;
    }

    public void FailedToFindKanji()
    {
        //Display an error message saying it could not be found
        GoBackToLookupMenu();
    }

    private string ReturnAppendedString(string[] strings)
    {
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < strings.Length; i++)
        {
            sb.Append(strings[i]);
            if(i < strings.Length - 1)
            {
                sb.Append("\n");
            }
        }

        return sb.ToString();
    }

    //I need a failed to find kanji screen as well
}

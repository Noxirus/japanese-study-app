using UnityEngine;
using TMPro;
using System.Text;

public class KanjiLookup : MonoBehaviour
{
    [SerializeField] TMP_InputField kanjiInputField;

    [Header("Screen Views")]
    [SerializeField] GameObject inputMenu;
    [SerializeField] GameObject searchingForKanjiScreen;
    [SerializeField] GameObject kanjiDetailsMenu;

    [Header("Kanji Details UI")]
    [SerializeField] TextMeshProUGUI kanjiCharacterText;
    [SerializeField] TextMeshProUGUI meaningsText;
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
        Debug.Log("Found kanji");
        kanjiCharacterText.text = kanji.kanji;
        meaningsText.text = ReturnAppendedString(kanji.meanings);   
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

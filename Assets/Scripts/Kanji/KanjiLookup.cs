using UnityEngine;
using TMPro;

public class KanjiLookup : MonoBehaviour
{
    static KanjiLookup instance;

    [SerializeField] TMP_InputField kanjiInputField;

    [Header("Screen Views")]
    [SerializeField] GameObject inputMenu;
    [SerializeField] GameObject searchingForKanjiScreen;

    [Header("Kanji Details UI")]
    [SerializeField] TextMeshProUGUI kanjiCharacterText;
    [SerializeField] TextMeshProUGUI meaningsText;
    [SerializeField] TextMeshProUGUI onYomiText;
    [SerializeField] TextMeshProUGUI kunYomiText;
    [SerializeField] TextMeshProUGUI addOrRemoveCardButtonText;

    KanjiService kanjiService;

    [SerializeField] KanjiDetails kanjiDetails;

    private void Start()
    {
        instance = this;
        kanjiService = KanjiService.GetInstance();
    }

    public static KanjiLookup GetInstance()
    {
        return instance;
    }

    public void FindKanjiCharacter()
    {
        if (kanjiInputField.text == "") return;
        inputMenu.SetActive(false);
        searchingForKanjiScreen.SetActive(true);
        kanjiService.RequestSingleKanji(kanjiInputField.text, OpenKanjiDetails, FailedToFindKanji);
    }

    public void OpenKanjiDetails(Kanji kanji)
    {
        kanjiDetails.gameObject.SetActive(true);
        kanjiDetails.SetKanjiDetails(kanji);
        ResetInputScreen();
    }


    public void ResetInputScreen()
    {
        searchingForKanjiScreen.SetActive(false);
        kanjiInputField.text = "";
        inputMenu.SetActive(true);
    }

    public void FailedToFindKanji()
    {
        //Display an error message saying it could not be found
        ResetInputScreen();
    }
    //I need a failed to find kanji screen as well
}

using TMPro;
using UnityEngine;

public class KanjiDetails : MonoBehaviour
{
    [Header("Kanji Details UI")]
    [SerializeField] TextMeshProUGUI kanjiCharacterText;
    [SerializeField] TextMeshProUGUI meaningsText;
    [SerializeField] TextMeshProUGUI onYomiText;
    [SerializeField] TextMeshProUGUI kunYomiText;
    [SerializeField] TextMeshProUGUI addOrRemoveCardButtonText;

    Kanji currentKanji;
    KanjiService kanjiService;


    private void Start()
    {
        kanjiService = KanjiService.GetInstance();
    }

    public void SetKanjiDetails(Kanji kanji)
    {
        kanjiCharacterText.text = kanji.kanji;

        meaningsText.text = HelperMethods.ReturnAppendedString(kanji.meanings, ", ");
        onYomiText.text = HelperMethods.ReturnAppendedString(kanji.on_readings);
        kunYomiText.text = HelperMethods.ReturnAppendedString(kanji.kun_readings);
        currentKanji = kanji;

        UpdateAddORRemoveCardButtonText();
    }

    public void UpdateToStudyCards()
    {
        if (kanjiService.HasInStudyCards(currentKanji.kanji))
        {
            kanjiService.RemoveFromStudyCards(currentKanji.kanji);
        }
        else
        {
            kanjiService.AddKanjiToStudyCards(currentKanji);
        }

        UpdateAddORRemoveCardButtonText();
    }

    void UpdateAddORRemoveCardButtonText()
    {
        if(kanjiService == null)
        {
            kanjiService = KanjiService.GetInstance();
        }

        addOrRemoveCardButtonText.text = kanjiService.HasInStudyCards(currentKanji.kanji) ? "Remove Card" : "Add To Deck";
    }

    public void CloseKanjiDetails()
    {
        gameObject.SetActive(false);
    }
}

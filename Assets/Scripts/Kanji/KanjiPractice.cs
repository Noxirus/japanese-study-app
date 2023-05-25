using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KanjiPractice : MonoBehaviour
{
    List<Kanji> kanjiList;

    Kanji currentKanji;
    bool bIsRomanjiQuestion = true;

    [SerializeField] TextMeshProUGUI kanjiText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI wordText;

    [SerializeField] GameObject answerButton;
    [SerializeField] GameObject newKanjiButton;

    [Header("On'yomi Reading")]
    [SerializeField] TextMeshProUGUI sinoHiraganaText;
    [Header("Kun'yomi Reading")]
    [SerializeField] TextMeshProUGUI nativeHiraganaText;

    private void OnEnable()
    {
        kanjiList = KanjiService.GetInstance().ReturnStudyCards();
    }

    public void DisplayQuestion()
    {
        bIsRomanjiQuestion = Random.Range(0, 2) == 0 ? true : false;
        currentKanji = kanjiList[Random.Range(0, kanjiList.Count)];

        if (bIsRomanjiQuestion)
        {
            kanjiText.text = "";
            kanjiText.gameObject.SetActive(false);
            descriptionText.text = "What is the kanji for this word?";

            int randomMeaning = Random.Range(0, currentKanji.meanings.Length);
            wordText.text = currentKanji.meanings[randomMeaning];

            wordText.gameObject.SetActive(true);
        }
        else
        {
            descriptionText.text = "What does this kanji represent?";
            kanjiText.text = currentKanji.kanji;
            kanjiText.gameObject.SetActive(true);
            wordText.gameObject.SetActive(false);
        }

        sinoHiraganaText.gameObject.SetActive(false);
        nativeHiraganaText.gameObject.SetActive(false);
        newKanjiButton.SetActive(false);
        answerButton.SetActive(true);
    }

    public void DisplayAnswer()
    {
        descriptionText.text = "The answer is: ";
        if (bIsRomanjiQuestion)
        {
            wordText.text = "";
            wordText.gameObject.SetActive(false);
            kanjiText.text = currentKanji.kanji;
            kanjiText.gameObject.SetActive(true);
        }
        else
        {
            kanjiText.text = "";
            kanjiText.gameObject.SetActive(false);
            wordText.text = HelperMethods.ReturnAppendedString(currentKanji.meanings, ", ");
            wordText.gameObject.SetActive(true);
        }

        sinoHiraganaText.text = HelperMethods.ReturnAppendedString(currentKanji.on_readings);
        sinoHiraganaText.gameObject.SetActive(true);
        nativeHiraganaText.text = HelperMethods.ReturnAppendedString(currentKanji.kun_readings);
        nativeHiraganaText.gameObject.SetActive(true);

        answerButton.SetActive(false);
        newKanjiButton.SetActive(true);
    }
}

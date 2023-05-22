using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HiraganaKatakanaPractice : MonoBehaviour
{
    [SerializeField] List<Character> japaneseCharacters;

    Character currentCharacter = null;
    bool bIsHiragana;

    [Header("UI Elements")]
    [SerializeField] GameObject characterContainer;
    [SerializeField] TextMeshProUGUI romanjiText;
    [SerializeField] TextMeshProUGUI scriptText;
    [SerializeField] Image answerImage;
    [SerializeField] GameObject newCharacterButton;
    [SerializeField] GameObject showAnswerButton;

    public void DisplayQuestion()
    {
        int randomChoice = Random.Range(0, japaneseCharacters.Count);

        if(currentCharacter == japaneseCharacters[randomChoice])
        {
            DisplayQuestion();
            return;
        }

        currentCharacter = japaneseCharacters[randomChoice];

        romanjiText.text = currentCharacter.romanji;
        answerImage.gameObject.SetActive(false);
        characterContainer.SetActive(true);

        newCharacterButton.SetActive(false);
        showAnswerButton.SetActive(true);

        bIsHiragana = Random.Range(0, 2) == 0 ? true : false;
        scriptText.text = bIsHiragana ? "In Hiragana" : "In Katakana";
    }

    public void ShowAnswer()
    {
        answerImage.sprite = bIsHiragana ? currentCharacter.hiragana : currentCharacter.katakana;
        characterContainer.SetActive(false);
        answerImage.gameObject.SetActive(true);
        showAnswerButton.SetActive(false);
        newCharacterButton.SetActive(true);
    }
}

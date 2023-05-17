using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WritingMenu : MonoBehaviour
{
    [SerializeField] List<Character> japaneseCharacters;

    Character currentCharacter = null;

    [SerializeField] GameObject characterContainer;
    [SerializeField] TextMeshProUGUI romanjiText;
    [SerializeField] TextMeshProUGUI scriptText;
    [SerializeField] Image answerImage;

    bool isHiragana;

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

        isHiragana = Random.Range(0, 2) == 0 ? true : false;
        scriptText.text = isHiragana ? "In Hiragana" : "In Katakana";
    }

    public void ShowAnswer()
    {
        answerImage.sprite = isHiragana ? currentCharacter.hiragana : currentCharacter.katakana;
        characterContainer.SetActive(false);
        answerImage.gameObject.SetActive(true);
    }
}

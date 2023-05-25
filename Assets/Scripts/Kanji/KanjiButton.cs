using UnityEngine;
using TMPro;

public class KanjiButton : MonoBehaviour
{
    Kanji assignedKanji;

    [SerializeField] TextMeshProUGUI kanjiButtonText;

    public void SetKanjiText(Kanji kanji)
    {
        assignedKanji = kanji;
        kanjiButtonText.text = kanji.kanji;
    }


    public void SelectKanji()
    {
        MenuManager.GetInstance().LookupKanji(assignedKanji);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class StudyCardLibrary : MonoBehaviour
{
    [SerializeField] GameObject kanjiButtonPrefab;
    [SerializeField] GameObject contentSection;

    List<KanjiButton> kanjiButtonList = new List<KanjiButton>();

    //I need a listener to know when to update this if the user removes something
    private void OnEnable()
    {
        DeactivateAllButtons();
        List<Kanji> studyCardList = KanjiService.GetInstance().ReturnStudyCards();

        for(int i = 0; i < studyCardList.Count; i++)
        {
            if(kanjiButtonList.Count <= i)
            {
                GameObject newButton = Instantiate(kanjiButtonPrefab, contentSection.transform);
                kanjiButtonList.Add(newButton.GetComponent<KanjiButton>());
            }
            kanjiButtonList[i].SetKanjiText(studyCardList[i]);
            kanjiButtonList[i].gameObject.SetActive(true);
        }
    }

    void DeactivateAllButtons()
    {
        for(int i = 0; i < kanjiButtonList.Count; i++)
        {
            kanjiButtonList[i].gameObject.SetActive(false);
        }
    }
}

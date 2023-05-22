using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EMenu
{
    main = 0,
    writing = 1,
    kanji = 2,
    MAX = 100,
}

public class MenuManager : MonoBehaviour
{
    
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject writingMenu;
    [SerializeField] GameObject kanjiMenu;

    EMenu currentMenu = EMenu.main;

    public void SelectMainMenu()
    {
        if(currentMenu == EMenu.main) { return; }
        writingMenu.SetActive(false);
        mainMenu.SetActive(true);
        currentMenu = EMenu.writing;

    }

    public void SelectWritingPractice()
    {
        if(currentMenu == EMenu.writing) { return; }

        mainMenu.SetActive(false);
        writingMenu.SetActive(true);
        currentMenu = EMenu.writing;
    }

    public void SelectKanjiPractice()
    {
        if(currentMenu == EMenu.kanji) { return; }

        mainMenu.SetActive(false);
        kanjiMenu.SetActive(true);
        currentMenu = EMenu.kanji;
    }
}

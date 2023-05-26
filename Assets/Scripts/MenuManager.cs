using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EMenu
{
    main = 0,
    writing = 1,
    kanjiLookup = 2,
    kanjiPractice = 3,
    kanjiCardLibrary = 4,
    MAX = 100,
}

public class MenuManager : MonoBehaviour
{
    Dictionary<EMenu, GameObject> menuDictionary = new Dictionary<EMenu, GameObject>();

    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject writingMenu;
    [SerializeField] GameObject kanjiStudyMenu;
    [SerializeField] GameObject kanjiLookupMenu;
    [SerializeField] GameObject kanjiCardLibraryMenu;

    [Header("Overlay Menus")]
    [SerializeField] KanjiDetails kanjiDetails;

    static MenuManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static MenuManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        menuDictionary.Add(EMenu.main, mainMenu);
        menuDictionary.Add (EMenu.writing, writingMenu);
        menuDictionary.Add(EMenu.kanjiPractice, kanjiStudyMenu);
        menuDictionary.Add(EMenu.kanjiLookup, kanjiLookupMenu);
        menuDictionary.Add(EMenu.kanjiCardLibrary, kanjiCardLibraryMenu);
    }

    public void SelectMenu(EMenu menu)
    {
        DeactivateAllMenus();
        menuDictionary[menu].SetActive(true);
    }

    void DeactivateAllMenus()
    {
        foreach(KeyValuePair<EMenu, GameObject> kvp in menuDictionary)
        {
            kvp.Value.SetActive(false);
        }
    }

    public void LookupKanji(Kanji kanji)
    {
        kanjiDetails.gameObject.SetActive(true);
        kanjiDetails.SetKanjiDetails(kanji);
    }
}

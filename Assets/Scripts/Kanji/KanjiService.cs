using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class KanjiService : MonoBehaviour
{
    Dictionary<string, Kanji> kanjiDictionary = new Dictionary<string, Kanji>();
    Dictionary<string, Kanji> kanjiStudyCards = new Dictionary<string, Kanji>();

    static KanjiService instance;

    const string kanjiAPIUrl = "https://kanjiapi.dev/v1/";

    const string PLAYER_PREFS_STRING = "savedKanji";

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        string savedKanji = PlayerPrefs.GetString(PLAYER_PREFS_STRING);

        Debug.Log("saved Kanji: " + savedKanji);
        if (savedKanji == null)
        {
            PlayerPrefs.SetString(PLAYER_PREFS_STRING, "");
            PlayerPrefs.Save();
        }
        else
        {
            for(int i = 0; i < savedKanji.Length; i++)
            {
                RequestSingleKanji(savedKanji[i].ToString(), AddKanjiToStudyCards, FailedToFindCharacter);
            }
        }
    }

    public void FailedToFindCharacter()
    {
        Debug.Log("Failed to find certain character");
    }

    public static KanjiService GetInstance()
    {
        return instance;
    }

    public List<Kanji> ReturnStudyCards()
    {
        List<Kanji> returnList = new List<Kanji>();
        foreach(KeyValuePair<string, Kanji> kvp in kanjiStudyCards)
        {
            returnList.Add(kvp.Value);
        }

        return returnList;
    }

    public void AddKanjiToStudyCards(Kanji kanji)
    {
        if (kanjiStudyCards.ContainsKey(kanji.kanji)) { return; }
        kanjiStudyCards.Add(kanji.kanji, kanji);

        string savedKanji = PlayerPrefs.GetString(PLAYER_PREFS_STRING);

        if(savedKanji.Contains(kanji.kanji)) { return; }
        PlayerPrefs.SetString(PLAYER_PREFS_STRING, savedKanji += kanji.kanji);
        PlayerPrefs.Save();
    }

    public void RemoveFromStudyCards(string kanji)
    {
        if (!kanjiStudyCards.ContainsKey(kanji)) { return; }
        kanjiStudyCards.Remove(kanji);
        string savedKanji = PlayerPrefs.GetString (PLAYER_PREFS_STRING);

        if (savedKanji.Contains(kanji))
        {
            for(int i = 0; i <  savedKanji.Length; i++)
            {
                if (savedKanji[i].ToString() == kanji)
                {
                    savedKanji = savedKanji.Remove(i, 1);
                    break;
                }
            }
            PlayerPrefs.SetString(PLAYER_PREFS_STRING, savedKanji);
            PlayerPrefs.Save();
        }
    }

    public bool HasInStudyCards(string kanji)
    {
        return kanjiStudyCards.ContainsKey(kanji);
    }


    public void RequestSingleKanji(string character, UnityAction<Kanji> followUpEvent, UnityAction failedEvent)
    {
        if (kanjiDictionary.ContainsKey(character))
        {
            followUpEvent.Invoke(kanjiDictionary[character]);
        }
        else
        {
            StartCoroutine(FindKanjiInformation(character, followUpEvent, failedEvent));
        }   
    }

    private IEnumerator FindKanjiInformation(string kanjiCharacter, UnityAction<Kanji> followUpEvent, UnityAction failedEvent)
    {
        string kanjiGetUrl = kanjiAPIUrl + "kanji/" + kanjiCharacter;
        UnityWebRequest request = UnityWebRequest.Get(kanjiGetUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            Kanji newKanjiInformation = JsonConvert.DeserializeObject<Kanji>(responseText);
            kanjiDictionary.Add(kanjiCharacter, newKanjiInformation);
            followUpEvent.Invoke(newKanjiInformation);
        }
        else
        {
            string error = request.error;
            failedEvent.Invoke();
            Debug.Log(error);
            //Need to display that the Kanji could not be found
        }
    }
}

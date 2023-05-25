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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Populate the current dictionary
        //Check the users player prefs to see what they have currently saved locally on their device
        //Add it to the dictionary and active kanji dictionary
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
        kanjiStudyCards.Add(kanji.kanji, kanji);
        //Add this to the player preferences
    }

    public void RemoveFromStudyCards(string kanji)
    {
        kanjiStudyCards.Remove(kanji);
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

using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;

public class KanjiService : MonoBehaviour
{
    Dictionary<string, Kanji> kanjiDictionary = new Dictionary<string, Kanji>();
    Dictionary<string, Kanji> activeStudyDictionary = new Dictionary<string, Kanji>();

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


    public void RequestKanji(string character, UnityAction<Kanji> followUpEvent)
    {
        if (kanjiDictionary.ContainsKey(character))
        {
            followUpEvent.Invoke(kanjiDictionary[character]);
        }
        else
        {
            StartCoroutine(FindKanjiInformation(character, followUpEvent));
        }   
    }

    private IEnumerator FindKanjiInformation(string kanjiCharacter, UnityAction<Kanji> followUpEvent)
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
            Debug.Log(error);
            //Need to display that the Kanji could not be found
        }
    }
}

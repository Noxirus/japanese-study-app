using UnityEngine;

[System.Serializable]
public enum EWordType
{
    none = 0,
    noun = 10,
    verb = 20,
    adverb = 30,
    adjective = 40,
    number = 50,
}

[CreateAssetMenu(fileName = "Character", menuName = "Kanji")]
public class Kanji : ScriptableObject
{
    public string kanjiString;
    public string englishMeaning;
    public EWordType wordType;
    [Header("On'yomi Reading")]
    public string sinoHiragana;
    [Header("Kun'yomi Reading")]
    public string nativeHiragana;
}

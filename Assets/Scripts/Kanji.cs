using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Kanji")]
public class Kanji : ScriptableObject
{
    public Sprite kanjiImage;
    public string englishMeaning;
    [Header("On'yomi Reading")]
    public string sinoHiragana;
    [Header("Kun'yomi Reading")]
    public string nativeHiragana;
}

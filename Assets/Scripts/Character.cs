using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "HiraganaAndKatakana")]
public class Character : ScriptableObject
{
    public string romanji;
    [Header("Images")]
    public Sprite hiragana;
    public Sprite katakana;
    [Header("Dakuten")]
    public bool hasQuoteDakuten;
    public bool hasBubbleDakuten;
}

using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Characters")]
public class Character : ScriptableObject
{
    public string romanji;
    public Sprite hiragana;
    public Sprite katakana;
    public bool hasQuoteDakuten;
    public bool hasBubbleDakuten;
}

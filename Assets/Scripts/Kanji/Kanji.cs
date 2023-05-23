
using Newtonsoft.Json;
using System.Text;

[System.Serializable]
public class Kanji
{
    public string kanji;
    public int stroke_count;
    public string[] meanings;
    public string heisig_en;
    public string[] kun_readings;
    public string[] on_readings;
    public string[] name_readings;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int grade;
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int jlpt;
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string unicode;

    public Kanji()
    {
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < meanings.Length; i++)
        {
            sb.Append(meanings[i] + " ");
        }
        return kanji + " " + sb.ToString();
    }
}

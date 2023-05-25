using System.Text;

public static class HelperMethods
{
    public static string ReturnAppendedString(string[] strings, string delimiter = "\n")
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < strings.Length; i++)
        {
            sb.Append(strings[i]);
            if (i < strings.Length - 1)
            {
                sb.Append(delimiter);
            }
        }

        return sb.ToString();
    }
}

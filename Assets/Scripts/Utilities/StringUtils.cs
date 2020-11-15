using UnityEngine;
public static class StringUtils {
    static public string Ellipsis (string text, int maxLength, char replacement = '.') {
        string result = text.Trim ();
        if (result.Length > maxLength) {
            result = result.Substring (0, maxLength - 3).PadRight (maxLength, replacement);
        }

        return result;
    }

    static public string Slugify (string text) {
        return text.Trim ().ToLower ().Replace (' ', '-');
    }

    static public string SecondsToMinutes (int timer) {
        int minutes = (int) Mathf.Floor (timer / 60);
        int seconds = timer - minutes * 60;
        return minutes.ToString().PadLeft(2, '0') + "'" + seconds.ToString().PadLeft(2, '0') + "\"";
    }
}
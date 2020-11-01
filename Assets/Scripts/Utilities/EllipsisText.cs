class EllipsisText {
    static public string Make (string text, int maxLength, char replacement = '.') {
        string result = text.Trim();
        if (result.Length > maxLength) {
            result = result.Substring(0, maxLength - 3).PadRight(maxLength, replacement);
        }

        return result;
    }
}
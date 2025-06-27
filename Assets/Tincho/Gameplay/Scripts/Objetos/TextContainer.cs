using System.Collections.Generic;

public class TextContainer
{
    private static Dictionary<string, string> _hints = new Dictionary<string, string>()
    {
        { "MedicNoteText", "Situation Report – Preliminary Summary\n\nOutbreak confirmed. Water is compromised. Clear instructions: retrieve the generator key in the shed, activate generator, and escape. Without that… no way out. Trust no one.\n\n— Dr. Elias Kessler\nAdvanced Response Unit, M.E.D.C.O.R.E.\nDate: 08/14 — 03:27 AM"},
        { "PuzzleHint", "Restart Protocol:\n\nPort A – The typical hue of urine.\nPort B – The shade used in cold therapy.\nPort C – The color of herbal medicine.\nPort D – The trail of a ruptured artery.\n\nIf unsure, think like a doctor.\n\n— Dr. Elias Kessler\nAdvanced Response Unit, M.E.D.C.O.R.E.\nDate: 08/16 — 04:15 PM"},
    };

    private static string GetHint(string key)
    {
        return _hints.TryGetValue(key, out var value) ? value : "Texto no encontrado.";
    }

    public static string TextSwitch(int noteType)
    {
        string textValue = string.Empty;

        switch (noteType) 
        {
            case 0: textValue = GetHint("MedicNoteText");
                break;
            case 1: textValue = GetHint("PuzzleHint");
                break;
        }
        return textValue;
    }
}

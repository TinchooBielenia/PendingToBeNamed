using System.Collections.Generic;

public class TextContainer
{
    private static Dictionary<string, string> _hints = new Dictionary<string, string>()
    {
        { "MedicNoteText", "Informe de Situación – Resumen Preliminar\r\n\r\nBrote confirmado. El agua está comprometida. Instrucciones claras: activar generador, desbloquear acceso principal y recuperar la llave maestra. Sin eso… no hay salida. No confíes en nadie.\r\n\r\n— Dr. Elias Kessler\r\nUnidad de Respuesta Avanzada, M.E.D.C.O.R.E.\r\nFecha: 14/08 — 03:27 AM" },
        { "PuzzleHint", "Protocolo de Reinicio (Clave de Color):\r\n\r\nPuerto A – El tono del suero salino.\r\nPuerto B – El matiz de la crioterapia.\r\nPuerto C – El color de la cura vegetal.\r\nPuerto D – El rastro de una arteria rota.\r\n\r\nSi no estás seguro, piensa como médico.\r\n\r\n— Dr. Elias Kessler\r\nUnidad de Respuesta Avanzada, M.E.D.C.O.R.E.\r\nFecha: 16/08 — 04:15 PM" },
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

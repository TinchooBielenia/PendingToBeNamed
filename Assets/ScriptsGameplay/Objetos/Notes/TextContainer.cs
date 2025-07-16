using System.Collections.Generic;
using UnityEngine;

//TP2 - Bielenia Martin - Juliana Dimeglio
public class TextContainer : MonoBehaviour
{
    public static TextContainer Instance { get; private set; }

    [System.Serializable]
    public class NoteEntry
    {
        public int noteID;
        [TextArea(3, 10)]
        public string text;
    }

    [SerializeField]
    private List<NoteEntry> _notesByID = new();

    //Consigna: Diccionario
    private Dictionary<int, string> _noteDictionary = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (var note in _notesByID)
        {
            if (!_noteDictionary.ContainsKey(note.noteID))
            {
                _noteDictionary.Add(note.noteID, note.text);
            }
            else
            {
                Debug.LogWarning($"Nota con ID duplicado: {note.noteID}");
            }
        }
    }

    public static string GetTextByID(int noteID)
    {
        if (Instance == null)
        {
            Debug.LogWarning("No hay instancia de TextContainer en la escena.");
            return "Texto no encontrado.";
        }

        if (!Instance._noteDictionary.TryGetValue(noteID, out var text))
        {
            return "Nota no encontrada.";
        }

        return text;
    }
}

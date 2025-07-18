using System;
using System.Collections.Generic;
using UnityEngine;

//TP2 - Martin Bielenia - Juliana Dimeglio
public class NotesTextContainer : MonoBehaviour
{
    public static NotesTextContainer Instance { get; private set; }

    [System.Serializable]
    public class NoteEntry
    {
        public int noteID;
        public NoteType textType;
        [TextArea(3, 10)]
        public string text;
    }

    public enum NoteType
    {
        Note,
        Hint,
    }

    [SerializeField]
    private List<NoteEntry> _notesByID = new();
    [SerializeField]
    private List<NoteEntry> _hintsByID = new();

    //Consigna: Diccionario
    private Dictionary<(int, NoteType), string> _noteDictionary = new();

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
            var key = (note.noteID, note.textType);
            if (!_noteDictionary.ContainsKey(key))
            {
                _noteDictionary.Add(key, note.text);
            }
            else
            {
                Debug.LogWarning($"Nota duplicada con ID: {note.noteID} y tipo: {note.textType}");
            }
        }

        foreach (var note in _hintsByID)
        {
            var key = (note.noteID, note.textType);
            if (!_noteDictionary.ContainsKey(key))
            {
                _noteDictionary.Add(key, note.text);
            }
            else
            {
                Debug.LogWarning($"Nota duplicada con ID: {note.noteID} y tipo: {note.textType}");
            }
        }
    }

    public static string GetTextByID(int noteID, NoteType textType)
    {
        if (Instance == null)
        {
            Debug.LogWarning("No hay instancia de NotesTextContainer en la escena.");
            return "Texto no encontrado.";
        }

        var key = (noteID, textType);
        if (!Instance._noteDictionary.TryGetValue(key, out var text))
        {
            return $"Texto no encontrado para ID: {noteID} y tipo: {textType}.";
        }

        return text;
    }
}

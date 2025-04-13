using System;
using Manager;
using TMPro;
using UI.Scenes;
using UnityEngine;

namespace General
{
    public class Notebook : MonoBehaviour
    {
        [SerializeField] private TMP_InputField note;

        private void OnEnable()
        {
            note.text = PlayerPrefs.GetString("notes");
        }

        public void Save()
        {
            PlayerPrefs.SetString("notes", note.text);
        }

        public void Back()
        {
            TransitionManager.Instance.CloseWindow(UIObjects.Instance.NotebookWindow);
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Scenarios
{
    public class Scenario : MonoBehaviour
    {
        public Button NextWindow;
        public Window Window;
        public string[] Texts;
        public GameObject Canvas;
        
        private int _current = -1;

        public event Action Completed;

        private void Awake()
        {
            _current = -1;
            OnNextWindow();
        }

        private void OnEnable() =>
            NextWindow.onClick.AddListener(OnNextWindow);

        private void OnDisable() => 
            NextWindow.onClick.RemoveListener(OnNextWindow);

        private void OnNextWindow()
        {
            if (_current >= Texts.Length - 1)
            {
                Window.Hide();
                Canvas.gameObject.SetActive(false);
                Completed?.Invoke();
                return;
            }
            
            _current++;
            Window.Show("Оповещение", Texts[_current]);
        }
    }
}
using System;
using CodeBase.Buildings;
using CodeBase.CameraLogic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Scenarios
{
    public class EndGame : MonoBehaviour
    {
        public MainCapsule Main;
        public int OxygenTarget = 60;
        public Window Window;
        public Button NextWindow;
        public Window LastWindow;
        public Canvas Canvas;
        public CameraMovement CameraMovement;
        public CameraScrollMouse ScrollMouse;
        public PlayTime Timer;

        public string[] Texts;
        public string FinalText;

        private int _current = -1;

        [SerializeField]
        private bool _isDone;
        
        private void OnEnable()
        {
            Main.OxygenChanged += OnOxygenChanged;
            NextWindow.onClick.AddListener(OnNextWindow);
        }

        public void Update()
        {
            if (_isDone)
            {
                ShowEndGameWindow();
                _isDone = false;
            }
        }

        private void OnDisable()
        {
            Main.OxygenChanged -= OnOxygenChanged;
            NextWindow.onClick.AddListener(OnNextWindow);
        }

        private void OnOxygenChanged(int oxygen)
        {
            if (Main.OxygenStored >= OxygenTarget) 
                ShowEndGameWindow();
        }

        private void ShowEndGameWindow()
        {
            Timer.StopTimer();
            CameraMovement.enabled = false;
            ScrollMouse.enabled = false;
            Canvas.gameObject.SetActive(true);
            _current = -1;
            OnNextWindow();
        }

        private void OnNextWindow()
        {
            if (_current >= Texts.Length - 1)
            {
                Window.Hide();
                LastWindow.Show("Оповещение", FinalText);
                return;
            }
            
            _current++;
            
            if (_current == 1) 
                Texts[_current] = string.Format(Texts[_current], $"{Timer.AsText()}");
            
            Window.Show("Оповещение", Texts[_current]);
        }
    }
}
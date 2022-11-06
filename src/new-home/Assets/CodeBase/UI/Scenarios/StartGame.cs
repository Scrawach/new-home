using System;
using CodeBase.CameraLogic;
using UnityEngine;

namespace CodeBase.UI.Scenarios
{
    public class StartGame : MonoBehaviour
    {
        public Scenario Scenario;
        public PlayTime Timer;
        public CameraMovement CameraMovement;
        public CameraScrollMouse ScrollMouse;

        private void Awake()
        {
            CameraMovement.enabled = false;
            ScrollMouse.enabled = false;
        }

        private void OnEnable() => 
            Scenario.Completed += OnCompleted;

        private void OnDisable() => 
            Scenario.Completed -= OnCompleted;

        private void OnCompleted()
        {
            Timer.StartTimer();
            CameraMovement.enabled = true;
            ScrollMouse.enabled = true;
        }
    }
}
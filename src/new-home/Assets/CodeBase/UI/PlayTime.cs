using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class PlayTime : MonoBehaviour
    {
        public int ElapsedSeconds = 0;
        public int ElapsedMinutes = 0;

        public TextMeshProUGUI Label;
        
        private float _inSecondTime = 1f;
        private float _elapsedTime;

        private bool _isWorking;
        
        public void StartTimer()
        {
            ElapsedSeconds = 0;
            _isWorking = true;
        }

        public void StopTimer()
        {
            _isWorking = false;
        }

        private void FixedUpdate()
        {
            if (!_isWorking)
                return;
            
            _elapsedTime += Time.fixedDeltaTime;
            if (_elapsedTime >= _inSecondTime)
            {
                ElapsedSeconds++;

                if (ElapsedSeconds >= 60)
                {
                    ElapsedSeconds = 0;
                    ElapsedMinutes++;
                }
                
                _elapsedTime -= _inSecondTime;
                UpdateTimer();
            }
        }

        private void UpdateTimer() => 
            Label.text = AsText();

        public string AsText()
        {
            var seconds = ElapsedSeconds.ToString();
            var minutes = ElapsedMinutes.ToString();
            if (ElapsedSeconds < 10)
                seconds = $"0{ElapsedSeconds}";
            if (ElapsedMinutes < 10)
                minutes = $"0{ElapsedMinutes}";
            return $"{minutes}:{seconds}";
        }
    }
}
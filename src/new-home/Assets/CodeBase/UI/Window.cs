using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class Window : MonoBehaviour
    {
        public TextMeshProUGUI Title;
        public TextMeshProUGUI Description;

        public float OpeningTime = 0.25f;
        public float ClosingTime = 0.25f;

        public AudioSource SFX;

        public void Show(string tittle, string description)
        {
            Title.text = tittle;
            Description.text = description;
            transform.localScale = Vector3.zero;
            transform.gameObject.SetActive(true);
            transform.DOKill();
            transform.DOScale(1f, OpeningTime).SetEase(Ease.OutBack);
            
            if (SFX != null)
                SFX.Play();
        }

        public void Hide()
        {
            transform.DOKill();
            transform.DOScale(0f, ClosingTime).SetEase(Ease.InBack);
        }
    }
}
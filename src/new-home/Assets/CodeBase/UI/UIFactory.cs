using System;
using CodeBase.Actors;
using CodeBase.Buildings;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI
{
    public class UIFactory : MonoBehaviour
    {
        public PanelUI WorkerPanel;
        public PanelUI MainPanel;
        public PanelUI DescriptionPanel;

        private PanelUI _last;

        public void TryShowPanel(ISelectable selectable)
        {
            if (selectable is MainCapsule capsule)
                ShowMainPanel();
        }

        public void ShowWorkerPanel() => 
            ShowPanel(WorkerPanel);

        public void HideWorkerPanel() => 
            HidePanel(WorkerPanel);

        public void ShowMainPanel() => 
            ShowPanel(MainPanel);

        public void HideMainPanel() => 
            HidePanel(MainPanel);

        public void ShowDescription() => 
            ShowPanel(DescriptionPanel);

        public void HideDescription() =>
            HidePanel(DescriptionPanel);

        private void ShowPanel(PanelUI panel)
        {
            HidePrevious();
            _last = panel;
            panel.transform.SetAsLastSibling();
            panel.transform.DOKill();
            panel.gameObject.SetActive(true);
            panel.transform.DOMoveY(0, 1f).SetEase(Ease.OutBack);
            panel.Activate();
        }

        private static void HidePanel(PanelUI panel)
        {
            panel.Deactivate();
            panel.transform.DOKill();
            panel.transform.DOMoveY(-200f, 1f).SetEase(Ease.InBack)
                .OnComplete(() => panel.gameObject.SetActive(false));
        }

        public void HidePrevious()
        {
            if (_last != null)
                HidePanel(_last);
            _last = null;
        }
    }
}
using System.Collections.Generic;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Actors
{
    public class ActorSelection : MonoBehaviour
    {
        public PlayerInput PlayerInput;
        public UIFactory UIFactory;
        
        public List<Actor> SelectedActors = new();

        private ISelectable _currentSelection;
        
        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            
            if (!PlayerInput.IsSelectButtonClicked()) 
                return;
            
            if (PlayerInput.TryGetFromMousePosition<Actor>(out var actor))
            {
                if (PlayerInput.IsMultiplyCommand())
                {
                    Select(actor);
                }
                else
                {
                    DeselectAll();
                    Select(actor);
                }
            }
            else if (PlayerInput.TryGetFromMousePosition<ISelectable>(out var select))
            {
                DeselectAll();
                _currentSelection = select;
                select.Select();
                UIFactory.TryShowPanel(select);
            }
        }

        public void Select(Actor actor)
        {
            SelectedActors.Add(actor);
            actor.Select();
            UIFactory.ShowWorkerPanel();
        }

        public void DeselectAll()
        {
            _currentSelection?.Deselect();
            _currentSelection = null;
            UIFactory.HidePrevious();
            
            foreach (var actor in SelectedActors) 
                actor.Deselect();
            SelectedActors.Clear();
            UIFactory.HideWorkerPanel();
        }
    }
}
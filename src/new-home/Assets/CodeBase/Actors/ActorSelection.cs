using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Actors
{
    public class ActorSelection : MonoBehaviour
    {
        public PlayerInput PlayerInput;
        
        public List<Actor> SelectedActors = new();
        
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
        }

        public void Select(Actor actor)
        {
            SelectedActors.Add(actor);
            actor.Select();
        }

        public void DeselectAll()
        {
            foreach (var actor in SelectedActors) 
                actor.Deselect();
            SelectedActors.Clear();
        }
    }
}
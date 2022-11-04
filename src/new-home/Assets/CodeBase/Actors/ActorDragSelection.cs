using System;
using UnityEngine;

namespace CodeBase.Actors
{
    public class ActorDragSelection : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform _selectionBox;

        [SerializeField] private ActorSelection _selection;
        [SerializeField] private ActorFactory _factory;

        private Camera _camera;
        
        private Vector3 _start;
        private bool _isDragging;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _start = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                var distance = (Input.mousePosition - _start).sqrMagnitude;
                if (distance > 50)
                {
                    _isDragging = true;
                    UpdateSelectionBox(_start, Input.mousePosition);
                }
                else
                {
                    _isDragging = false;
                    UpdateSelectionBox(Vector2.zero, Vector2.zero);
                }
            }

            if (Input.GetMouseButtonUp(0) && _isDragging)
            {
                SelectUnits();
                UpdateSelectionBox(Vector2.zero, Vector2.zero);
                _isDragging = false;
            }
        }
        
        private void UpdateSelectionBox(Vector2 startPosition, Vector2 currentPosition)
        {
            var centerPoint = (startPosition + currentPosition) / 2;
            var width = Mathf.Abs(currentPosition.x - startPosition.x);
            var height = Mathf.Abs(currentPosition.y - startPosition.y);
            _selectionBox.sizeDelta = new Vector2(width, height);
            _selectionBox.anchoredPosition = centerPoint;
        }

        private void SelectUnits()
        {
            var halfDelta = _selectionBox.sizeDelta / 2;
            var anchoredPosition = _selectionBox.anchoredPosition;
            var min = anchoredPosition - halfDelta;
            var max = anchoredPosition + halfDelta;
            
            _selection.DeselectAll();
            
            foreach (var actor in _factory.Actors)
            {
                var screenPosition = _camera.WorldToScreenPoint(actor.transform.position);
                if (screenPosition.x > min.x && screenPosition.x < max.x && screenPosition.y > min.y &&
                    screenPosition.y < max.y)
                {
                    _selection.Select(actor);
                }
            }
        }
    }
}
using Hexagon.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexagon.Controllers
{
    public class InputController : MonoBehaviour
    {
        private RaycastHit2D _raycastHit;

        private IInteractable _interactable;

        private bool _isInteractable => _raycastHit.transform.TryGetComponent<IInteractable>(out _interactable);

        private void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Ray rayPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            _raycastHit = Physics2D.GetRayIntersection(rayPoint);
            if (_raycastHit && _isInteractable)
            {
                _interactable.ClickInteract(_raycastHit.point);
            }
        }
    }
}

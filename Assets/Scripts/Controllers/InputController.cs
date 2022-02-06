using Hexagon.Interfaces;
using Hexagon.Tile.Swap;
using UnityEngine;

namespace Hexagon.Controllers
{
    public class InputController : MonoBehaviour
    {
        private RaycastHit2D _raycastHit;

        private IInteractable _interactable;

        private Vector3 _firstInputPosition;

        private float _swipeMoveDistanceFactor = 5f;

        private bool _isInteractable => _raycastHit.transform.TryGetComponent<IInteractable>(out _interactable);
        private bool _didPlayerSwipeClockwise => Input.mousePosition.y > _firstInputPosition.y ? true : false;
        private bool _inputReleased => Input.GetMouseButtonUp(0);

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _firstInputPosition = Input.mousePosition;
            }

            CheckInputForSelecting();
            CheckInputForSwiping();
        }

        private void CheckInputForSelecting()
        {
            if (!_inputReleased) return;
            if (DidPlayerSwipe()) return;
            if (TileSwapController.IsSwapping) return;

            Ray rayPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            _raycastHit = Physics2D.GetRayIntersection(rayPoint);
            if (_raycastHit && _isInteractable)
            {
                _interactable.ClickInteract(_raycastHit.point);
            }
        }

        private void CheckInputForSwiping()
        {
            if (!_inputReleased) return;
            if (!DidPlayerSwipe()) return;

            if (_didPlayerSwipeClockwise)
            {
                StartCoroutine(TileSwapController.SwapClockwiseCoroutine());
            }
            else
            {
                StartCoroutine(TileSwapController.SwapCounterClockwiseCoroutine());
            }

            _firstInputPosition = Vector3.zero;
        }

        private bool DidPlayerSwipe()
        {
            float inputDistance = Vector3.Distance(_firstInputPosition, Input.mousePosition);
            bool result = inputDistance > _swipeMoveDistanceFactor ? true : false;
            return result;
        }
    }
}

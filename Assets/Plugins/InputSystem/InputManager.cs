using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace ViJ
{
    public class InteractionObject : MonoBehaviour
    {

    }

    public class InputManager : MonoBehaviour
    {
        private Actions mActions;

        private bool mInputStarted;

        private List<InteractionObject> mCapturedObjects = new List<InteractionObject>();
        private List<Camera> mCameras = new List<Camera>();

        public void RegisterCamera(Camera cam)
        {
            if (!mCameras.Contains(cam))
                mCameras.Add(cam);
        }

        private void Awake()
        {
            mActions = new Actions();
            mActions.InputActions.Enable();

            mActions.InputActions.PointerDownUp.performed += OnPointerPerformed;
            mActions.InputActions.PointerDownUp.canceled += OnPointerCanceled;

            mActions.InputActions.PointerMove.performed += OnPointerMove;
        }

        private void OnPointerPerformed(InputAction.CallbackContext context)
        {
            mInputStarted = true;
            Debug.Log("PointerDown");
        }

        private void OnPointerCanceled(InputAction.CallbackContext context)
        {
            mInputStarted = false;
            Debug.Log("PointerUp");
        }

        private Vector2 mPointerCoord;

        private void OnPointerMove(InputAction.CallbackContext context)
        {
            if (mInputStarted)
            {
                mPointerCoord = context.ReadValue<Vector2>();
                Debug.Log($"PointerDrag {mPointerCoord}");
            }
        }

        private List<InteractionObject> Trace3dObjects(Vector2 coord)
        {
            foreach(var camera in mCameras)
            {
                camera.ScreenPointToRay(coord);
            }
            return new List<InteractionObject>();
        }

        private bool IsOverUI(Vector2 pos)
        {
            var eventData = new PointerEventData(EventSystem.current) { position = pos };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    }
}
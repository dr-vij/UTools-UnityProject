using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace ViJ
{
    /// <summary>
    /// Interaction object. it is used for all interaction subscriptions
    /// </summary>
    public class InteractionObject : MonoBehaviour
    {

    }

    /// <summary>
    /// This object is used to remove object from interaction hierarchy
    /// </summary>
    public class InteractionObjectIgnorer : MonoBehaviour
    {

    }

    public class InputDataContainer
    {
        private bool mInputStarted = false;

        public bool InputStarted => mInputStarted;

        public Vector2 PointerPosition = Vector2.zero;
        public Vector2 PointerDelta = Vector2.zero;

        public void StartInput()
        {
            if (!mInputStarted)
            {
                ResetInputData();
                mInputStarted = true;
            }
        }

        public void StopInput()
        {
            if (mInputStarted)
            {
                ResetInputData();
                mInputStarted = false;
            }
        }

        private void ResetInputData()
        {
            PointerPosition = Vector3.zero;
            PointerDelta = Vector3.zero;
        }
    }

    public class InputManager : MonoBehaviour
    {
        [SerializeField] private int mDragTriggerDistance = 0;

        private InputDataContainer mInputData = new InputDataContainer();

        private Actions mActions;
        private List<InteractionObject> mCapturedObjects = new List<InteractionObject>();
        private List<Camera> mCameras = new List<Camera>();

        public void RegisterCamera(Camera cam)
        {
            if (!mCameras.Contains(cam))
                mCameras.Add(cam);
        }

        private void OnEnable()
        {
            mActions = new Actions();
            mActions.GestureActions.Enable();

            mActions.GestureActions.PointerStart.performed += OnPointerPerformed;
            mActions.GestureActions.PointerStart.canceled += OnPointerCanceled;

            mActions.GestureActions.PointerMove.performed += OnPointerMove;
        }

        private void OnDisable()
        {
            mActions.GestureActions.PointerStart.performed -= OnPointerPerformed;
            mActions.GestureActions.PointerStart.canceled -= OnPointerCanceled;

            mActions.GestureActions.PointerMove.performed -= OnPointerMove;

            mActions.Dispose();
        }

        /// <summary>
        /// Called when pointer down
        /// </summary>
        /// <param name="context"></param>
        private void OnPointerPerformed(InputAction.CallbackContext context)
        {
            mInputData.StartInput();
        }

        /// <summary>
        /// Called when pointer up
        /// </summary>
        /// <param name="context"></param>
        private void OnPointerCanceled(InputAction.CallbackContext context)
        {
            mInputData.StopInput();
        }

        /// <summary>
        /// Called when pointer have changed its position
        /// </summary>
        /// <param name="context"></param>
        private void OnPointerMove(InputAction.CallbackContext context)
        {
            if (mInputData.InputStarted)
            {
                mInputData.
            }
        }

        private List<InteractionObject> Trace3dObjects(Vector2 coord)
        {
            foreach (var camera in mCameras)
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

        private void Update()
        {

        }
    }
}
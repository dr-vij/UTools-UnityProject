using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace ViJApps
{
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// This parameters decides how far should drag perform to call Drag. If distance is less, press will be performed
        /// </summary>
        [SerializeField] private int mDragOrPressTriggerDistance = 0;
        [SerializeField] private bool mRaiseDragOnUpdates = true;

        private InputDataContainer mInputData = new InputDataContainer();

        private Actions mActions;
        private List<Camera> mCameras = new List<Camera>();

        public void RegisterCamera(Camera cam)
        {
            if (!mCameras.Contains(cam))
                mCameras.Add(cam);

            //We sort cameras by depth for now.
            mCameras.Sort((cam1, cam2) => cam1.depth.CompareTo(cam2.depth));
        }

        /// <summary>
        /// Create Actions and subscribe it's events
        /// </summary>
        private void OnEnable()
        {
            mActions = new Actions();
            mActions.GestureActions.Enable();

            mActions.GestureActions.PointerStart.performed += OnPointerPerformed;
            mActions.GestureActions.PointerStart.canceled += OnPointerCanceled;

            mActions.GestureActions.PointerMove.performed += OnPointerMove;
        }

        /// <summary>
        /// Unsubscribe from all events and dispose Actions
        /// </summary>
        private void OnDisable()
        {
            mActions.GestureActions.PointerStart.performed -= OnPointerPerformed;
            mActions.GestureActions.PointerStart.canceled -= OnPointerCanceled;

            mActions.GestureActions.PointerMove.performed -= OnPointerMove;

            mActions.Dispose();
        }

        /// <summary>
        /// Called when pointer is down
        /// </summary>
        /// <param name="context"></param>
        private void OnPointerPerformed(InputAction.CallbackContext context)
        {
            mInputData.StartInput();
            mInputData.PointerDownPosition = context.ReadValue<Vector2>();
            mInputData.PointerCurrentPosition = context.ReadValue<Vector2>();

            //TODO: PointerDown here and capture dragged object
        }

        /// <summary>
        /// Called when pointer is up
        /// </summary>
        /// <param name="context"></param>
        private void OnPointerCanceled(InputAction.CallbackContext context)
        {

            if (!mInputData.IsDragTriggered)
            {
                //TODO: Press here
                Debug.Log("Press");
            }
            else
            {
                //TODO: Drag end here
                Debug.Log("Drag end");
            }

            //TODO: Pointer up here
            mInputData.StopInput();
        }

        /// <summary>
        /// Called when pointer have changed its position
        /// </summary>
        /// <param name="context"></param>
        private void OnPointerMove(InputAction.CallbackContext context)
        {
            var currentPosition = context.ReadValue<Vector2>();

            if (mInputData.IsPointerDownTriggered)
            {
                var lastPosition = mInputData.PointerCurrentPosition;
                mInputData.PointerCurrentPosition = currentPosition;
                mInputData.PointerIterationDelta = currentPosition - lastPosition;

                //TODO: Pointer Move can be here ???

                //Calculate distance from pointer down and perform drag
                var totalDelta = mInputData.PointerCurrentPosition - mInputData.PointerDownPosition;
                if (!mInputData.IsDragTriggered && totalDelta.magnitude > mDragOrPressTriggerDistance)
                {
                    mInputData.TriggerDrag();
                    //TODO: Drag start here

                    Debug.Log("Drag start");
                }
                else if (mInputData.IsDragTriggered)
                {
                    //TODO: Drag here
                    Debug.Log("Drag");
                }
            }

            //TODO: Pointer Move can be here ???
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
            if (mRaiseDragOnUpdates && mInputData.IsPointerDownTriggered && mInputData.IsDragTriggered)
            {
                //TODO: Drag here
            }
        }
    }
}
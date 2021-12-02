using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ViJApps
{
    public class CameraTracer
    {
        public readonly static IComparer<RaycastHit> HitDistanceComparer = new FuncComparer<RaycastHit>((hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

        //SETTINGS
        private int mRaycastCapacity;
        private int mRaycastLayerMask;

        //BUFFER DATA
        private RaycastHit[] mHits;
        private int mCurrentHitCount;

        public int RaycastCapacity
        {
            get => mRaycastCapacity;
            set
            {
                mRaycastCapacity = value;
                RefreshCapacityArray();
            }
        }

        public CameraTracer(int raycastCapacity)
        {
            RaycastCapacity = raycastCapacity;
        }

        private void RefreshCapacityArray()
        {
            Array.Resize(ref mHits, mRaycastCapacity);
        }

        public void TraceCamera(Vector2 position, Camera cam)
        {
            var ray = cam.ScreenPointToRay(position, Camera.MonoOrStereoscopicEye.Mono);
            var raycastLimitPlane = new Plane(-cam.transform.forward, cam.transform.position + cam.transform.forward * cam.farClipPlane);
            raycastLimitPlane.Raycast(ray, out var rayDistance);
            mCurrentHitCount = Physics.RaycastNonAlloc(ray, mHits, rayDistance, mRaycastLayerMask);
            Array.Sort(mHits, 0, mCurrentHitCount, HitDistanceComparer);
        }

        public bool IsOverUI(Vector2 pos)
        {
            var eventData = new PointerEventData(EventSystem.current) { position = pos };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    }
}
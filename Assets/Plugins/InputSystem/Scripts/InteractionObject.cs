using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ViJApps
{
    /// <summary>
    /// Interaction object. it is used for all interaction subscriptions
    /// </summary>
    public class InteractionObject : MonoBehaviour
    {
        /// <summary>
        /// Friends are used to get Input Actions like it is the same object
        /// </summary>
        public List<InteractionObject> Friends = new List<InteractionObject>();

        private void Awake()
        {
            var interactionObjects = GetComponentsInChildren<InteractionObject>();

            //We check here that only one InteractionObject existst on this gameobject
            if (interactionObjects.Where(c => c.transform == transform).Count() != 1)
            {
                Debug.LogError($"Several InteractionObject scripts found at one gameobject {gameObject.name}", gameObject);
            }
        }
    }
}

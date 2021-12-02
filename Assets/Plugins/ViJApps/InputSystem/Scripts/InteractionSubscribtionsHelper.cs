using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViJApps
{
    public static class InteractionSubscribtionsHelper
    {
        /// <summary>
        /// Unified pointer press subscription
        /// </summary>
        /// <param name="interactionObject"></param>
        /// <param name="handler"></param>
        public static void SubscribePressEvent(this InteractionObject interactionObject, EventHandler<PointerInteractionEventArgs> handler)
        {
            interactionObject.Subscribe(InteractionEvents.InteractionPressEvent, handler);
        }

        /// <summary>
        /// Unified pointer drag subscription
        /// </summary>
        /// <param name="interactionObject"></param>
        /// <param name="pointerDragStartHandler"></param>
        /// <param name="pointerDragHandler"></param>
        /// <param name="pointerDragEndHandler"></param>
        public static void SubscribePointerDragEvent(this InteractionObject interactionObject,
            EventHandler<PointerDragInteractionEventArgs> pointerDragStartHandler,
            EventHandler<PointerDragInteractionEventArgs> pointerDragHandler,
            EventHandler<PointerDragInteractionEventArgs> pointerDragEndHandler)
        {
            interactionObject.Subscribe(InteractionEvents.InteractionDragStartEvent, pointerDragStartHandler);
            interactionObject.Subscribe(InteractionEvents.InteractionDragEvent, pointerDragHandler);
            interactionObject.Subscribe(InteractionEvents.InteractionDragEndEvent, pointerDragEndHandler);
        }

        /// <summary>
        /// Unified pointer move subscription
        /// </summary>
        /// <param name="interactionObject"></param>
        /// <param name="pointerMoveHandler"></param>
        public static void SubscribePointerMoveEvent(this InteractionObject interactionObject, EventHandler<PointerInteractionEventArgs> pointerMoveHandler)
        {
            interactionObject.Subscribe(InteractionEvents.InteractionPointerMoveEvent, pointerMoveHandler);
        }
    }
}
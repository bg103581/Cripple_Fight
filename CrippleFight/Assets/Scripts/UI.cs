using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Use this for initialization

public class UI : Selectable
    {
        //Use this to check what Events are happening
        BaseEventData m_BaseEvent;

        void Update()
        {
            //Check if the GameObject is being highlighted
            if (IsHighlighted(m_BaseEvent) == true)
            {
                //Output that the GameObject was highlighted, or do something else
                Debug.Log("Selectable is Highlighted");
            }
        }
    }


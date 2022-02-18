using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chess
{
    public class InputManager : EventSystem
    {
        EventSystem m_EventSystem;

        void Start()
        {
            //Fetch the Event System from the Scene
            m_EventSystem = GetComponent<EventSystem>();
        }

        void Update()
        {
            //Check if the left Mouse button is clicked
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //m_EventSystem.
            }
        }
    }
}

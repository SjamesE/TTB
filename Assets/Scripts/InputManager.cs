using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chess
{
    public class InputManager : MonoBehaviour
    {
        public bool isDragging = false;
        public GameObject objectDragged = null;
        public Vector3 originalTransform;

        private Vector3 lastMousePos;

        void Update()
        {
            if (isDragging) UpdateDrag();

            //Check if the left Mouse button is clicked
            if (Input.GetMouseButtonDown(0))
            {
                List<RaycastResult> results = RaycastMouse();

                foreach (var obj in results)
                {
                    if (obj.gameObject.CompareTag("Piece"))
                    {
                        isDragging = true;
                        objectDragged = obj.gameObject;
                        lastMousePos = Input.mousePosition;
                        originalTransform = obj.gameObject.GetComponent<RectTransform>().position;
                    }
                }
                return;
            }

            // Update lastMousePos if the left click is still clicked
            if (Input.GetMouseButton(0))
            {
                lastMousePos = Input.mousePosition;
                return;
            }

            // Check if Left click was released
            if (Input.GetMouseButtonUp(0))
            {
                objectDragged.gameObject.GetComponent<RectTransform>().position = originalTransform;
                isDragging = false;
                objectDragged = null;
                originalTransform = new Vector3();
            }
        }

        private void UpdateDrag()
        {
            Vector3 offset = Input.mousePosition - lastMousePos;
            objectDragged.GetComponent<RectTransform>().localPosition += offset;
        }

        public List<RaycastResult> RaycastMouse()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                pointerId = -1,
            };

            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            Debug.Log(results.Count);

            return results;
        }

    }
}

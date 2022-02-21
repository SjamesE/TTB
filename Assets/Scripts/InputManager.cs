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
            // Update object being dragged
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
                        obj.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    }
                }
                return;
            }

            if (!isDragging) return;

            // Update lastMousePos if the left click is still clicked
            if (Input.GetMouseButton(0))
            {
                lastMousePos = Input.mousePosition;
                return;
            }

            // Check if Left click was released
            if (Input.GetMouseButtonUp(0))
            {
                GameObject gameObj = objectDragged.gameObject;
                RectTransform rt = objectDragged.GetComponent<RectTransform>();
                Vector2 tableSize = GameObject.FindGameObjectWithTag("Table").transform.position;
                if (rt.position.x < 0 || rt.position.y < 0 || rt.position.x > tableSize.x || rt.position.y > tableSize.y)
                {
                    objectDragged.gameObject.GetComponent<RectTransform>().position = originalTransform;
                }
                else
                {
                    Table table = GameObject.FindGameObjectWithTag("PieceManager").GetComponent<PieceManager>().table;
                    GetSquareIndex(rt.position);
                }
                gameObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                isDragging = false;
                objectDragged = null;
                originalTransform = new Vector3();
            }
        }

        private Vector2Int GetSquareIndex(Vector2 position)
        {
            Vector2 tableSize = GameObject.FindGameObjectWithTag("Table").transform.position;
            float x = tableSize.x % (tableSize.x / 8);
            float y = tableSize.y % (tableSize.y / 8);
            return new Vector2Int(Mathf.FloorToInt(x), Mathf.FloorToInt(y));
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

            return results;
        }

    }
}

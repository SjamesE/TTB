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
                        originalTransform = obj.gameObject.GetComponent<RectTransform>().localPosition;
                        obj.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    }
                }
                return;
            }

            if (!isDragging) return;

            // Update lastMousePos
            lastMousePos = Input.mousePosition;

            // Check if Left click was released
            if (Input.GetMouseButtonUp(0))
            {
                GameObject gameObj = objectDragged.gameObject;
                RectTransform rt = objectDragged.GetComponent<RectTransform>();
                Vector2 pos = rt.localPosition;
                Vector2 tableSize = GameObject.FindGameObjectWithTag("Table").GetComponent<RectTransform>().sizeDelta;
                if (pos.x > 0 && pos.y > 0 && pos.x < tableSize.x && pos.y < tableSize.y)
                {
                    Debug.Log("Inside");
                    Table table = GameObject.FindGameObjectWithTag("PieceManager").GetComponent<PieceManager>().table;

                    Vector2Int newSquare = GetSquareIndex(pos);
                    PieceManager pm = GameObject.FindGameObjectWithTag("PieceManager").GetComponent<PieceManager>();
                    pm.table.MovePiece(GetSquareIndex(originalTransform), newSquare);
                    rt.localPosition = new Vector3(newSquare.x * 120 + 60, newSquare.y * 120 + 60);
                }
                else
                {
                    rt.localPosition = originalTransform;
                }
                gameObj.GetComponent<SpriteRenderer>().sortingOrder = 1;

                isDragging = false;
                objectDragged = null;
                originalTransform = new Vector3();
            }
        }

        private Vector2Int GetSquareIndex(Vector2 position)
        {
            Vector2 tableSize = GameObject.FindGameObjectWithTag("Table").GetComponent<RectTransform>().sizeDelta;
            int x = Mathf.FloorToInt(position.x / (tableSize.x / 8f));
            int y = Mathf.FloorToInt(position.y / (tableSize.y / 8f));
            //Debug.Log($"X: {x} = {position.x} / 120, Y: {y} = {position.y} / 120 ");
            return new Vector2Int(x, y);
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

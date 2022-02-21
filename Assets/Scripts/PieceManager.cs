using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chess
{
    public class PieceManager : MonoBehaviour
    {
        public GameObject parent;

        public Table table { get; private set; }
        public Object prefab;

        // Start is called before the first frame update
        void Start()
        {
            table = new Table();
            CreatePieces();
        }

        /// <summary>
        /// Generate the chess Pieces
        /// </summary>
        private void CreatePieces()
        {
            int index = 0;
            foreach (var square in table.squares)
            {
                // Skip square if empty
                if (square.Piece == Piece.none) continue;

                // Create Piece GameObject
                GameObject gameObject = Instantiate(prefab, parent.transform) as GameObject;

                // Rename it and give it a tag
                gameObject.name = "Piece" + index;
                gameObject.tag = "Piece";

                // Set the image's sprite
                Sprite sprite = table.textures[(int)square.Piece];
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

                // Set it's position and size
                Vector3 newPos = new Vector3(square.Position.x * 120 + 60, square.Position.y * 120 + 60);
                gameObject.GetComponent<RectTransform>().localPosition = newPos;

                Vector2 parentSize = GetComponentInParent<RectTransform>().sizeDelta;
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(parentSize.x / 8, parentSize.x / 8);

                // Image alpha set to 0
                Color color = gameObject.GetComponent<Image>().color;
                color.a = 0f;
                gameObject.GetComponent<Image>().color = color;

                // Increase the index
                index++;
            }
        }
    }
}

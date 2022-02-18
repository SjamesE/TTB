using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chess
{
    public class PieceManager : MonoBehaviour
    {
        public GameObject parent;
        public GameObject origin;
        [Range(1f, 2f)]
        public float pieceSize = 1;

        Table table;
        public Object prefab;

        // Start is called before the first frame update
        void Start()
        {
            table = new Table();
            CreatePieces();
        }

        // Update is called once per frame
        void Update()
        {

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
                gameObject.GetComponent<Image>().sprite = sprite;

                // Set it's position and size
                Vector3 newPos = new Vector3(square.Position.x * 120 + 60, square.Position.y * 120 + 60);
                gameObject.GetComponent<RectTransform>().localPosition = newPos;
                Vector2 newSize = sprite.textureRect.size * pieceSize;
                gameObject.GetComponent<RectTransform>().sizeDelta = newSize;

                Vector2 parentSize = GetComponentInParent<RectTransform>().sizeDelta;
                Vector2 padding = -new Vector2(Mathf.Abs((parentSize.x / 8 - newSize.x) / 2), Mathf.Abs((parentSize.y / 8 - newSize.y) / 2));
                gameObject.GetComponent<Image>().raycastPadding = new Vector4(padding.x, padding.y, padding.x, padding.y);

                // Increase the index
                index++;
            }
        }
    }
}

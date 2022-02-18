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

        private void CreatePieces()
        {
            int index = 0;
            foreach (var square in table.squares)
            {
                if (square.Piece == Piece.none) continue;
                GameObject gameObject = Instantiate(prefab, parent.transform) as GameObject;
                gameObject.name = "Piece" + index;
                gameObject.GetComponent<RectTransform>().localPosition = new Vector3(square.Position.x * 120 + 60, square.Position.y * 120 + 60);
                //Texture2D texture = table.textures[(int)square.Piece];
                Sprite sprite = table.textures[(int)square.Piece];
                gameObject.GetComponent<Image>().sprite = sprite;
                gameObject.GetComponent<RectTransform>().sizeDelta = sprite.textureRect.size * pieceSize;
                index++;
            }
        }
    }
}

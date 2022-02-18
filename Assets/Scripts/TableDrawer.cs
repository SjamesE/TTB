using UnityEngine;
using UnityEngine.UI;

namespace Chess
{
    public class TableDrawer : MonoBehaviour
    {
        Image image;
        Texture2D texture;

        [SerializeField]
        public Color lightColor;
        [SerializeField]
        public Color darkColor;

        // Start is called before the first frame update
        void Start()
        {
            //Initialize Table
            image = GetComponent<Image>();
            texture = new Texture2D(8, 8);
            UpdateTable();

            //Initialize Pieces
        }

        void UpdateTable()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Color color = ( x % 2 < 1  ==  y % 2 < 1 ) ? lightColor : darkColor;
                    texture.SetPixel(x, y, color);
                }
            }
            texture.filterMode = FilterMode.Point;
            texture.Apply();
            image.sprite = Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(8,8)), Vector2.zero);
        }
    }
}
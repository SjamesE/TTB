using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chess
{
    public enum Piece
    {
        none,
        WPawn,
        WRook,
        WKnight,
        WBishop,
        WQueen,
        WKing,
        BPawn,
        BRook,
        BKnight,
        BBishop,
        BQueen,
        BKing
    }

    public class TableSquare
    {
        public Vector2 Position { get; private set; }
        public string Notation { get; private set; }
        public Piece Piece { get; set; }

        public TableSquare(int x, int y, string notation)
        {
            Position = new Vector2(x, y);
            Notation = notation;
        }
    }

    public class Table
    {
        public List<TableSquare> squares;
        public List<Sprite> textures;

        public Table()
        {
            squares = new List<TableSquare>();

            textures = new List<Sprite>();
            List<Sprite> tempTextures = Resources.LoadAll<Sprite>("ChessPieces").ToList();

            textures.Add(Sprite.Create(new Texture2D(1, 1), new Rect(), Vector2.zero));
            textures.Add(tempTextures[9]);
            textures.Add(tempTextures[11]);
            textures.Add(tempTextures[8]);
            textures.Add(tempTextures[6]);
            textures.Add(tempTextures[10]);
            textures.Add(tempTextures[7]);
            textures.Add(tempTextures[3]);
            textures.Add(tempTextures[5]);
            textures.Add(tempTextures[2]);
            textures.Add(tempTextures[0]);
            textures.Add(tempTextures[4]);
            textures.Add(tempTextures[1]);


            ResetSquares();
        }

        public void ResetSquares()
        {
            char[] letters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    int index = y * 8 + x;
                    squares.Add(new TableSquare(x, y, letters[x] + y.ToString()));
                    squares[index].Piece = Piece.none;
                }
            }

            //White Pieces
            squares[0].Piece  = Piece.WRook;
            squares[1].Piece  = Piece.WKnight;
            squares[2].Piece  = Piece.WBishop; 
            squares[3].Piece  = Piece.WQueen;
            squares[4].Piece  = Piece.WKing;
            squares[5].Piece  = Piece.WBishop;
            squares[6].Piece  = Piece.WKnight;
            squares[7].Piece  = Piece.WRook;
            squares[8].Piece  = Piece.WPawn;
            squares[9].Piece  = Piece.WPawn;
            squares[10].Piece = Piece.WPawn;
            squares[11].Piece = Piece.WPawn;
            squares[12].Piece = Piece.WPawn;
            squares[13].Piece = Piece.WPawn;
            squares[14].Piece = Piece.WPawn;
            squares[15].Piece = Piece.WPawn;

            //Black Pieces
            squares[48].Piece = Piece.BPawn;
            squares[49].Piece = Piece.BPawn;
            squares[50].Piece = Piece.BPawn;
            squares[51].Piece = Piece.BPawn;
            squares[52].Piece = Piece.BPawn;
            squares[53].Piece = Piece.BPawn;
            squares[54].Piece = Piece.BPawn;
            squares[55].Piece = Piece.BPawn;
            squares[56].Piece = Piece.BRook;
            squares[57].Piece = Piece.BKnight;
            squares[58].Piece = Piece.BBishop;
            squares[59].Piece = Piece.BQueen;
            squares[60].Piece = Piece.BKing;
            squares[61].Piece = Piece.BBishop;
            squares[62].Piece = Piece.BKnight;
            squares[63].Piece = Piece.BRook;
        }
    }
}
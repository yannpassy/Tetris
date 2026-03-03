using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board board {get; private set;}
    public TetrominoData tetromino {get; private set;}
    public Vector3Int[] cells {get; private set;}
    public Vector3Int position {get; private set;}
    public void Initialize(Board _board, Vector3Int _position, TetrominoData _tetromino)
    {
        this.board = _board;
        this.position = _position;
        this.tetromino = _tetromino;

        if(this.cells == null)
        {
            this.cells = new Vector3Int[_tetromino.cells.Length]; // which is 4 for length
        }

        for (int i = 0; i < _tetromino.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)_tetromino.cells[i];
        }
    }
}

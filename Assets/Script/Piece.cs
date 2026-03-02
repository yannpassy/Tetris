using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board board {get; private set;}
    public TetrominoData data {get; private set;}
    public Vector3Int[] cells {get; private set;}
    public Vector3Int position {get; private set;}
    public void Initialize(Board _board, Vector3Int _position, TetrominoData _data)
    {
        this.board = _board;
        this.position = _position;
        this.data = _data;

        if(this.cells == null)
        {
            this.cells = new Vector3Int[_data.cells.Length]; // which is 4 for length
        }
    }
}

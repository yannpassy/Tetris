using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    private void Update() 
    {
        this.board.ClearTetromino(this);

        if(Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        this.board.DisplayTetromino(this);
    }

    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }
    }

    private bool Move(Vector2Int _translation)
    {
        Vector3Int newPosition = this.position;
        newPosition.x += _translation.x;
        newPosition.y += _translation.y;

        bool valid = this.board.IsValidPosition(this, newPosition);
        
        if(valid)
        {
            this.position = newPosition;
        }

        return valid;
    }
}

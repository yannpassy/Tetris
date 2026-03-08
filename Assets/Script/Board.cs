using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap {get; private set;}
    public Piece activePiece {get; private set;}
    public TetrominoData[] tetrominoes;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10,20);

    public RectInt boardBounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake() 
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponent<Piece>();
        for (int i = 0; i < tetrominoes.Length; i++)
        {
            this.tetrominoes[i].Initialize();
        }
    }

    private void Start() 
    {
        SpawnPiece();
    }

    public void SpawnPiece()
    {
        int random = Random.Range(0, this.tetrominoes.Length);
        TetrominoData tetromino = this.tetrominoes[random];

        this.activePiece.Initialize(this, this.spawnPosition , tetromino);
        DisplayTetromino(this.activePiece);
    }

    public void DisplayTetromino(Piece _piece)
    {
        for (int i = 0; i < _piece.cells.Length; i++)
        {
            Vector3Int tilePosition = _piece.cells[i] + _piece.position;
            this.tilemap.SetTile(tilePosition, _piece.tetromino.tile); // affiche la case dans le board à la bonne place
        }
    }

    public void ClearTetromino(Piece _piece)
    {
        for (int i = 0; i < _piece.cells.Length; i++)
        {
            Vector3Int tilePosition = _piece.cells[i] + _piece.position;
            this.tilemap.SetTile(tilePosition, null); // vide la case dans le board à la bonne place
        }
    }

    public bool IsValidPosition(Piece _piece, Vector3Int _newPosition)
    {
        RectInt bounds = this.boardBounds;
        for (int i = 0; i < _piece.cells.Length; i++)
        {
            Vector3Int tilePosition = _piece.cells[i] + _newPosition;

            if (!bounds.Contains((Vector2Int)tilePosition)) // if tilePosition is not present inside the rect ( the board)
            {
                return false;
            }

            if (this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
            
        }
        
        return true;
    }
}

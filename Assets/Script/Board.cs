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
        Set(this.activePiece);
    }

    public void Set(Piece _piece)
    {
        for (int i = 0; i < _piece.cells.Length; i++)
        {
            Vector3Int tilePosition = _piece.cells[i] + _piece.position;
            this.tilemap.SetTile(tilePosition, _piece.tetromino.tile);
        }
    }
}

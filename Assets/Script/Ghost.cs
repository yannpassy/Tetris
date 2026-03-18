using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    public Tile ghostTile;
    public Board board;
    public Piece trackingPiece;

    public Tilemap ghostTilemap {get; private set;}
    public Vector3Int[] cells {get; private set;}
    public Vector3Int position {get; private set;}

    private void Awake() 
    {
        this.ghostTilemap = GetComponentInChildren<Tilemap>();
        this.cells = new Vector3Int[4];
    }

    void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        DisplayGhostPiece();
    }

    private void Clear()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3Int tilePosition = this.cells[i] + this.position;
            this.ghostTilemap.SetTile(tilePosition, null); // affiche la case dans le board à la bonne place
        }
    }

    private void Copy() // copy the tracking piece to the ghost cells
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            this.cells[i] = this.trackingPiece.cells[i];
        }
            
    }

    private void Drop()
    {
        Vector3Int position = this.trackingPiece.position;

        int currentY = position.y;
        int bottomY =  (- this.board.boardSize.y /2) -1 ;

        this.board.ClearTetromino(this.trackingPiece);  // pour eviter que le ghostpiece soit en collision avec la vrai piece

        for (int row = currentY; row >= bottomY; row--)
        {
            position.y =row;

            if(this.board.IsValidPosition(this.trackingPiece, position))
            {
                this.position = position;
            }
            else
            {
                break;
            }
        }

        this.board.DisplayTetromino(this.trackingPiece); // après avoir touver la position du ghostpiece, on preut réafficher la vrai
    }

    private void DisplayGhostPiece()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3Int tilePosition = this.cells[i] + this.position;
            this.ghostTilemap.SetTile(tilePosition, this.ghostTile); // affiche la case dans le board à la bonne place
        }
    }
}

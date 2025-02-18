using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSystem : MonoBehaviour
{    
    [SerializeField] private TileBase highlightTile; 
    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private Tilemap tempTilemap;

    public TileBase buildingTileMap;

    private Vector3Int highlightedTilePos;
    private bool highlighted;

    public Vector3Int GetMouseOnGridPos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int  mouseCellPos = mainTilemap.WorldToCell(mousePos);
        mouseCellPos.z = 0;
        
        return mouseCellPos;
    }


    public void placeBuildingTile(Vector3 position)
    {
        Vector3Int cellPosition = mainTilemap.WorldToCell(position);
        mainTilemap.SetTile(cellPosition, buildingTileMap);
    }

    public bool isTileMapFree(Vector3 position)
    {
        Vector3Int cellPosition = mainTilemap.WorldToCell(position);
        TileBase tile = mainTilemap.GetTile(cellPosition);

        return !(tile == buildingTileMap);
    }

    private void HighlightTile()
    {
        Vector3Int  mousegridPos = GetMouseOnGridPos(); 
        if (highlightedTilePos != mousegridPos)
        {
            tempTilemap.SetTile(highlightedTilePos, null);

            TileBase tile = mainTilemap.GetTile(mousegridPos);

            if (tile)
            {
                tempTilemap.SetTile(mousegridPos, highlightTile);
                highlightedTilePos = mousegridPos;

                highlighted = true;
            }
            else{
                highlighted = false;
            }
        }

    }

    private void Update()
    {
        HighlightTile();
    }
}

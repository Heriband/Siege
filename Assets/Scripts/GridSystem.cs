using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSystem : MonoBehaviour
{    
    [SerializeField] private TileBase highlightTile; 
    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private Tilemap tempTilemap;

    private Vector3Int highlightedTilePos;
    private bool highlighted;

    public Vector3Int GetMouseOnGridPos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int  mouseCellPos = mainTilemap.WorldToCell(mousePos);
        mouseCellPos.z = 0;
        
        return mouseCellPos;
    }

    private void HighlightTile()
    {
        Vector3Int  mousegridPos = GetMouseOnGridPos(); 
        Debug.Log(mousegridPos);
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

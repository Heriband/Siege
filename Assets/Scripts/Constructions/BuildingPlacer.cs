using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{

    public GridSystem gridSystem;
    public static BuildingPlacer instance;

    protected GameObject building;
    protected GameObject buildingGhost;

    private Camera cameraPlayer;


    private void Awake()
    {
        instance = this;
        cameraPlayer = Camera.main;
        building = null;
        buildingGhost = null;
    }

    private void Update()
    {
        //mode construction
        if (building)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CancelBuilding();
                return;
            }

            Vector3Int mouseCellPos = gridSystem.GetMouseOnGridPos();
            Vector3 newPos = new Vector3(mouseCellPos.x + 0.5f, mouseCellPos.y + 0.5f, 0);
            ConstructionController controller = building.GetComponent<ConstructionController>();

            if (buildingGhost)
            {
                buildingGhost.transform.position = newPos;
                (buildingGhost.GetComponent<SpriteRenderer>()).color = canBePlaced(controller) ?  Color.green :  Color.red;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (canBePlaced(controller))
                {   
                    CastleController.instance.spendGold(controller.costGold);
                    PlaceBuilding(newPos);
                }
            }
        }
    }

    public void SetBuildingPrefab(GameObject prefab)
    {
        building = prefab;
        _PrepareBuilding();
    }

    protected virtual void _PrepareBuilding()
    {
        if (buildingGhost != null)
        {
            Destroy(buildingGhost);
        }

        buildingGhost = Instantiate(building);
        buildingGhost.SetActive(true);
        disableScript(buildingGhost);
    }

    private bool canBePlaced(ConstructionController controller)
    {
        return building && gridSystem.isTileMapFree(buildingGhost.transform.position) &&
               CastleController.instance.getGold() >= controller.costGold;
    }
    
    private void PlaceBuilding(Vector3 position)
    { 
            //Debug.Log("tower create");
            Instantiate(building, position, Quaternion.identity);
            gridSystem.placeBuildingTile(position);


            Destroy(buildingGhost);
            buildingGhost = null; 
            building = null;
        
    }

    private void CancelBuilding()
    {
        if (buildingGhost)
        {
            Destroy(buildingGhost);
            buildingGhost = null;
        }
        building = null;
    }


    public void disableScript(GameObject o)
    {
        MonoBehaviour[] scripts = o.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false; 
        }
    }

    public void enableScript(GameObject o)
    {
        MonoBehaviour[] scripts = o.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true; 
        }
    }
}

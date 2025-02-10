using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{
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
        if (building != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CancelBuilding();
                return;
            }

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // Distance par rapport à la caméra
            Vector3 worldPosition = cameraPlayer.ScreenToWorldPoint(mousePosition);

            if (buildingGhost != null)
            {
                buildingGhost.transform.position = worldPosition;
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlaceBuilding(worldPosition);
            }
        }
    }

    public void SetBuildingPrefab(GameObject prefab)
    {
        //Debug.Log("set building prefab");
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

    private void PlaceBuilding(Vector3 position)
    {
        if (building != null)
        {
            Instantiate(building, position, Quaternion.identity);

            Destroy(buildingGhost);
            buildingGhost = null; 
            building = null;
        }
    }

    private void CancelBuilding()
    {
        if (buildingGhost != null)
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

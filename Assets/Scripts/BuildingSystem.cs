using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static EnumsAndStructs;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem instance;

    public GridLayout gridLayout;
    private Grid grid;

    [SerializeField] Tilemap BuildTilemap;
    [SerializeField] TilemapRenderer tilemapRenderer;
    [SerializeField] Tilemap BackGroundTilemap;
    [SerializeField] TileBase[] occupacedTiles;
    [SerializeField] AudioSource audiom;
    [SerializeField] AudioClip sound;


    public PlecableObject objectToPlace;

    private void Awake()
    {
        instance = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }
    private void Update()
    {
        if (!objectToPlace)
        {
            tilemapRenderer.enabled = false;
            return;
        }
        else
        {
            tilemapRenderer.enabled = true;
        }
           

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanBePlaced(objectToPlace) && objectToPlace.Place())
            {
                audiom.PlayOneShot(sound);
                getResources(objectToPlace);
                Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                TakeArea(start, objectToPlace.Size);
                objectToPlace = null;
            }
            else
            {
                if (objectToPlace != null)
                    Destroy(objectToPlace.gameObject);
            }
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {

        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    public void InitializeWithObject(GameObject prefab)
    {

        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlecableObject>();
        obj.AddComponent<ObjectDrag>();

    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];

        int counter = 0;
        foreach(var cell in area.allPositionsWithin)
        {
            
            Vector3Int pos = new Vector3Int(cell.x, cell.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    private bool CanBePlaced(PlecableObject obj)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        area.size = new Vector3Int(obj.Size.x + 1, obj.Size.y + 1, 1);

        TileBase[] buildArray = GetTilesBlock(area, BuildTilemap);
        TileBase[] backgroundArray = GetTilesBlock(area, BackGroundTilemap);


        for(var i = 0; i < buildArray.Length; i++)
        {
            if (isOccupaced(buildArray[i]) || !backgroundArray[i])
            {
                return false;
            }
        }
        return true;
    }

    private bool isOccupaced(TileBase o)
    {
        foreach(var v in occupacedTiles)
        {
            if (v == o)
            {
                return true;
            }
        }
        return false;
    }

    private void getResources(PlecableObject obj)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        area.position -= new Vector3Int(2, 2);
        area.size = new Vector3Int(obj.Size.x + 4, obj.Size.y + 4, 1);
       

        TileBase[] buildArray = GetTilesBlock(area, BuildTilemap);

        int wood = 0;
        int stone = 0;

        for (var i = 0; i < buildArray.Length; i++)
        {
            if (buildArray[i] == occupacedTiles[1])
                stone++;
            else if (buildArray[i] == occupacedTiles[2])
                wood++;
        }
        obj.nearResources.Add(new EnumsAndStructs.Resource() { name = ResourceType.Wood, amount = wood });
        obj.nearResources.Add(new EnumsAndStructs.Resource() { name = ResourceType.Stone, amount = stone });
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        BuildTilemap.BoxFill(start, occupacedTiles[0], start.x, start.y,
                            start.x + size.x, start.y + size.y);
    }
}   

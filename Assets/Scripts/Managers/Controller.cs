using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Controller : MonoBehaviour
{
    //Variables
    Ray2D ray;
    RaycastHit2D rayHit;
    LayerMask unplaceableLayer;
    new Camera camera;
    float xAxisValue;
    float yAxisValue;

    public static Action<string> OnPlayerError;
    public static Action<int> OnUnitPlacement;

    private GameManager gameMan;

    //Unit instantiating.
    private GameObject selectedUnit;
    private UnitBase selectedUnitObject;
    private UnitPool unitPool;
    private UnitBase newUnitObject;

    private int mouseUpCheck;

    //Grid used in the level.
    Grid grid;
    //The bounds scriptable object that holds how far the camera can move left, right, up, and down.
    [SerializeField] LevelBounds bounds;

    //Init
    void Start()
    {
        grid = FindObjectOfType<Grid>();
        camera = FindObjectOfType<Camera>();
        gameMan = FindObjectOfType<GameManager>();
        unitPool = FindObjectOfType<UnitPool>();

        selectedUnit = null;
        mouseUpCheck = 0;
    }

    void Update()
    {
        //**--CAMERA MOVEMENT--**//
        //If w, a, s, or d are being pressed. Move camera. Arrow keys also work.
        {
            if (Input.GetAxis("Horizontal") > 0.0f || Input.GetAxis("Vertical") > 0.0f ||
                Input.GetAxis("Horizontal") < 0.0f || Input.GetAxis("Vertical") < 0.0f)
            {
                CameraMovement();
            }
        }
        //**--END CAMERA MOVEMENT--**//

        //**--UNIT SELECTION AND PLACEMENT--**//
        //If a unit is selected...
        if (selectedUnit != null)
        {
            //Drag the unit's image around the screen so the player knows where they will place it.
            selectedUnit.transform.position = new Vector3(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);

            //...and the player right clicks...
            if(Input.GetMouseButtonUp(1))
            {
                //Deselect the unit.
                Deselect();
            }

            //...and if the player left clicks...
            if (Input.GetMouseButtonUp(0))
            {
                //...and they have a selected unit...
                if (mouseUpCheck == 2)
                {
                    RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    //...and the player has enough funds to buy said unit...
                    if(gameMan.balance >= selectedUnitObject.cost && hit.collider == null)
                    {
                        //Buy and place unit.
                        PlaceUnit(selectedUnit);
                    }
                    else if(hit.collider != null)
                    {
                        if (OnPlayerError != null)
                            OnPlayerError("Cannot place a unit there!");
                    }
                    else
                    {
                        //Tell the player they do not have enough funds to buy this unit.
                        if (OnPlayerError != null)
                            OnPlayerError("Insufficent Funds!");
                    }   
                }
                else
                {
                    //We now have a unit selected.
                    mouseUpCheck++;
                }
            }
        }
    }

    //FUNCTION//
    //CAMERAMOVEMENT: Called when Horizontal or Vertical axis > 0.0.
    //Translates the camera's position.
    void CameraMovement()
    {
        //Camera movement axis.
        xAxisValue = Input.GetAxis("Horizontal") / 10;
        yAxisValue = Input.GetAxis("Vertical") / 10;

        //Check if the camera can move in whatever direction it is trying to move in. If it can't, return out of the function.
        if (camera.transform.position.x >= bounds.maxX && Input.GetAxis("Horizontal") > 0.0f)
        {
            xAxisValue = 0.0f;
        }
        if(camera.transform.position.x <= bounds.minX && Input.GetAxis("Horizontal") < 0.0f)
        {
            xAxisValue = 0.0f;
        }
        if(camera.transform.position.y >= bounds.maxY && Input.GetAxis("Vertical") > 0.0f)
        {
            yAxisValue = 0.0f;
        }
        if (camera.transform.position.y <= bounds.minY && Input.GetAxis("Vertical") < 0.0f)
        {
            yAxisValue = 0.0f;
        }

        //Move camera on input.
        camera.transform.Translate(xAxisValue, 0.0f, 0.0f);
        camera.transform.Translate(0.0f, yAxisValue, 0.0f);
    }

    //FUNCTION//
    //SELECTUNIT: Called when a unit selection button is clicked.
    //Set which unit is supposed to be selected. Called when a unit button is clicked.
    public void SelectUnit(GameObject unitToSelect)
    {
        selectedUnit = unitToSelect;
        selectedUnitObject = unitToSelect.GetComponent<UnitBase>();
        selectedUnit = Instantiate(selectedUnit, new Vector3(Input.mousePosition.x, Input.mousePosition.y), Quaternion.identity);
        selectedUnit.layer = 2;
        mouseUpCheck++;
    }

    public void PlaceUnit(GameObject newUnit)
    {
        //Create spotholder.
        //Spotholder is instantiated on top of the unit so another unit cannot be placed there. This is because the unit itself has a circle collider that takes up more space than wanted for raycast detection.
        GameObject spotHolder = new GameObject("Spot Holder");
        spotHolder.AddComponent<BoxCollider2D>();
        spotHolder.GetComponent<BoxCollider2D>().size = newUnit.GetComponent<BoxCollider2D>().size;
        spotHolder.layer = 0;

        //Instantiate a spotholder at mouse position.
        //GameObject createdUnit = new GameObject(newUnit, new Vector3(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y, 0.0f), Quaternion.identity);
        //GameObject createdUnit = newUnit;
        GameObject createdUnit = unitPool.CreateNew(newUnit, camera.ScreenToWorldPoint(Input.mousePosition));
        Instantiate(spotHolder, createdUnit.transform);
        //Get the unitpool to create a new unit.
        newUnitObject = createdUnit.GetComponent<UnitBase>();

        //Get the centre of the cell the unit was created in and move it there.
        Vector3Int cellPosition = grid.WorldToCell(createdUnit.transform.position);
        createdUnit.transform.position = grid.GetCellCenterWorld(cellPosition);

        //Set the unit to active so it can attack.
        newUnitObject.isActive = true;

        //Add the unit to ignore raycast layer.
        createdUnit.layer = 2;

        //Raise the unit's OnCreation event to take money from the player.
        if(UnitBase.OnCreation != null)
            UnitBase.OnCreation(createdUnit);

        if (OnUnitPlacement != null)
            OnUnitPlacement(createdUnit.GetInstanceID());
    }

    //Destroys the selected unit. Can only be called if there is a selected unit currently and the player right clicks.
    public void Deselect()
    {
        Destroy(selectedUnit);
        mouseUpCheck = 0;
    }
}

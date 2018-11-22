using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInfo : MonoBehaviour {

    //all surounding hexxes
    public List<GameObject> neighbourTiles = new List<GameObject>();

    //Terain variables
    public bool canPass;
    public int team;
    public int movementCost;
    public string tileType;
    public string tileInfo;

    // NeighbourCheck variables
    public int rayPos = 30;
    public Vector3 rayCast;

    //movement variables
    public bool canMoveTo;
    public int pointCost;
    public Renderer rend;
    private Color baseColor;

    //Refrences
    private MovementManager mm;
    public UIManager ui;
    public GameObject myUnit;

    void Start()
    {
        //variable asigning
        ui = UIManager.instance;
        mm = MovementManager.instance;
        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;

        //Casts raycasts out from all sides of the hex.
        //All hexes it hits get add to the neighbouringtiles list.
        RaycastHit hit;
        for(int i = 0; i < 6; i++)
        {
            rayCast = Quaternion.AngleAxis(rayPos, transform.forward) * transform.up;
            if (Physics.Raycast(transform.position, rayCast, out hit,2))
            {
                neighbourTiles.Add(hit.collider.gameObject);
            }
            rayPos += 60;
        }
    }

    //Sets hover collor
    void OnMouseEnter()
    {
        //Hover whitout AStar collors
        if(!canMoveTo)
        {
            rend.material.SetColor("_Color",Color.cyan);
        }
        //Hover whit AStar collors
        else
        {
            Color c = new Color(0.6f,0.6f,0.6f,1);
            rend.material.SetColor("_Color",c);
        }
    }

    //reverts hover collor
    void OnMouseExit()
    {
        //Hover whitout AStar collors
        if(!canMoveTo)
        {
            rend.material.SetColor("_Color",baseColor);
        }
        //Hover whit AStar collors
        else
        {
            rend.material.SetColor("_Color",Color.gray);
        }
    }

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(mm.movingUnit != null)
            {
                MovementManager.instance.ResetTiles();
                mm.movingUnit = null;
            }
            ui.TilePopup(this);
        }

        if(Input.GetButtonDown("Fire2"))
        {
            if(mm.movingUnit != null && myUnit == null)
            {
                ProcesUnit(mm.movingUnit);
            }
        }
    }

    public void ProcesAStarPathfinding(int movementPoints)
    { 
        foreach (GameObject tile in neighbourTiles)
        {
            print("Start");
            tile.GetComponent<HexInfo>().AStarStep(movementPoints,0);
        }
    }

    public void AStarStep(int movementPoints, int cost)
    {
        if(canPass == true && myUnit == null)
        {
            int newPointCost = cost + movementCost;
            if(pointCost != 0)
            {
                if(pointCost > newPointCost)
                {
                    pointCost = newPointCost;
                }
            }
            else
            {
                 pointCost = newPointCost;
            }
            MovementManager.resetTiles += AStarReset;
            if(newPointCost == movementPoints)
            {
                rend.material.SetColor("_Color",Color.gray);
                canMoveTo = true;
            }
            else if(newPointCost < movementPoints)
            {
                rend.material.SetColor("_Color",Color.gray);
                canMoveTo = true;
                foreach (GameObject tile in neighbourTiles)
                {
                    print(pointCost + "cost");
                    tile.GetComponent<HexInfo>().AStarStep(movementPoints,pointCost);
                }
            }
        }
    }

    public void AStarReset()
    {
        MovementManager.resetTiles -= AStarReset;
        canMoveTo = false;
        pointCost = 0;
        rend.material.SetColor("_Color",baseColor);
    }


    //can move vraagen en de movement zelf voor bestaande units en asigned var's aan nieuwe
    public void ProcesUnit(GameObject newUnit)
    {
        if(mm.movingUnit != null)
        {
            if(canPass == true)
            {
                if(canMoveTo)
                {
                    myUnit = newUnit;
                    myUnit.GetComponent<UnitInfo>().tile.GetComponent<HexInfo>().myUnit = null;
                    myUnit.GetComponent<UnitInfo>().toMove -= pointCost;
                    myUnit.GetComponent<UnitInfo>().tile = this.gameObject;
                    myUnit.transform.position = this.gameObject.transform.position;
                    ui.UpdateMovementLeft();
                    mm.movingUnit = null;
                    ui.TurnOffUIInfo();
                    MovementManager.instance.ResetTiles();
                }
            }
        }
        else
        {
            myUnit = newUnit;
            myUnit.GetComponent<UnitInfo>().tile = this.gameObject;
        }
    }
}

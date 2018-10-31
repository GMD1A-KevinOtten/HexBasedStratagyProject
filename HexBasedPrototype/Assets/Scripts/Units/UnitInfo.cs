using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour {

    public Vector3 baseScale;
    private MovementManager mm;
    private UIManager ui;
    public GameObject tile;
    public string unitName;
    public string info;
    public int baseToMove;
    public int toMove;
    public int team;
    public int hp;
    public int dmg;

    private void OnEnable()
    {
        UIManager.TurnEnd += AddMovement;
    }

    private void OnDisable()
    {
        UIManager.TurnEnd -= AddMovement;
    }

    public virtual void Start()
    {
        mm = MovementManager.instance;
        ui = UIManager.instance;
        baseScale = gameObject.transform.localScale;
    }

    void OnMouseEnter()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ui.UnitPopup(this);
            mm.movingUnit = this.gameObject;
            tile.GetComponent<HexInfo>().ProcesAStarPathfinding(toMove);
        }
    }

    void OnMouseExit()
    {
        transform.localScale = baseScale;
    }

    public void AddMovement()
    {
        toMove = baseToMove;
    }
}

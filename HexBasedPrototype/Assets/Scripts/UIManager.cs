using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public GameObject tileInfoPannel;
    public Text Name;
    public Text Info;
    public Text Movement;
    public Text Team;
    public GameObject buyPannel;
    public UnitInfo currentUnit;

    public delegate void Turn();
    public static event Turn TurnEnd;

    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("Escape"))
        {
            TurnOffUIInfo();
            MovementManager.instance.movingUnit = null;
        }
    }

    public void TurnOffUIInfo()
    {
        tileInfoPannel.SetActive(false);
        buyPannel.SetActive(false);
    }

    public void TilePopup(HexInfo inf)
    {
        if(buyPannel.activeSelf && Info.gameObject.GetComponent<BaseHex>() == null)
        {
            buyPannel.SetActive(false);
        }
        tileInfoPannel.SetActive(true);
        Name.text = inf.tileType;
        Info.text = inf.tileInfo;
        if(inf.team != 0 )
        {
            Team.text = "Team: " + inf.team.ToString();
        }
        else
        {
           Team.text = "";
        }

        if(inf.movementCost != 0)
        {
            Movement.text = "Movement Cost: " + inf.movementCost.ToString() + " Unit's";
        }
        else
        {
            Movement.text = "Cant Pass";
        }
    }

    public void UnitPopup(UnitInfo uInf)
    {
        if(buyPannel.activeSelf)
        {
            buyPannel.SetActive(false);
        }
        currentUnit = uInf;
        tileInfoPannel.SetActive(true);
        Name.text = uInf.unitName;
        Info.text = uInf.info;
        Team.text = "Team: " + uInf.team.ToString();
        UpdateMovementLeft();
    }

    public void UpdateMovementLeft()
    {
        Movement.text = "Movement Left: " + currentUnit.toMove.ToString() + " Unit's";
    }

    public void TurnOnBuyPannel()
    {
        buyPannel.SetActive(true);
    }

    public void EndTurn()
    {
        if(TurnEnd != null)
        {
            TurnEnd();
            TurnOffUIInfo();
        }
    }
}

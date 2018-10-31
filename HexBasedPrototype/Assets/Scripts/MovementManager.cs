using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

	//This script holds information relevant to movement and the tiles used in this

	public static MovementManager instance;
	public GameObject movingUnit;

	public delegate void Tiles();
    public static event Tiles resetTiles;

	//Reset changes to tiles subscribed to the event after movement or deselect
	public void ResetTiles()
	{
		resetTiles();
	}

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
}

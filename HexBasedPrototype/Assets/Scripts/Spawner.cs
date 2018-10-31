using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public List<GameObject> Units = new List<GameObject>();
    public GameObject spawnPos;


    public void SpawnGarrison()
    {
        if(spawnPos.GetComponent<HexInfo>().myUnit == null)
        {
            spawnPos.GetComponent<HexInfo>().ProcesUnit(Instantiate(Units[0], spawnPos.transform.position, Quaternion.identity));
        }
    }
}

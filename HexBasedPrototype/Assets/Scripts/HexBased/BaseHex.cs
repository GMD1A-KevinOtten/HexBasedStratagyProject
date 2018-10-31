using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHex : MonoBehaviour {

    private UIManager ui;
    public Spawner sp;

    private void Start()
    {
        ui = UIManager.instance;
        sp = GameObject.FindWithTag("Canvas").GetComponent<Spawner>();
    }

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ui.TurnOnBuyPannel();
            sp.spawnPos = this.gameObject;
        }
    }
}

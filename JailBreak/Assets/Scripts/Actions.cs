using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour {

    Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void CloseMap()
    {
        //TODO reactivate camera control
        player.SetPlayerDisabled(false);
        GameObject.Find("Canvas").transform.Find("Map").gameObject.SetActive(false);
    }
}

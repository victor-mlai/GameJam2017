using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour {

    Player player;
    AudioManager audioManager;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void CloseMap()
    {
        audioManager.Play("Map Fold");
        player.SetPlayerDisabled(false);
        GameObject.Find("Canvas").transform.Find("Map").gameObject.SetActive(false);
    }
}

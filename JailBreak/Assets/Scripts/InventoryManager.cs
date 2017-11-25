using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    List<string> inventoryList;
    Player player;

    public List<Sprite> sprites;
    List<GameObject> slots = new List<GameObject>();
    public GameObject map;

    // Use this for initialization
    void Start () {

        for (int i = 0; i < 9; i++)
        {
            string slot = "Slot (" + i + ")";
            slots.Add(gameObject.transform.Find(slot).Find("Button").gameObject);
        }

        player = GameObject.Find("Player").GetComponent<Player>();
        inventoryList = new List<string>();
        inventoryList.Add("map");
        inventoryList.Add("key");
    }
	
	// Update is called once per frame
	void Update () {
        //inventoryList = player.getInventoryList();
        int i = 0;
        foreach (string item in inventoryList)
        {
            switch (item)
            {
                case "key":
                    slots[i].GetComponent<Image>().sprite = sprites[1];
                    slots[i].GetComponent<Button>().interactable = true;
                    break;
                case "map":
                    slots[i].GetComponent<Image>().sprite = sprites[2];
                    slots[i].GetComponent<Button>().interactable = true;
                    break;
                default:
                    slots[i].GetComponent<Image>().sprite = sprites[0];
                    slots[i].GetComponent<Button>().interactable = false;
                    break;
            }
            i++;
        }

        while (i < slots.Count)
        {
            slots[i].GetComponent<Image>().sprite = sprites[0];
            slots[i].GetComponent<Button>().interactable = false;
            i++;
        }

	}

    public void UseItem(int number)
    {
        string item = slots[number - 1].GetComponent<Image>().sprite.name;

        switch (item)
        {
            case "key":
                break;
            case "Map":
                map.SetActive(true);
                gameObject.SetActive(false);
                break;
            default:
                break;
        }

        //TODO reactivate camera
        //player.SetPlayerDisabled(false);
    }
}

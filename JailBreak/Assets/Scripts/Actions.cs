using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour {

    public void CloseMap()
    {
        //TODO reactivate camera control
        GameObject.Find("Canvas").transform.Find("Map").gameObject.SetActive(false);
    }
}

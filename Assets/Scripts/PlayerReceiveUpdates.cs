﻿using UnityEngine;
using System.Collections;
using SimpleJSON;


//Carson
//Enemy/Allies updating script
public class PlayerReceiveUpdates : MonoBehaviour {
    public int playerID;

	// Use this for initialization
	void Start () {
        NetworkingManager.Subscribe(update_position, DataType.Player, playerID);
        NetworkingManager.Subscribe(took_damage, DataType.Hit, playerID);
        NetworkingManager.Subscribe(died, DataType.Killed, playerID);
        GameData.PlayerPosition.Add(playerID, transform.position);
	}

    void update_position(JSONClass player) {
        Vector3 position = new Vector3(player["x"].AsFloat, player["y"].AsFloat, -10f);
        transform.position = position;
        Quaternion rotation = new Quaternion(0, 0, player["rotationZ"].AsFloat, player["rotationW"].AsFloat);
        transform.rotation = rotation;
        GameData.PlayerPosition[playerID] = position;
    }

    void took_damage(JSONClass packet)
    {
        GetComponent<BaseClass>().doDamage(packet["Damage"].AsFloat, true);
    }

    void died(JSONClass packet)
    {
        NetworkingManager.Unsubscribe(DataType.Player, playerID);
        Destroy(gameObject);
    }
}

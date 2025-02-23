using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kabe : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Collider playerCollider = player.GetComponent<Collider>();
            Collider takoyakiCollider = GetComponent<Collider>();

            if (playerCollider != null && takoyakiCollider != null)
            {
                Debug.Log($"Ignoring collision between {gameObject.name} and {player.name}");
                Physics.IgnoreCollision(takoyakiCollider, playerCollider, true);
            }
            else
            {
                Debug.LogError("Collider not found on Player or Takoyaki!");
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }
}

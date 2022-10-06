using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffLocation : MonoBehaviour
{
    int numberOfPickUps;
    int countDropOffs;

    void Start() {
        var pickUps = GameObject.FindGameObjectsWithTag("Pickup");
        numberOfPickUps = pickUps.Length;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Pickup")) {
            countDropOffs++;

            if (countDropOffs == numberOfPickUps) {
                Debug.Log("You win!");
            } else {
                Debug.Log("You have " + countDropOffs + " out of " + numberOfPickUps + " items");
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Pickup")) {
            countDropOffs--;
        }
    }
}

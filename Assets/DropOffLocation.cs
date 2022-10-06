using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffLocation : MonoBehaviour
{
    int numberOfPickUps;
    int countDropOffs;

    bool isDone = false;
    bool rotateBack = false;

    public GameObject room;

    void Start() {
        var pickUps = GameObject.FindGameObjectsWithTag("Pickup");
        numberOfPickUps = pickUps.Length;
    }

    void Update() { 
        var rotation = room.transform.rotation.eulerAngles;
        if (isDone) {
            if (rotation.y < 180) {
                room.transform.Rotate(0, 1f, 0);
            }

            if (Input.GetMouseButtonDown(0)) {
                rotateBack = true;
            }
        }

        if (rotateBack) {
            if (rotation.y > 0)  {
               room.transform.Rotate(0, 1f, 0);
            } else {
                rotateBack = false;
                isDone = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Pickup")) {
            countDropOffs++;

            if (countDropOffs == numberOfPickUps) {
                Debug.Log("You win!");
                isDone = true;
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

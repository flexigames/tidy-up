using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffLocation : MonoBehaviour
{
    int numberOfPickUps = 2;
    int countDropOffs;

    bool isDone = false;
    bool rotateBack = false;

    public GameObject room;

    public GameObject bookPrefab;

    void Start() {
        SpawnBooks();
    }

    void SpawnBooks() {
        for (int i = 0; i < numberOfPickUps; i++) {
            SpawnBook();
        }
    }

    void RemoveAllPickups() {
        var books = GameObject.FindGameObjectsWithTag("Pickup");
        foreach (var book in books) {
            Destroy(book);
        }
    }

    void SpawnBook() {
        var x = Random.Range(-room.transform.localScale.x / 2f, room.transform.localScale.x / 2f);
        var y = 7f;
        var z = Random.Range(-room.transform.localScale.z / 2f, room.transform.localScale.z / 2f);

        var position = new Vector3(x, y, z);

        var book = Instantiate(bookPrefab, position, Quaternion.identity);
        book.transform.parent = room.transform;
    }

    void Update() { 
        var rotation = room.transform.rotation.eulerAngles;
        if (isDone) {
            if (rotation.y < 180) {
                room.transform.Rotate(0, 1f, 0);
            }

            if (Input.GetMouseButtonDown(0)) {
                RemoveAllPickups();
                countDropOffs = 0;
                numberOfPickUps++;
                SpawnBooks();
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

    void OnEnd() {
        RemoveAllPickups();
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

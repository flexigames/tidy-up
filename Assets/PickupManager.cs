using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private GameObject currentItem;
    private Vector3 itemSize;

    void Update() {

        if (currentItem != null) {
            var mouseRayCast = RayCastMouse();
            if (mouseRayCast != null) {
                var colliderSize = currentItem.GetComponent<Collider>().bounds.size;

                var mouseHit =  mouseRayCast.Value.point;
                var normal = mouseRayCast.Value.normal;
                var pointingUp = normal.y > 0.9f;
                var pontingRight = normal.z > 0.9f;
                var pointingLeft = normal.x < -0.9f;

                var offset = new Vector3(0, 0, 0);

                if (pointingUp) {
                    offset = new Vector3(0f, itemSize.y / 2f, 0f);
                } else if (pontingRight) {
                    offset = new Vector3(0f, 0f, itemSize.z / 2f);
                } else if (pointingLeft) {
                    offset = new Vector3(-itemSize.x / 2f, 0f, 0f);
                }

                currentItem.transform.position = mouseHit + offset;
            }
        } 
        
        if (Input.GetMouseButtonDown(0)) {
            if (currentItem == null) {
                PickUpObject();
            } else {
                DropObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            currentItem.transform.Rotate(0, -45, 0);
        } 
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            currentItem.transform.Rotate(0, 45, 0);
        }
    }

    void DropObject() {
        currentItem.GetComponent<Rigidbody>().isKinematic = false;
        currentItem.GetComponent<Collider>().enabled = true;
        currentItem = null;
        itemSize = Vector3.zero;
    }

    void PickUpObject() {
        var hit = RayCastMouse();
        if (hit is RaycastHit theHit && theHit.collider.gameObject.tag == "Pickup") {
            itemSize = theHit.collider.bounds.size;
            theHit.collider.enabled = false;
            theHit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            currentItem = theHit.collider.gameObject;
            currentItem.transform.rotation = Quaternion.identity;
        }
    }

    RaycastHit? RayCastMouse() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            return hit;
        } else {
            return null;
        }
    }
}

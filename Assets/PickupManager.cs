using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private GameObject currentItem;

    void Update() {

        if (currentItem != null) {
            var mouseRayCast = RayCastMouse();
            if (mouseRayCast != null) {
                var mouseHit =  mouseRayCast.Value.point;
                var normal = mouseRayCast.Value.normal;
                var pointingUp = normal.y > 0.9f;

                if (pointingUp) {
                    currentItem.transform.position = new Vector3(mouseHit.x, mouseHit.y + 0.07f, mouseHit.z);
                } else {
                    currentItem.transform.position = mouseHit + normal * 0.22f;
                }
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
        currentItem.GetComponent<Collider>().enabled = true;
        currentItem = null;
    }

    void PickUpObject() {
        var hit = RayCastMouse();
        if (hit is RaycastHit theHit && theHit.collider.gameObject.tag == "Pickup") {
            theHit.collider.enabled = false;
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

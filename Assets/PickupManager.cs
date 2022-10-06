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
                currentItem.transform.position = new Vector3(mouseHit.x, mouseHit.y + 0.06f, mouseHit.z);
            }
        } 
        
        if (Input.GetMouseButtonDown(0)) {
            if (currentItem == null) {
                PickUpObject();
            } else {
                DropObject();
            }
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

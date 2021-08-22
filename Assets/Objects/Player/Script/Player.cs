using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, HasCameraSlotInterface {
    public GameObject currentStar;
    public GameObject ship;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(initialPosition());
    }

    // Update is called once per frame
    void Update() {
        ship.GetComponent<Ship>().setTarget(currentStar.GetComponent<Star>());
    }

    public void setCurrentStar(GameObject star) {
        currentStar = star; 
    }

    public void setCurrentShip(GameObject shipToAssign) {
        ship = shipToAssign;
        ship.transform.position = transform.position;
    }

    public Vector3 getCameraPosition() {
        return currentStar.GetComponent<Star>().getCameraPosition();
    }

    public Vector3 getCameraTarget() {
        return currentStar.GetComponent<Star>().getCameraTarget();
    }

    protected IEnumerator initialPosition() {
        yield return new WaitUntil(() => currentStar != null);
        yield return new WaitUntil(() => ship != null);

        ship.GetComponent<Ship>().initShip(currentStar.GetComponent<Star>());
    }

}

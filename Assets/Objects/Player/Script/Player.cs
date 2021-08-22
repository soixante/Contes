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
        //Movement();
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

    protected void Movement() {
        if (currentStar != null) {
            //    if (currentStar.GetComponent<Star>().getPlayerPosition() == transform.position) {
            //        ship.GetComponent<>
            //        AnimatorStateInfo stateInfo = shipAnimator.GetCurrentAnimatorStateInfo(0);
            //        if (stateInfo.shortNameHash == jumpInState) {
            //            shipAnimator.SetBool("jump_in", false);
            //        }
            //    }

            //    if (currentStar.GetComponent<Star>().getPlayerPosition() != transform.position) {
            //        AnimatorStateInfo stateInfo = shipAnimator.GetCurrentAnimatorStateInfo(0);
            //        if (stateInfo.shortNameHash == idleState) {
            //            shipAnimator.SetBool("jump_in", true);
            //        }
            //    }

            //    Vector3 targetStarSlotPosition = currentStar.GetComponent<Star>().getPlayerPosition();

            //    if (targetStarSlotPosition != transform.position) {
            //        Vector3 currentPosition = transform.position;
            //        float transition = 1.5f * Time.deltaTime;
            //        Vector3 newPosition = Vector3.Lerp(currentPosition, targetStarSlotPosition, transition);

            //        float currentDistance = Vector3.Distance(newPosition, targetStarSlotPosition);
            //        if (currentDistance < 0.01f) {
            //            newPosition = targetStarSlotPosition;
            //        }

            //        transform.position = newPosition;
            //    }
        }
    }


}

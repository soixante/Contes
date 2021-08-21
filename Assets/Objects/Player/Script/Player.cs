using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, HasCameraSlotInterface {
    public GameObject currentStar;
    public GameObject ship;

    protected Animator shipAnimator;

    int idleState = Animator.StringToHash("idle");
    int jumpInState = Animator.StringToHash("jump_in");
    int jumpOutState = Animator.StringToHash("jump_out");

    // Start is called before the first frame update
    void Start() {
        shipAnimator = ship.GetComponent<Animator>();
        StartCoroutine(initialPosition());
    }

    // Update is called once per frame
    void Update() {
        Movement();
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

        transform.position = currentStar.GetComponent<Star>().getPlayerPosition();
        ship.transform.position = transform.position;
    }

    protected void Movement() {
        if (currentStar != null) {
            //if (currentStar.transform.Find("PlayerSlot").transform.position == transform.position) {
            //    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            //    if (stateInfo.shortNameHash == jumpInState) {
            //        animator.SetBool("jump_in", false);
            //    }
            //}

            //if (currentStar.transform.Find("PlayerSlot").transform.position != transform.position) {
            //    AnimatorStateInfo stateInfo = shipAnimator.GetCurrentAnimatorStateInfo(0);
            //    if (stateInfo.shortNameHash == idleState) {
            //        shipAnimator.SetBool("jump_in", true);
            //    }
            //}

            //Vector3 targetStarSlotPosition = currentStar.transform.Find("PlayerSlot").transform.position;
            //targetStarSlotPosition.y += 0.6f;
            //targetStarSlotPosition.x += 0.6f;
            //targetStarSlotPosition.z += 0.6f;

            //if (targetStarSlotPosition != transform.position) {
            //    Vector3 currentPosition = transform.position;
            //    float transition = 1.5f * Time.deltaTime;
            //    Vector3 newPosition = Vector3.Lerp(currentPosition, targetStarSlotPosition, transition);

            //    float currentDistance = Vector3.Distance(newPosition, targetStarSlotPosition);
            //    if (currentDistance < 0.01f) {
            //        newPosition = targetStarSlotPosition;
            //    }

            //    transform.position = newPosition;
            //}
        }
    }


    protected void onJumpInAnimationEnd() {
        shipAnimator.SetBool("jump_in", false);
        transform.position = currentStar.transform.Find("PlayerSlot").transform.position;
    }
}

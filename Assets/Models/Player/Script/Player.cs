using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject currentStar;
    protected Animator animator;

    int idleState = Animator.StringToHash("idle");
    int jumpInState = Animator.StringToHash("jump_in");
    int jumpOutState = Animator.StringToHash("jump_out");

    // Start is called before the first frame update
    void Start() {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Movement();
    }

    protected void Movement() {
        if (currentStar != null && hasPlayerSlot()) {
            //if (currentStar.transform.Find("PlayerSlot").transform.position == transform.position) {
            //    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            //    if (stateInfo.shortNameHash == jumpInState) {
            //        animator.SetBool("jump_in", false);
            //    }
            //}

            if (currentStar.transform.Find("PlayerSlot").transform.position != transform.position) {
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.shortNameHash == idleState) {
                    animator.SetBool("jump_in", true);
                }
            }

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

    protected bool hasPlayerSlot() {
        return (currentStar.transform.Find("PlayerSlot") != null);
    }

    protected void onJumpInAnimationEnd() {
        animator.SetBool("jump_in", false);
        transform.position = currentStar.transform.Find("PlayerSlot").transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public Vector3 shipPosition;

    protected Animator animator;
    protected bool jumpGateAvailable = false;
    protected GameObject currentStar;

    int idleState = Animator.StringToHash("idle");
    int jumpInState = Animator.StringToHash("jump_in");
    int jumpOutState = Animator.StringToHash("jump_out");

    public void setCurrentStar(GameObject star) {
        if (star.CompareTag("Star")) {
            currentStar = star;
        }
    }

    public void initShip(GameObject star) {
        if (star.CompareTag("Star")) {
            currentStar = star;
        }
        transform.position = currentStar.transform.position + shipPosition;
        jumpGateAvailable = true;
    }

    // Start is called before the first frame update
    void Start() {
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        JumpGateAction();
    }

    protected void onJumpInAnimationEnd() {
        transform.position = currentStar.transform.position + shipPosition;
    }

    protected void JumpGateAction() {
        if (jumpGateAvailable) {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (transform.position == currentStar.transform.position + shipPosition) {
                if (stateInfo.shortNameHash == jumpInState) {
                    animator.SetBool("jump_in", false);
                }
            } else {
                if (stateInfo.shortNameHash == idleState) {
                    animator.SetBool("jump_in", true);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    protected HasShipSlotInterface slot;
    protected Animator animator;
    protected bool jumpGateAvailable = true;

    int idleState = Animator.StringToHash("idle");
    int jumpInState = Animator.StringToHash("jump_in");
    int jumpOutState = Animator.StringToHash("jump_out");

    public void setTarget(HasShipSlotInterface target) {
        slot = target;
    }

    public void initShip(HasShipSlotInterface initialTarget) {
        slot = initialTarget;
        transform.position = slot.getShipPosition();
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
        transform.position = slot.getShipPosition();
    }

    protected void JumpGateAction() {
        if (jumpGateAvailable) {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (slot != null) {
                if (transform.position == slot.getShipPosition()) {
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
}

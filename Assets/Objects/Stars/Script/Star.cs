using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour, HasCameraSlotInterface, HasPlayerSlotInterface, HasShipSlotInterface
{
    public Vector3 playerPosition;
    public Vector3 cameraPosition;
    public Vector3 cameraTargetPosition;

    protected Animator animator;
    protected float rotationSpeed;

    int idleState = Animator.StringToHash("idle");
    int bumpState = Animator.StringToHash("bump");

    // Start is called before the first frame update
    void Start() {
        rotationSpeed = Random.Range(0.5f, 10f);
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        standardRotation();
        //animate();
    }

    private void OnMouseEnter() {
        if (animator != null) {
            animator.SetBool("select", true);
        }
    }

    private void OnMouseExit() {
        if (animator != null) {
            animator.SetBool("select", false);
        }
    }

    public Vector3 getShipPosition() {
        Vector3 absolutePosition = transform.position + (playerPosition != null ? playerPosition : Vector3.zero);
        return absolutePosition;
    }

    public Vector3 getCameraPosition() {
        Vector3 absolutePosition = transform.position + (cameraPosition != null ? cameraPosition : Vector3.zero);
        return absolutePosition;
    }

    public Vector3 getCameraTarget() {
        Vector3 absolutePosition = transform.position + (cameraTargetPosition != null ? cameraTargetPosition : Vector3.zero);
        return absolutePosition;
    }

    public Vector3 getPlayerPosition() {
        Vector3 absolutePosition = transform.position + (playerPosition != null ? playerPosition : Vector3.zero);
        return absolutePosition;
    }

    protected void animate() {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool toBump = Random.Range(0.0f, 2.0f) > 1.8f;
        if (toBump && stateInfo.shortNameHash == idleState) {
            animator.SetBool("bumpin", true);
        } else {
            animator.SetBool("bumpin", false);
        }
    }

    protected void standardRotation() {
        transform.Rotate(0f, -0.02f * rotationSpeed, 0.0f, Space.World);
    }
}

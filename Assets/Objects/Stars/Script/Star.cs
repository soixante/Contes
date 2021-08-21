using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour, HasCameraSlotInterface, HasPlayerSlotInterface
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
        animate();
    }

    public Vector3 getCameraPosition() {
        return (cameraPosition != null ? cameraPosition : Vector3.zero);
    }

    public Vector3 getCameraTarget() {
        return (cameraTargetPosition != null ? cameraTargetPosition : Vector3.zero);
    }

    public Vector3 getPlayerPosition() {
        return (playerPosition != null ? playerPosition : Vector3.zero);
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

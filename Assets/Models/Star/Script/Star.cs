using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    protected float rotationSpeed;
    protected Animator animator;
    protected Material material;
    public Vector3 targetPosition;
    int idleState = Animator.StringToHash("idle");
    int bumpState = Animator.StringToHash("bump");


    // Start is called before the first frame update
    void Start() {
        rotationSpeed = Random.Range(0.5f, 10f);
        animator = gameObject.GetComponent<Animator>();
        material = gameObject.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update() {
        if (targetPosition != transform.position) {
            Vector3 currentPosition = transform.position;
            float transition = 1.5f * Time.deltaTime;
            Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, transition);

            float currentDistance = Vector3.Distance(newPosition, targetPosition);
            if (currentDistance < 0.02f) {
                newPosition = targetPosition;
            }

            transform.position = newPosition;
        }

        transform.Rotate(0f, -0.02f * rotationSpeed, 0.0f, Space.World);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool toBump = Random.Range(0.0f, 2.0f) > 1.8f;
        if (toBump && stateInfo.shortNameHash == idleState) {
            animator.SetBool("bumpin", true);
        } else {
            animator.SetBool("bumpin", false);
        }
    }
}

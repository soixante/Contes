using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    protected float rotationSpeed;
    protected Animator animator;
    protected Material material;
    int idleState = Animator.StringToHash("idle");
    int bumpState = Animator.StringToHash("bump");
    protected bool toDraw = false;

    protected void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 5.0f);
    }


    // Start is called before the first frame update
    void Start() {
        rotationSpeed = Random.Range(0.5f, 10f);
        animator = gameObject.GetComponent<Animator>();
        material = gameObject.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update() {
        Collider[] positionCollider = Physics.OverlapSphere(transform.position, 3.0f);
        if (positionCollider.Length > 1) {
            toDraw = true;
        } else {
            toDraw = false;
        }

        //transform.Rotate(0f, -0.02f * rotationSpeed, 0.0f, Space.World);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool toBump = Random.Range(0.0f, 2.0f) > 1.8f;
        if (toBump && stateInfo.shortNameHash == idleState) {
            animator.SetBool("bumpin", true);
        } else {
            animator.SetBool("bumpin", false);
        }
    }
}

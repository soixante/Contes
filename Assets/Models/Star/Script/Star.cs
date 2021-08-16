using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public bool toDestroy = false;
    protected float rotationSpeed;
    protected Animator animator;
    int idleState = Animator.StringToHash("idle");
    int bumpState = Animator.StringToHash("bump");


    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(0.5f,10f);
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(0f, -0.02f * rotationSpeed, 0.0f, Space.Self);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool toBump = Random.Range(0.0f, 2.0f) > 1.8f;
        if (toBump && stateInfo.shortNameHash == idleState) {
            animator.SetBool("bumpin", true);
        } else {
            animator.SetBool("bumpin", false);
        }

    }

    public void explode() {
        animator.SetBool("bumpin", false);
        animator.SetBool("explode", true);
    }

    public void onExplosionFinished() {
        toDestroy = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Star : MonoBehaviour {
    protected Animator animator;
    protected float rotationSpeed;
    protected GameObject associatedLabel;
    protected string status;
    protected bool focusedState;

    int idleState = Animator.StringToHash("idle");
    int bumpState = Animator.StringToHash("bump");

    // Start is called before the first frame update
    void Start() {
        Quaternion rot = Quaternion.Euler(0, 45, 0);
        transform.rotation = rot;
        rotationSpeed = Random.Range(0.5f, 10f);
        animator = transform.GetComponent<Animator>();
        status = "UNKNOWN";
    }

    // Update is called once per frame
    void Update() {
        standardRotation();
    }

    private void OnMouseEnter() {
        if (!focusedState) {
            if (animator != null) {
                animator.SetBool("select", true);
            }

            if (associatedLabel != null) {
                //associatedLabel.GetComponent<Animator>().SetBool("deployed", true);
            }
        }
    }

    private void OnMouseExit() {
        if (!focusedState) {
            if (animator != null) {
                animator.SetBool("select", false);
            }
            if (associatedLabel != null) {
                //associatedLabel.GetComponent<Animator>().SetBool("deployed", false);
            }
        }
    }

    public void setFocus(bool state) {
        focusedState = state;
        animator.SetBool("select", false);
    }

    public string getStatus() {
        return status;
    }

    public GameObject getAssociatedLabel() {
        return associatedLabel;
    }

    public void setLabel(GameObject starLabel) {
        associatedLabel = starLabel;
    }

    protected void standardRotation() {
        transform.Rotate(0f, -0.02f * rotationSpeed, 0.0f, Space.World);
    }
}

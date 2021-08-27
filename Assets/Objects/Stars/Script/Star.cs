using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Star : MonoBehaviour, HasCameraSlotInterface, HasPlayerSlotInterface, HasShipSlotInterface {
    public Vector3 playerPosition;
    public Vector3 cameraPosition;
    public Vector3 cameraTargetPosition;

    protected Animator animator;
    protected float rotationSpeed;
    protected GameObject associatedLabel;

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
    }

    private void OnMouseEnter() {
        if (animator != null) {
            animator.SetBool("select", true);
        }

        if (associatedLabel != null) {
            Debug.Log($"label: {associatedLabel.transform.name}");
            associatedLabel.GetComponent<Animator>().SetBool("deployed", true);
        }
    }

    private void OnMouseExit() {
        if (animator != null) {
            animator.SetBool("select", false);
        }
        if (associatedLabel != null) {
            Debug.Log($"label: {associatedLabel.transform.name}");
            associatedLabel.GetComponent<Animator>().SetBool("deployed", false);
        }
    }

    public GameObject getAssociatedLabel() {
        return associatedLabel;
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

    public void setLabel(GameObject starLabel) {
        associatedLabel = starLabel;
    }

    protected void standardRotation() {
        transform.Rotate(0f, -0.02f * rotationSpeed, 0.0f, Space.World);
    }
}

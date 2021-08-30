using System.Collections;
using UnityEngine;

public class CameraObject : MonoBehaviour {
    public Vector3 focusPlayerPosition;
    public Vector3 focusStarPosition;

    protected Vector3 targetPosition;
    protected float transitionSpeed;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(initialCamera());
        transitionSpeed = 1.5f;
    }

    // Update is called once per frame
    void Update() {
        Movement();
    }

    public void focus(GameObject focusedObject) {
        if (focusedObject.CompareTag("Star")) {
            transitionSpeed = 4f;
            focusStar(focusedObject);
        }

        if (focusedObject.CompareTag("Player")) {
            transitionSpeed = 1.5f;
            focusPlayer(focusedObject);
        }
    }

    protected void focusPlayer(GameObject player) {
        Vector3 newTargetPosition = player.GetComponent<Player>().currentStar.transform.position + focusPlayerPosition;
        targetPosition = newTargetPosition;
    }

    protected void focusStar(GameObject star) {
        Vector3 newTargetPosition = star.transform.position + focusStarPosition;
        targetPosition = newTargetPosition;
    }

    protected IEnumerator initialCamera() {
        yield return new WaitUntil(() => targetPosition != null);
        transform.position = targetPosition;
    }

    protected void Movement() {
        if (targetPosition != transform.position) {
            Vector3 currentPosition = transform.position;
            float transition = transitionSpeed * Time.deltaTime;
            Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, transition);

            float currentDistance = Vector3.Distance(newPosition, targetPosition);
            if (currentDistance < 0.01f) {
                newPosition = targetPosition;
            }

            transform.position = newPosition;
        }
    }
}

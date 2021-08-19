using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public Vector3 targetPosition;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        //if (targetPosition != transform.position) {
        //    Vector3 currentPosition = transform.position;
        //    float transition = 1.5f * Time.deltaTime;
        //    Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, transition);

        //    float currentDistance = Vector3.Distance(newPosition, targetPosition);
        //    if (currentDistance < 0.02f) {
        //        newPosition = targetPosition;
        //    }

        //    transform.position = newPosition;
        //}
    }
}

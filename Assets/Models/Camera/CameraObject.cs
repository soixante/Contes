using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour {
    public GameObject objectToFocus;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        Movement();
    }

    protected void Movement() {
        if (objectToFocus != null && hasCameraInfos()) {
            Vector3 cameraPosition = objectToFocus.transform.Find("CameraSlot").transform.position;
            Transform cameraTarget = objectToFocus.transform.Find("CameraTarget").transform;
            transform.LookAt(objectToFocus.transform);

            if (cameraPosition != transform.position) {
                Vector3 currentPosition = transform.position;
                float transition = 1.5f * Time.deltaTime;
                Vector3 newPosition = Vector3.Lerp(currentPosition, cameraPosition, transition);

                float currentDistance = Vector3.Distance(newPosition, cameraPosition);
                if (currentDistance < 0.01f) {
                    newPosition = cameraPosition;
                }

                //transform.rotation = Quaternion.RotateTowards( transform.rotation, Quaternion.LookRotation(transform.position - newPosition), transition);
                transform.position = newPosition;
            }
        }
    }

    protected bool hasCameraInfos() {
        return (objectToFocus.transform.Find("CameraSlot") != null && objectToFocus.transform.Find("CameraTarget") != null) ;
    }
}

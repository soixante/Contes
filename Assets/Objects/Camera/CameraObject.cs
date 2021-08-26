using System.Collections;
using UnityEngine;

public class CameraObject : MonoBehaviour {
    public HasCameraSlotInterface slot;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(initialCamera());
    }

    // Update is called once per frame
    void Update() {
        Movement();
    }

    public void setToSlot(HasCameraSlotInterface objectSlot) {
        slot = objectSlot;
    }


    protected IEnumerator initialCamera() {
        yield return new WaitUntil(() => slot != null);
        transform.position = slot.getCameraPosition();
        transform.LookAt(slot.getCameraTarget());
        positionCamera();
    }

    protected void positionCamera() {
        transform.position = slot.getCameraPosition();
        transform.LookAt(slot.getCameraTarget());
    }

    protected void Movement() {
        if (slot != null) {
            Vector3 cameraPosition = slot.getCameraPosition();

            if (cameraPosition != transform.position) {
                Vector3 currentPosition = transform.position;
                float transition = 1.5f * Time.deltaTime;
                Vector3 newPosition = Vector3.Lerp(currentPosition, cameraPosition, transition);

                float currentDistance = Vector3.Distance(newPosition, cameraPosition);
                if (currentDistance < 0.01f) {
                    newPosition = cameraPosition;
                }

                transform.position = newPosition;
            }
        }
    }

    //Amazon.fr – Service Retour Produits. 300, route de Pithiviers.Site des 3 Arches. 45962 Orléans Cedex 9.
}

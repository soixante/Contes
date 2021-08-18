using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject GalaxyPrefab;

    protected GameObject Galaxy;

    // Start is called before the first frame update
    void Start() {
        createGalaxy();
    }

    // Update is called once per frame
    void Update() {
        Vector3 cameraPosition = Camera.main.transform.localPosition;
        cameraPosition.x += Input.GetAxis("Horizontal") * Time.deltaTime * 20;
        cameraPosition.y += Input.GetAxis("Vertical") * Time.deltaTime * 20;
        Camera.main.transform.localPosition = cameraPosition;
    }

    protected void createGalaxy() {
        Galaxy = Instantiate(GalaxyPrefab);
    }
}

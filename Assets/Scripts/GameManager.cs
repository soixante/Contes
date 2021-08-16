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
    }

    protected void createGalaxy() {
        Galaxy = Instantiate(GalaxyPrefab);
    }
}

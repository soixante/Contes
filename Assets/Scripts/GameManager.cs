using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject GalaxyPrefab;
    public GameObject PlayerPrefab;
    public Canvas Hud;

    protected GameObject galaxy;
    protected GameObject player;

    // Start is called before the first frame update
    void Start() {
        createGalaxy();
        initPlayer();
        initCamera();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.name.Contains("star_")) {
                    player.GetComponent<Player>().currentStar = hit.transform.gameObject;
                    Camera.main.GetComponent<CameraObject>().objectToFocus = hit.transform.gameObject;
                }
            }
        }
    }

    protected void createGalaxy() {
        galaxy = Instantiate(GalaxyPrefab);
    }

    protected void initPlayer() {
        player = Instantiate(PlayerPrefab);
        assignStartingStarToPlayer();
    }

    protected void initCamera() {
        GameObject currentStarToFocus = player.GetComponent<Player>().currentStar;
        Camera.main.GetComponent<CameraObject>().objectToFocus = currentStarToFocus;
    }

    protected void assignStartingStarToPlayer() {
        GameObject currentStar = galaxy.GetComponent<Galaxy>().getRandomStar();

        player.GetComponent<Player>().currentStar = currentStar;
    }
}

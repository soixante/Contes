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
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            Camera.main.transform.localPosition = player.transform.position;
        }

        Vector3 cameraPosition = Camera.main.transform.localPosition;
        cameraPosition.x += Input.GetAxis("Horizontal") * Time.deltaTime * 20;
        cameraPosition.y += Input.GetAxis("Vertical") * Time.deltaTime * 20;
        Camera.main.transform.localPosition = cameraPosition;

        GameObject console = Hud.transform.GetChild(0).gameObject;
        console.GetComponent<Text>().text =
            $"currentStarCount: {galaxy.GetComponent<Galaxy>().currentStarCount}\n";
    }

    protected void createGalaxy() {
        galaxy = Instantiate(GalaxyPrefab);
    }

    protected void initPlayer() {
        player = Instantiate(PlayerPrefab);
        assignStartingStarToPlayer();
    }

    protected void assignStartingStarToPlayer() {
        GameObject currentStar = galaxy.GetComponent<Galaxy>().getRandomStar();

        player.GetComponent<Player>().currentStar = currentStar;
    }
}

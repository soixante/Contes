using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject GalaxyPrefab;
    public GameObject PlayerPrefab;
    public GameObject[] ShipPrefabs;
    public GameObject mainCamera;
    public float ZoomSpeedMouse = 35f;

    protected GameObject galaxy;
    protected GameObject player;
    protected GameObject currentFocusedStar;

    private static readonly float[] BoundsX = new float[] { -10f, 5f };
    private static readonly float[] BoundsZ = new float[] { -18f, -4f };
    private static readonly float[] ZoomBounds = new float[] { 10f, 85f };

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(createGalaxy());
        StartCoroutine(initPlayer());
        StartCoroutine(initCamera());
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        float offset = Input.GetAxis("Mouse ScrollWheel");
        if (offset != 0) {
            mainCamera.GetComponent<Camera>().fieldOfView = Mathf.Clamp(mainCamera.GetComponent<Camera>().fieldOfView - (offset * ZoomSpeedMouse), ZoomBounds[0], ZoomBounds[1]);
        }

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.GetComponent<Star>() != null) {
                    GameObject focusedStar = hit.transform.gameObject;

                    if (focusedStar == currentFocusedStar) {
                        // move player
                        mainCamera.GetComponent<CameraObject>().focus(player);
                        currentFocusedStar.GetComponent<Star>().setFocus(false);
                        player.GetComponent<Player>().currentStar = focusedStar;
                        mainCamera.GetComponent<CameraObject>().focus(player);
                        currentFocusedStar = null;
                    } else {
                        // get star info
                        focusedStar.GetComponent<Star>().setFocus(true);
                        currentFocusedStar = focusedStar;
                        mainCamera.GetComponent<CameraObject>().focus(focusedStar);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            mainCamera.GetComponent<CameraObject>().focus(player);
            currentFocusedStar.GetComponent<Star>().setFocus(false);
            currentFocusedStar = null;
        }
    }

    protected IEnumerator createGalaxy() {
        galaxy = Instantiate(GalaxyPrefab);
        yield return new WaitUntil(() => galaxy.GetComponent<Galaxy>().currentStarCount > 0);
    }

    protected IEnumerator initPlayer() {
        yield return new WaitUntil(() => galaxy.GetComponent<Galaxy>().currentStarCount > 0);
        player = Instantiate(PlayerPrefab);
        player.name = "soixante";

        int i = Random.Range(0, ShipPrefabs.Length);
        GameObject ship = Instantiate(ShipPrefabs[i]);
        ship.name = "soixantecopter";
        GameObject star = galaxy.GetComponent<Galaxy>().findStartingStar();

        Debug.Log($"setting star {star.name} to {player}");
        player.GetComponent<Player>().setCurrentStar(star);
        Debug.Log($"setting ship {ship.name} to {player}");
        player.GetComponent<Player>().setCurrentShip(ship);
    }

    protected IEnumerator initCamera() {
        yield return new WaitUntil(() => player != null);
        yield return new WaitUntil(() => player.GetComponent<Player>() != null);
        yield return new WaitUntil(() => player.GetComponent<Player>().currentStar != null);
        yield return new WaitUntil(() => player.GetComponent<Player>().ship != null);

        Debug.Log($"setting camera to {player} slot");
        mainCamera.GetComponent<CameraObject>().focus(player);
    }
}

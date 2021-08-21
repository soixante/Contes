using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject GalaxyPrefab;
    public GameObject PlayerPrefab;
    public GameObject[] ShipPrefabs;

    protected GameObject galaxy;
    protected GameObject player;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(createGalaxy());
        StartCoroutine(initPlayer());
        StartCoroutine(initCamera());
        //initPlayer();
        //initCamera();
    }

    // Update is called once per frame
    void Update() {
        //if (Input.GetMouseButtonDown(0)) {
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit)) {
        //        if (hit.transform.name.Contains("star_")) {
        //            player.GetComponent<Player>().currentStar = hit.transform.gameObject;
        //            Camera.main.GetComponent<CameraObject>().objectToFocus = hit.transform.gameObject;
        //        }
        //    }
        //}
    }

    protected IEnumerator createGalaxy() {
        galaxy = Instantiate(GalaxyPrefab);
        yield return new WaitUntil(() => galaxy.GetComponent<Galaxy>().currentStarCount > 0);
    }

    protected IEnumerator initPlayer() {
        yield return new WaitUntil(() => galaxy.GetComponent<Galaxy>().currentStarCount > 0);
        player = Instantiate(PlayerPrefab);

        int i = Random.Range(0, ShipPrefabs.Length);
        GameObject ship = Instantiate(ShipPrefabs[i]);
        GameObject star;
        star = galaxy.GetComponent<Galaxy>().findStartingStar();

        player.GetComponent<Player>().setCurrentStar(star);
        player.GetComponent<Player>().setCurrentShip(ship);
    }

    protected IEnumerator initCamera() {
        yield return new WaitUntil(() => player != null);
        yield return new WaitUntil(() => player.GetComponent<Player>() != null);
        yield return new WaitUntil(() => player.GetComponent<Player>().currentStar != null);
        yield return new WaitUntil(() => player.GetComponent<Player>().ship != null);

        Camera.main.GetComponent<CameraObject>().setObjectToFocus(player.GetComponent<Player>());
    }

    //protected void initCamera() {
    //    GameObject currentStarToFocus = player.GetComponent<Player>().currentStar;
    //    Camera.main.GetComponent<CameraObject>().objectToFocus = currentStarToFocus;
    //}

    //protected void assignStartingStarToPlayer() {
    //    GameObject currentStar = galaxy.GetComponent<Galaxy>().getRandomStar();

    //    player.GetComponent<Player>().currentStar = currentStar;
    //}
}

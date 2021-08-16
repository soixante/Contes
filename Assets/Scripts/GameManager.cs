using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GalaxyConstants {
    public const float SIZE_SMALL = 2.5f;
    public const float SIZE_MEDIUM = 5f;
    public const float SIZE_LARGE = 10f;
}

public class GameManager : MonoBehaviour
{
    public GameObject starGO;
    public int numberOfStar = 10;
    public float galaxySize = GalaxyConstants.SIZE_SMALL;
    public List<GameObject> InactiveStars = new List<GameObject>();
    public List<GameObject> ActiveStars = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        createGalaxy(GalaxyConstants);
    }

    // Update is called once per frame
    void Update()
    {
        this.galaxy.ForEach(checkStarToRemove);

        if (Input.GetKeyDown(KeyCode.R)) {
            sendDestroyGalaxyEvent();
            //createGalaxy();
        }
    }

    protected void createGalaxy() {

    }

    protected void createGalaxy() {
        for (int i = 0; i < this.numberOfStar; i++) {
            InactiveStars.Add(createNewStar("name"));
        }
    }

    protected void createGalaxy() {
        for (int i = 0; i < this.numberOfStar; i++) {
            this.galaxy.Add(createSolarSystem());
        }
    }

    protected void sendDestroyGalaxyEvent() {
        this.galaxy.ForEach(destroySolarSystem);
    }

    protected void checkStarToRemove(GameObject star) {
        if (star.GetComponent<StarGO>().toDestroy) {
            galaxy.Remove(star);
        }
    }

    protected void destroySolarSystem(GameObject star) {
        star.GetComponent<StarGO>().explode();
    }

    protected GameObject createSolarSystem() {
         return createNewStar($"sun");
    }

    protected GameObject createNewStar(string name) {
        float angle = Random.Range(0.0f, 2 * Mathf.PI);
        float dist = Random.Range(0.0f, this.galaxySize);

        float x = Mathf.Cos(angle) * dist;
        float z = Mathf.Sin(angle) * dist;

        Vector3 pos = transform.position + new Vector3(x, 0, z);
        float angleDegrees = -angle * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);

        GameObject newStar = Instantiate(this.starGO, pos, rot);

        return newStar;
    }
}

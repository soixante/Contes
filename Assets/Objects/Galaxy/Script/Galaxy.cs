using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy : MonoBehaviour {
    const int numberOfStars = 150;
    const float galaxyRadius = 100f;
    const float minimalStarDistance = 6.0f;
    const float maximalStarDistance = 24.0f;
    public int currentStarCount = 0;
    public GameObject[] StarPrefabs;

    protected GameObject[] ActiveStars = new GameObject[numberOfStars];

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(createSolarSystems());
    }

    // Update is called once per frame
    void Update() {
    }

    public GameObject findStartingStar() {
        int i = Random.Range(0, currentStarCount);
        return transform.Find($"system_{i}").gameObject;
    }

    protected IEnumerator createSolarSystems() {
        Vector3 currentPosition;
        GameObject createdStar;
        int failed = 0;
        int i;
        for (i = 0; i < numberOfStars; i++) {
            currentPosition = getRandomPosition();
            currentPosition.y = 0;
            createdStar = createStarAt(currentPosition, $"system_{i}", i == 0);
            yield return null;

            if (createdStar == null) {
                failed++;
                i--;
            } else {
                ActiveStars[i] = createdStar;
                failed = 0;
            }

            if (failed > numberOfStars) {
                Debug.Log("failed");
                break;
            }
        }

        currentStarCount = i;
    }
    
    protected GameObject createStarAt(Vector3 targetPosition, string name, bool first) {
        bool collision = true;
        bool isolated = false;

        Collider[] positionCollider = Physics.OverlapSphere(targetPosition, minimalStarDistance);
        if (positionCollider.Length == 0) {
            collision = false;
        }

        if (!first) {
            positionCollider = Physics.OverlapSphere(targetPosition, maximalStarDistance);
            if (positionCollider.Length == 0) {
                isolated = true;
            }
        }

        if (collision || isolated) {
            return null;
        }

        GameObject star = createRandomStar(); //+
        star.transform.position = targetPosition;
        star.name = name;
        
        return star;
    }

    protected GameObject createRandomStar() {
        int i = Random.Range(0, StarPrefabs.Length);
        return Instantiate(StarPrefabs[i], getInitialPosition(), getInitialRotation(), transform);
    }

    protected Vector3 getRandomPosition() {
        //return Random.insideUnitSphere* galaxyRadius;
        float angle = Random.Range(0.0f, 2 * Mathf.PI);
        float dist = Random.Range(0.0f, galaxyRadius);

        float x = Mathf.Cos(angle) * dist;
        float z = Mathf.Sin(angle) * dist;


        Vector3 pos = transform.position + new Vector3(x, 0, z);

        return pos;
    }

    protected Vector3 getInitialPosition() {
        return Vector3.zero;
    }

    protected Quaternion getInitialRotation() {
        Quaternion rot = Quaternion.Euler(0, 0, 0);

        return rot;
    }
}

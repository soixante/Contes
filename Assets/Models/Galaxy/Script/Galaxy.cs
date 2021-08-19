using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy : MonoBehaviour {
    const int numberOfStars = 100;
    const float galaxyRadius = 30f;
    public int currentStarCount = 0;

    public GameObject StarPrefab;
    protected GameObject initStar;
    protected Vector3 targetPosition;

    // Start is called before the first frame update
    void Start() {
        createStars();
    }

    // Update is called once per frame
    void Update() {
    }

    public GameObject getRandomStar() {
        //bool starFound = false;
        //bool failed = false;
        //int failedAttempt = 0;
        //while (!starFound && !failed) {
        //    int i = Random.Range(0, currentStarCount);
        //    Transform star = transform.Find($"star_{i}");
        //    if (star != null) {
        //        return star.gameObject;
        //    }
        //    failedAttempt++;
        //    if (failedAttempt > currentStarCount) {
        //        failed = true;
        //    }
        //}

        //Debug.Log("Fail finding random star");
        //return null;
        return createDefaultStar();
    }

    protected void destroyStars() {
        //for (int i = transform.childCount-1; i >= 0; i--) {
        //    Destroy(transform.GetChild(i).gameObject);
        //}
    }

    protected void createStars() {
        Vector3 currentPosition;
        int failed = 0;
        int i;
        for (i = 0; i < numberOfStars; i++) {
            currentPosition = getRandomPosition();
            currentPosition.y = 0;
            if (!createStarAt(currentPosition, $"star_{i}")) {
                failed++;
                i--;
            } else {
                failed = 0;
            }

            if (failed > numberOfStars) {
                Debug.Log("failed");
                break;
            }
        }

        currentStarCount = i;

    }

    protected bool createStarAt(Vector3 targetPosition, string name) {
        bool collision = true;
        bool isolated = false;

        GameObject star = createDefaultStar();

        star.GetComponent<Collider>().enabled = false;

        Collider[] positionCollider = Physics.OverlapSphere(targetPosition, 3.0f);
        if (positionCollider.Length == 0) {
            collision = false;
        }

        positionCollider = Physics.OverlapSphere(targetPosition, 6.0f);
        if (positionCollider.Length == 0) {
            isolated = true;
        }

        if (collision || isolated) {
            Destroy(star);
            return false;
        }

        star.transform.localPosition = targetPosition;
        star.GetComponent<Collider>().enabled = true;
        star.name = name;

        //star.GetComponent<Star>().targetPosition = targetPosition;

        return true;

    }

    protected GameObject createDefaultStar() {
        Vector3 pos = getInitialPosition();
        Quaternion rot = getInitialRotation();
        GameObject star = Instantiate(StarPrefab, pos, rot, transform);

        return star;
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
        return new Vector3(0, 0, 0);
    }

    protected Quaternion getInitialRotation() {
        Quaternion rot = Quaternion.Euler(0, 0, 0);

        return rot;
    }
}

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
        if (Input.GetKeyDown(KeyCode.Space)) {
            destroyStars();
            createStars();
        }
    }

    public GameObject getRandomStar() {
        return createDefaultStar();
    }

    protected void destroyStars() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    protected void createStars() {
        Vector3 currentPosition;
        int failed = 0;
        for (int i = 0; i < numberOfStars; i++) {
            currentPosition = getRandomPosition();
            currentPosition.y = 0;
            if (!createStarAt(currentPosition)) {
                failed++;
                i--;
            } else {
                failed = 0;
            }

            if (failed > numberOfStars) {
                Debug.Log("failed");
                break;
            }

            currentStarCount = i;
        }

    }

    protected bool createStarAt(Vector3 targetPosition) {
        bool collision = true;
        bool isolated = false;

        Collider[] positionCollider = Physics.OverlapSphere(targetPosition, 2.0f);
        if (positionCollider.Length == 0) {
            collision = false;
        }

        positionCollider = Physics.OverlapSphere(targetPosition, 6.0f);
        if (positionCollider.Length == 0) {
            isolated = true;
        }

        if (collision || isolated) {
            return false;
        }

        GameObject star = createDefaultStar();
        star.transform.localPosition = targetPosition;
        //star.GetComponent<Collider>().enabled = false;
        //star.GetComponent<Collider>().enabled = true;
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

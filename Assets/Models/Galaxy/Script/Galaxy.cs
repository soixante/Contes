using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy : MonoBehaviour {
    const int numberOfStars = 30;
    const float galaxyRadius = 10f;
    protected int gigou = 0;
    public Material materia = null;


    public GameObject StarPrefab;

    // Start is called before the first frame update
    void Start() {
        createStars();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            createStar();
        }
    }

    protected void createStars() {
        int failed = 0;
        int eternali = 0;
        for (int i = 0; i < numberOfStars; i++) {
            Vector3 pos = getInitialPosition();
            Vector3 newPos = getRandomPosition();
            Quaternion rot = getInitialRotation();

            GameObject star = Instantiate(StarPrefab, pos, rot);
            star.name = $"star{eternali}";
            star.transform.position = newPos;
            star.GetComponent<SphereCollider>().radius = 1.5f;
            Collider[] positionCollider = Physics.OverlapSphere(newPos, 5.0f);

            if (positionCollider.Length == 0) {
                failed = 0;
            } else {
                Destroy(star);
                i--;
                failed++;
            }

            if (failed > numberOfStars) {
                Debug.Log("failed");
                break;
            }
            eternali++;
        }
    }

    protected void createStar() {
        Vector3 newPos = getRandomPosition();
        GameObject star = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        star.transform.position = newPos;
        star.name = $"star{gigou}";
        Collider[] positionCollider = Physics.OverlapSphere(newPos, 3.0f);

        if (positionCollider.Length > 1) {
            Debug.Log($"currentObject : {star.name}, collision object : {positionCollider[0].gameObject.name}");
            Destroy(star);
        }
        gigou++;
    }

    protected Vector3 getRandomPosition() {
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

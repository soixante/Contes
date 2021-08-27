using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Galaxy : MonoBehaviour {
    const int numberOfStars = 150;
    const float galaxyRadius = 100f;
    const float minimalStarDistance = 6.0f;
    const float maximalStarDistance = 24.0f;
    public int currentStarCount = 0;
    public GameObject[] StarPrefabs;
    public GameObject StarLabelPrefab;

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
        return ActiveStars[i];
        //return transform.Find($"system_{i}").gameObject;
    }

    protected IEnumerator createSolarSystems() {
        Vector3 currentPosition;
        GameObject createdStar;
        GameObject createdLabel;
        int failed = 0;
        int i;
        for (i = 0; i < numberOfStars; i++) {
            string starname = generateRandomStarname();
            currentPosition = getRandomPosition();
            currentPosition.y = 0;
            createdStar = createStarAt(currentPosition, starname, i==0);
            yield return null;

            if (createdStar == null) {
                failed++;
                i--;
            } else {
                ActiveStars[i] = createdStar;
                createdLabel = createLabelAt(currentPosition, starname);
                createdStar.GetComponent<Star>().setLabel(createdLabel);
                yield return null;
                failed = 0;
            }

            if (failed > numberOfStars) {
                Debug.Log("failed");
                break;
            }
        }

        currentStarCount = i;
    }

    protected GameObject createLabelAt(Vector3 currentStarPosition, string starname) {
        Vector3 pos = currentStarPosition;
        pos.z = currentStarPosition.z - 1.5f;
        Vector3 scale = new Vector3 (0.025f, 0.025f, 0.025f);
        Quaternion rot = Quaternion.Euler(90, 0, 0);
        GameObject createdStarlabel =  Instantiate(StarLabelPrefab, pos, rot , transform);
        createdStarlabel.transform.localScale = scale;
        RectTransform rt = createdStarlabel.GetComponent<RectTransform>();
        createdStarlabel.GetComponent<Canvas>().worldCamera = Camera.main;


        rt.sizeDelta = new Vector2(300, 5);

        TextMeshProUGUI tmp = createdStarlabel.GetComponent<TextMeshProUGUI>();
        tmp.text = starname;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontSize = 22;
        tmp.characterSpacing = 8;

        createdStarlabel.name = $"label_{starname}";

        return createdStarlabel;
    }
         

    protected string generateRandomStarname() {
        string randomPick = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        string[] randomName = {
            "YEANU",
            "YOSEFU",
            "ALICE",
            "CLEMENTINE",
            "ANNIE",
            "ELEONORE",
            "EMILIENNE",
            "ELEONORE",
            "CHAPITRE",
            "FELICIEN",
            "JULIEN",
            "GASPARD",
            "VIOLETTE",
            "SUZANNE",
            "FLORENT",
            "LÉONIE",
            "CHRISTOPHE",
            "JACQUES"
            };

        int i = Random.Range(2, 3);
        string code = "";
        for (int j = 0; j<i; j++) {
            int k = Random.Range(26, randomPick.Length);
            code += randomPick[k];
        }
        int l = Random.Range(0, 26);
        int m = Random.Range(0, randomName.Length);

        return $"{randomName[m]}-{code}-{randomPick[l]}";
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

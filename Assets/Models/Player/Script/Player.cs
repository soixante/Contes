using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject currentStar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetStarSlotPosition = currentStar.transform.GetChild(0).transform.position;
        targetStarSlotPosition.y += 0.6f;
        targetStarSlotPosition.x += 0.6f;
        targetStarSlotPosition.z += 0.6f;

        if (targetStarSlotPosition != transform.position) {
            Vector3 currentPosition = transform.position;
            float transition = 1.5f * Time.deltaTime;
            Vector3 newPosition = Vector3.Lerp(currentPosition, targetStarSlotPosition, transition);

            float currentDistance = Vector3.Distance(newPosition, targetStarSlotPosition);
            if (currentDistance < 0.02f) {
                newPosition = targetStarSlotPosition;
            }

            transform.position = newPosition;
        }
    }
}

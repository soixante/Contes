using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HasCameraSlotInterface 
{
    Vector3 getCameraPosition();
    Vector3 getCameraTarget();
}

public interface HasPlayerSlotInterface {
    Vector3 getPlayerPosition();
}

public interface HasShipSlotInterface {
    Vector3 getShipPosition();
}

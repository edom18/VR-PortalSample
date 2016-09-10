using UnityEngine;
using System.Collections;

public class PortalManager : MonoBehaviour {

    public GameObject[] RenderTextureCameras;
    public GameObject[] Portals;

    int _portalIndex;
	
	// Update is called once per frame
	void Update () {
        _portalIndex = 0;

        foreach(var camera in RenderTextureCameras) {
            camera.transform.localPosition = transform.position - Portals[_portalIndex].transform.position;
            camera.transform.localRotation = transform.localRotation;
            _portalIndex++;
        }
	}
}

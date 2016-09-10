using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public Transform CameraRig;

    float _speed = 0.05f;

	void Update () {
        if (Input.GetKey(KeyCode.W)) {
            var pos = transform.position;
            pos += CameraRig.forward * _speed;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.S)) {
            var pos = transform.position;
            pos -= CameraRig.forward * _speed;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.D)) {
            var pos = transform.position;
            pos += CameraRig.right * _speed;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.A)) {
            var pos = transform.position;
            pos -= CameraRig.right * _speed;
            transform.position = pos;
        }
	}
}

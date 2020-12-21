using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] private Transform target = null;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    void FixedUpdate() {
        if(target == null) {
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}

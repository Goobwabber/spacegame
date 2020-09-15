using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    // Settings
	public Transform target;
    public float distance = 100.0f;
    public float distanceSensorRadius = 10f;
    public float smoothness = 2f;
    public LayerMask selectionLayers;
    public float selectionRadius = 75;
    public Material selectionMaterial;
    public Object personPrefab;

    // Speed Multipliers
    public float xSpeedMultiplier = 0.2f;
    public float ySpeedMultiplier = 20.0f;
    public float zoomMultiplier = 20f;

    // Limits
    public float maxYAngle = 90f;
    public float minYAngle = -90f;
    public float maxDistance = 500f;
    public float minDistance = 20f;
    public float distanceRadius = 10f;

    // Camera Orbit Data
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;

    // Cursor Hover Data
    Renderer hoverTile;
    Vector3 hoverPos;
    bool hoveredTile = false;

    // Use this for initialization
    void Start() {
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;

        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>()){
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void Update() {
        if(hoveredTile) {
            hoverTile.materials = new Material[] { hoverTile.materials[0] };
            hoveredTile = false;
        }
    }

    void LateUpdate() {
        //Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        // if (Input.GetMouseButtonDown(0)) {
        //     Instantiate(personPrefab, fromRotation * new Vector3(0.0f, 0.0f, -3f) + hit.point, fromRotation);
        // }

        cursorRayCast();
        zoomDistanceCast();
        updateCamera();

        if (Input.GetMouseButtonDown(0) && hoveredTile) {
            Vector3 direction = (target.position - transform.position).normalized;
            Instantiate(personPrefab, -direction*2f + hoverPos, Quaternion.LookRotation(transform.up, transform.forward));
        }
    }

    private void cursorRayCast() {
        Ray ray = GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 50, selectionLayers)) {
            hoveredTile = true;
            hoverPos = hit.point;

            hoverTile = hit.transform.gameObject.GetComponent<Renderer>();
            hoverTile.materials = new Material[] {
                hoverTile.material,
                selectionMaterial
            };
        }
    }

    private void zoomDistanceCast() {
        RaycastHit hit;
        Vector3 direction = (target.position - transform.position).normalized;
        if(Physics.SphereCast(transform.position + (direction * distanceSensorRadius), distanceSensorRadius, direction, out hit, 1000, selectionLayers)) {
            distanceRadius = Vector3.Distance(hit.point, target.position);
        }
    }

    private void updateCamera() {
        if (target) {
            if (Input.GetMouseButton(1)) {
                velocityX += xSpeedMultiplier * Input.GetAxis("Mouse X") * distance * 0.02f;
                velocityY += ySpeedMultiplier * Input.GetAxis("Mouse Y") * 0.02f;
            }

            rotationYAxis += velocityX;
            rotationXAxis -= velocityY;
            rotationXAxis = ClampAngle(rotationXAxis, minYAngle, maxYAngle);
            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;
 
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * zoomMultiplier, minDistance + distanceRadius, maxDistance + distanceRadius);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;
 
            transform.rotation = rotation;
            transform.position = position;

            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothness);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothness);
        }
    }

    public static float ClampAngle(float angle, float min, float max) {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}

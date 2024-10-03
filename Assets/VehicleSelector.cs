using System.Collections;
using UnityEngine;

public class VehicleSelector : MonoBehaviour {
    private Camera mainCamera;
    private GameObject selectedVehicle;
    private Color originalColor;

    public Color highlightColor = Color.yellow;
    public float minX = -5f, maxX = 5f, minY = -5f, maxY = 5f;
    public float moveSpeed = 5f; // Velocidade de movimento

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
        HandleSelection();
    }

    private void HandleSelection() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.CompareTag("Vehicle")) {
                ResetSelectedVehicle();
                SelectVehicle(hit.collider.gameObject);
            }
        }

        MoveSelectedVehicle();
    }

    private void SelectVehicle(GameObject vehicle) {
        selectedVehicle = vehicle;
        originalColor = vehicle.GetComponent<SpriteRenderer>().color;
        vehicle.GetComponent<SpriteRenderer>().color = highlightColor;
    }

    private void ResetSelectedVehicle() {
        if (selectedVehicle != null) {
            selectedVehicle.GetComponent<SpriteRenderer>().color = originalColor;
            selectedVehicle = null;
        }
    }

    private void MoveSelectedVehicle() {
        if (selectedVehicle == null) return;

        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.RightArrow)) moveDirection = Vector3.right;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) moveDirection = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.UpArrow)) moveDirection = Vector3.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) moveDirection = Vector3.down;

        if (moveDirection != Vector3.zero) {
            // Restrição de movimento baseada na orientação
            float angle = selectedVehicle.transform.rotation.eulerAngles.z;
            if ((angle >= 45 && angle < 135) || (angle >= 225 && angle < 315)) moveDirection.y = 0; // Horizontal
            else moveDirection.x = 0; // Vertical

            // Inicia a corrotina para mover o veículo
            StartCoroutine(MoveVehicle(selectedVehicle.transform.position + moveDirection));
        }
    }

    private IEnumerator MoveVehicle(Vector3 targetPosition) {
        Vector3 startPosition = selectedVehicle.transform.position;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (Vector3.Distance(selectedVehicle.transform.position, targetPosition) > 0.01f) {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            selectedVehicle.transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null; // Espera até o próximo frame
        }

        // Garante que o veículo chegue exatamente à posição alvo
        selectedVehicle.transform.position = targetPosition;
    }
}

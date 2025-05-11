using UnityEngine;
using UnityEngine.InputSystem;

public class FrogTongue : MonoBehaviour
{
    public float tongueSpeed = 10f;
    public float retractionSpeed = 15f;
    public float maxTongueLength = 5f;
    public LayerMask collisionLayers;
    public Transform mouthPosition;
    public Material tongueMaterial;
    public int tongueSegments = 20;
    public float tipWidth = 0.2f; // Adjust the width of the tip

    private LineRenderer lineRenderer;
    private bool isExtending = false;
    private bool isRetracting = false;
    private Vector3 targetPosition;
    private float currentTongueLength = 0f;
    private bool toShoot = false;

    [SerializeField] private AudioSource LickAudio;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component is missing!");
            return;
        }

        lineRenderer.positionCount = 0;  // Start with no points
        lineRenderer.material = tongueMaterial;

        Debug.Log("LineRenderer initialized.");
    }

    public void OnShoot(InputAction.CallbackContext callbackContext)
    {
        if(GameManager.instance.ShowUI == false)
        {
            return;
        }
        if (callbackContext.started && GameManager.instance.GameAudio>0)
        {
            toShoot = true;
        }
    }
    void Update()
    {
        if (toShoot) // Assuming "Fire1" is mapped to the gamepad button
        {
            toShoot = false;
            if (GameManager.instance.isGameActive && !isExtending && !isRetracting)
            {
                LickAudio.Play();
                StartExtending();
            }
        }

        if (isExtending)
        {
            ExtendTongue();
        }
        else if (isRetracting)
        {
            RetractTongue();
        }
    }

    void StartExtending()
    {
        isExtending = true;
        isRetracting = false;
        currentTongueLength = 0f;
        targetPosition = mouthPosition.position + mouthPosition.forward * maxTongueLength;
        
        // Set initial points for the Line Renderer
        lineRenderer.positionCount = tongueSegments;
        for (int i = 0; i < tongueSegments; i++)
        {
            lineRenderer.SetPosition(i, mouthPosition.position);
        }

        Debug.Log("Started Extending");
    }

    void ExtendTongue()
    {
        if (currentTongueLength < maxTongueLength)
        {
            float deltaLength = tongueSpeed * Time.deltaTime;
            currentTongueLength = Mathf.Min(currentTongueLength + deltaLength, maxTongueLength);

            UpdateTonguePositions(currentTongueLength);

            Vector3 endPosition = lineRenderer.GetPosition(tongueSegments - 1);
            RaycastHit hit;
            if (Physics.Raycast(lineRenderer.GetPosition(0), endPosition - lineRenderer.GetPosition(0), out hit, currentTongueLength, collisionLayers))
            {
                currentTongueLength = hit.distance;
                UpdateTonguePositions(currentTongueLength);
                isExtending = false;
                isRetracting = true;
                Debug.Log("Hit an object");
            }
            if (hit.collider)
            {
                if (hit.collider.gameObject.CompareTag("Fly"))
                {
                    Destroy(hit.collider.gameObject);
                    print(hit.collider.gameObject.name);
                    int scoreToAdd = hit.collider.gameObject.name.Contains("Plain_Fly") ? 1 : hit.collider.gameObject.name.Contains("Plus3") ? 3 : 5;
                    GameManager.instance.AddScore(gameObject.name, scoreToAdd);
                    GameManager.instance.TakenPlaces.Remove(int.Parse(hit.collider.gameObject.transform.parent.name));
                    GameManager.instance.currentNumOfFlies--;
                }
                else if (hit.collider.gameObject.CompareTag("Player"))
                {
                    GameManager.instance.ResetPlayer(hit.collider.gameObject.name);
                }
                currentTongueLength = Vector3.Distance(lineRenderer.GetPosition(0), hit.point);
                UpdateTonguePositions(currentTongueLength);
                isExtending = false;
                isRetracting = true;
                Debug.Log("Hit an object");
            }
        }
        else
        {
            isExtending = false;
            isRetracting = true;
            Debug.Log("Max length reached");
        }
    }

    void RetractTongue()
    {
        if (currentTongueLength > 0)
        {
            float deltaLength = retractionSpeed * Time.deltaTime;
            currentTongueLength = Mathf.Max(currentTongueLength - deltaLength, 0);

            UpdateTonguePositions(currentTongueLength);
        }
        else
        {
            isRetracting = false;
            ResetTongue();
        }
    }

    void ResetTongue()
    {
        lineRenderer.positionCount = 0; // Make the tongue disappear
        isExtending = false;
        isRetracting = false;
        Debug.Log("Tongue reset");
    }

    void UpdateTonguePositions(float length)
    {
        Vector3 direction = (targetPosition - mouthPosition.position).normalized;
        for (int i = 0; i < tongueSegments; i++)
        {
            float t = (float)i / (tongueSegments - 1);
            float segmentLength = t * length;
            Vector3 basePosition = mouthPosition.position + direction * segmentLength;
            lineRenderer.SetPosition(i, basePosition);

            
        }
    }
}

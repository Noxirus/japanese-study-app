using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineDrawer : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public GameObject linePrefab; // The prefab for the line object
    private GameObject currentLine; // The currently drawn line
    private LineRenderer lineRenderer; // The LineRenderer component of the current line
    private Camera canvasCamera; // The camera used to render the canvas
    private Vector2 referenceResolution; // The reference resolution of the canvas

    private void Start()
    {
        // Instantiate a new line object from the prefab
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();

        // Get the camera used to render the canvas
        canvasCamera = GetComponent<Canvas>().worldCamera;

        // Get the reference resolution of the canvas
        referenceResolution = GetComponent<CanvasScaler>().referenceResolution;

        lineRenderer.sortingOrder = GetComponent<Canvas>().sortingOrder + 1;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Convert the screen position to canvas space
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), eventData.position, canvasCamera, out canvasPosition);

        // Normalize the canvas position relative to the reference resolution
        canvasPosition /= referenceResolution;

        Debug.Log("Canvas Position: " +  canvasPosition);
        Debug.Log("Event Pos: " + eventData.position);

        // Start drawing a new line
        lineRenderer.positionCount = 0;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, eventData.position);
        lineRenderer.SetPosition(1, eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert the screen position to canvas space
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), eventData.position, canvasCamera, out canvasPosition);

        // Normalize the canvas position relative to the reference resolution
        canvasPosition /= referenceResolution;

        // Add the current cursor position as a new point in the line
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, eventData.position);
    }
}
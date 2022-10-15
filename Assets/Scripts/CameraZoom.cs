using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    // main camera
    [SerializeField]
    public Camera cam;
    // zoom center, focus on this object! (Should be character position la)
    [SerializeField]
    public Vector3 target;

    [SerializeField]
    public float zoomInSize;
    [SerializeField]
    public float zoomOutSize;
    [SerializeField]
    public float zoomSpeed;
    [SerializeField]
    public bool isZoomActive;
    // Do you want to zoom in or out ?
    [SerializeField]
    public bool isZoomIn;

    private void Start()
    {
        cam = Camera.main;
        // target focused on main cam center
        target = new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y,
            -10
        );
    }

    public void LateUpdate()
    {
        if (isZoomActive) 
        {
            // perform zoom process
            if (isZoomIn)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomInSize, zoomSpeed);
                cam.transform.position = Vector3.Lerp(cam.transform.position, target, zoomSpeed);
            }
            else 
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomOutSize, zoomSpeed);
                cam.transform.position = Vector3.Lerp(cam.transform.position, target, zoomSpeed);
            }

            // identify if zoom should end
            if (isZoomIn)
            {
                // if zoom in, cam size near zoomIn size then, stop la
                isZoomActive = !(Mathf.Abs(cam.orthographicSize - zoomInSize) < 1);
            }
            else
            {
                // if zoom out, cam size near zoomOut size then, stop la
                isZoomActive = !(Mathf.Abs(cam.orthographicSize - zoomOutSize) < 1);
            }
        }
    }
}

using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;

    [SerializeField] private ParallaxLayer[] backgroundLayer;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        float currentCameraPositionX  = mainCamera.transform.position.x;
        float distance = currentCameraPositionX - lastCameraPositionX;
        lastCameraPositionX = currentCameraPositionX;

        foreach (ParallaxLayer layer in backgroundLayer)
        {
            layer.Move(distance);
        }
    }
}

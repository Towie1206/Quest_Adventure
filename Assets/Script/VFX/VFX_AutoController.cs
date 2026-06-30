using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1;
    [Space]
    [SerializeField] private bool randomOffset = true;
    [SerializeField] private bool randomRotation = true;

    [Header("Random Rotation")]
    [SerializeField] private float minRotation = 0;
    [SerializeField] private float maxRotation = 360;

    [Header("Random Position")]
    [SerializeField] private float xMinOffset = -.3f;
    [SerializeField] private float xMaxOffset = .3f;
    [Space]
    [SerializeField] private float yMinOffset = -.3f;
    [SerializeField] private float yMaxOffset = .3f;


    private void Start()
    {
        ApplyRamdomOffset();
        ApplyRamdomRotation();

        if(autoDestroy)
            Destroy(gameObject, destroyDelay);
    }
    private void ApplyRamdomOffset()
    {
        if (!randomOffset)
            return;

        float xOffset = Random.Range(xMinOffset, xMaxOffset);
        float yOffset = Random.Range(yMinOffset, yMaxOffset);

        transform.position = transform.position + new Vector3(xOffset, yOffset, 0);
    }
    private void ApplyRamdomRotation()
    {
        if (!randomRotation)
            return;

        float zRotation = Random.Range(minRotation, maxRotation);

        transform.position = transform.position + new Vector3(0, 0, zRotation);
    }
}

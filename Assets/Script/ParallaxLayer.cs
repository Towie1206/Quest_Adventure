using UnityEngine;

[System.Serializable]
public class ParallaxLayer 
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMutiplier; // dùng để x % với tốc độ camera kiểu như di chuyển theo camera với quãng đường ngắn hơn
    [SerializeField] private float imageWidthOffset = 10;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }

    public void Move(float distanceToMove)
    {
        background.position += Vector3.right * (distanceToMove * parallaxMutiplier); //new Vector3(distanceToMove * parallaxMutiplier, 0, 0);
    }
    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)
    {
        float imageRightEdge = (background.position.x + imageHalfWidth) - imageWidthOffset;
        float imageLeftEdge = (background.position.x - imageHalfWidth) + imageWidthOffset;

        if (imageRightEdge < cameraLeftEdge)
        {
            background.position += Vector3.right * (imageFullWidth);
        }
        else if (imageRightEdge > cameraRightEdge)
        {
            background.position += Vector3.right * (-imageFullWidth);
        }
    }
}

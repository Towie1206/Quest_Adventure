using UnityEngine;

[System.Serializable]
public class ParallaxLayer 
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMutiplier; // dùng để x % với tốc độ camera kiểu như di chuyển theo camera với quãng đường ngắn hơn

    public void Move(float distanceToMove)
    {
        background.position += Vector3.right * (distanceToMove * parallaxMutiplier); //new Vector3(distanceToMove * parallaxMutiplier, 0, 0);
    }
}

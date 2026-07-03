using UnityEngine;
using UnityEngine.UI;

public class UI_TreeConnection : MonoBehaviour
{
    [SerializeField] private RectTransform rotationPoint; // điểm xoay của dây nối canva enmty
    [SerializeField] private RectTransform connectionLength; // chiều dài của dây nối canva image
    [SerializeField] private RectTransform childNodeConnectionPoint; // điểm kết nối của node con

    public void DirectConnection(NodeDirectionType direction, float length, float offset)
    {
        bool shouldBeActice = direction != NodeDirectionType.None; // có hướng thì có thể active
        float finalLength = shouldBeActice ? length : 0; // trên true thì có chiều dài, false thì 0
        float angle = GetDirectionAngle(direction);

        rotationPoint.localRotation = Quaternion.Euler(0,0,angle + offset); // offset là xoay thêm phải là xoay Z not Y
        connectionLength.sizeDelta = new Vector2(finalLength, connectionLength.sizeDelta.y);
    }

    public Image GetConnectionImage() => connectionLength.GetComponent<Image>();

    public Vector2 GetConnectionPoint(RectTransform rect)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                rect.parent as RectTransform,
                childNodeConnectionPoint.position,
                null,
                out var localPosition
            );

        return localPosition;
    }

    private float GetDirectionAngle(NodeDirectionType type)
    {
        switch (type)
        {
            case NodeDirectionType.UpLeft: return 135f;
            case NodeDirectionType.Up: return 90f;
            case NodeDirectionType.UpRight: return 45f;
            case NodeDirectionType.Left: return 180f;
            case NodeDirectionType.Right: return 0f;
            case NodeDirectionType.DownLeft: return -135f;
            case NodeDirectionType.Down: return -90f;
            case NodeDirectionType.DownRight: return -45f;
            default: return 0f;
        }
    }
}

    public enum NodeDirectionType
    {
        None,
        UpLeft,
        Up,
        UpRight,
        Left,
        Right,
        DownLeft,
        Down,
        DownRight

    }
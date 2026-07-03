using UnityEngine;

public class UI_ToolTip : MonoBehaviour // chỉ show cái khung còn detail thì override lại class này
{
    private RectTransform rect;
    [SerializeField] private Vector2 offset = new Vector2(300, 20);// độ lệch của tooltip so với node
    protected virtual void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public virtual void ShowToolTip(bool show, RectTransform targetRect) // targetRect là vị trí của node mà tooltip sẽ hiển thị
    {
        if (!show)
        {
            rect.position = new Vector2(9999, 9999);
            return;
        }

        UpdatePosition(targetRect);
    }
    private void UpdatePosition(RectTransform targetRect) // vị trí tree node
    {
        float screenCenterX = Screen.width / 2f;
        float screenTop = Screen.height; 
        float screenBottom = 0f;

        Vector2 targetPosition = targetRect.position;
        //logic : chia màn hình làm 2 nửa, if node in left then tooltip in right and vice versa(ngược lại)
        targetPosition.x = targetPosition.x > screenCenterX ? targetPosition.x - offset.x : targetPosition.x + offset.x;// bigger mean in right else in left

        float verticalHalf = rect.sizeDelta.y / 2f; // nửa chiều cao của tooltip
        float topY = targetPosition.y + verticalHalf; // tooltip top + độ cao hiện tại của node
        float bottomY = targetPosition.y - verticalHalf; // tooltip bottom - độ cao hiện tại của node

        //logic: nếu top > screen top thì tooltip sẽ bị đẩy xuống, nếu mà bottom < screen bottom thì tooltip sẽ bị đẩy lên
        if (topY > screenTop)
            targetPosition.y = screenTop - verticalHalf - offset.y;
        else if(bottomY < screenBottom)
            targetPosition.y = screenBottom + verticalHalf + offset.y;

            rect.position = targetPosition;
    }
    protected virtual string GetColoredText(string color, string text)
    {
        return $"<color={color}>{text}.</color>";
    }
}

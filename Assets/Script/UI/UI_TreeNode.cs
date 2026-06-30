using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
{
    [SerializeField] private Image skillIcon;
    [SerializeField] private string lockColorHex = "#9F9797";
    private Color lastColor;
    public bool isUnlocked;
    public bool isLocked;

    private void Awake()
    {
        UpdateItemColor(GetColorByHex(lockColorHex));
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateItemColor(Color.white);
    }

    private bool CanBeUnlock()
    {
        if (isLocked || isUnlocked)
            return false;

        return true;
    }

    private void UpdateItemColor(Color color)
    {
        if (skillIcon == null)
            return;

        lastColor = skillIcon.color;
        skillIcon.color = color;
    }

    public void OnPointerDown(PointerEventData eventData) // khi ấn con trỏ chuột vào 
    {
        if(CanBeUnlock())
            Unlock();
    }

    public void OnPointerEnter(PointerEventData eventData) // khi di con trỏ chuột vào
    {
        if (!isUnlocked)
            UpdateItemColor(Color.white * .9f);
    }

    public void OnPointerExit(PointerEventData eventData) // khi bỏ con trỏ chuột ra 
    {
        if(!isUnlocked)
            UpdateItemColor(lastColor);
    }

    private Color GetColorByHex(string hexNumber)
    {
        ColorUtility.TryParseHtmlString(hexNumber, out Color color);

        return color;
    }
}

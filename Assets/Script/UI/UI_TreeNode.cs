using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private UI ui;
    private RectTransform rect;
    private UI_SkillTree skillTree;
    private UI_TreeConnectHandler connectHandler;

    [Header("Unlock Details")]
    public UI_TreeNode[] neededNodes;
    public UI_TreeNode[] conflictNodes;
    public bool isUnlocked;
    public bool isLocked;

    [Header("Skill Details")]
    public Skill_DataSO skillData;
    [SerializeField] private string skillName;
    [SerializeField] private Image skillIcon;
    [SerializeField] private int skillCost;
    [SerializeField] private string lockColorHex = "#9F9797";
    private Color lastColor;


    private void Awake()
    {
        ui = GetComponentInParent<UI>();

        rect = GetComponent<RectTransform>();

        skillTree = GetComponentInParent<UI_SkillTree>();

        connectHandler = GetComponent<UI_TreeConnectHandler>();

        UpdateIconColor(GetColorByHex(lockColorHex));
    }

    public void Refund()
    {
        isUnlocked = false;
        isLocked = false;

        UpdateIconColor(GetColorByHex(lockColorHex));

        connectHandler.UnlockedConnectionImage(false);

        skillTree.AddSkillPoints(skillData.cost);
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
        LockConflictNodes();

        skillTree.RemoveSkillPoints(skillData.cost);
        connectHandler.UnlockedConnectionImage(true);

        skillTree.skillManager.GetSkillByType(skillData.skilltype).SetSkillUpgrade(skillData.upgradeType);
    }

    private bool CanBeUnlock()
    {
        if (isLocked || isUnlocked)
            return false;

        if (!skillTree.EnoughSkillPoints(skillData.cost))
            return false;

        foreach (var node in neededNodes) // nếu bất kì node nào trong neededNodes chưa được mở khóa thì không thể mở khóa node hiện tại
        {
            if (!node.isUnlocked)
                return false;
        }

        foreach (var node in conflictNodes) // nếu bất kì node nào trong conflictNodes đã được mở khóa thì không thể mở khóa node hiện tại
        {
            if (node.isUnlocked)
                return false;
        }

        return true;
    }

    private void LockConflictNodes()
    {
        foreach (var node in conflictNodes)
        {
            node.isLocked = true;
            node.UpdateIconColor(GetColorByHex(lockColorHex));
        }
    }

    private void UpdateIconColor(Color color)
    {
        if (skillIcon == null)
            return;

        lastColor = skillIcon.color;
        skillIcon.color = color;
    }

    public void OnPointerDown(PointerEventData eventData) // khi ấn con trỏ chuột vào 
    {
        if (CanBeUnlock())
            Unlock();
        else if (isLocked)
            ui.skillToolTip.LockedSkilLEffect();
    }

    public void OnPointerEnter(PointerEventData eventData) // khi di con trỏ chuột vào
    {
        ui.skillToolTip.ShowToolTip(true, rect, this);

        if (isUnlocked || isLocked)
            return;

        ToggleNodeHighlight(true);
    }

    public void OnPointerExit(PointerEventData eventData) // khi bỏ con trỏ chuột ra 
    {
        ui.skillToolTip.ShowToolTip(false, rect);

        if (isUnlocked || isLocked)
            return;

        ToggleNodeHighlight(false);

    }

    private void ToggleNodeHighlight(bool highlight)
    {
        Color highlightColor = Color.white * .9f; highlightColor.a = 1f; // màu trắng mờ hơn một chút và alpha = 1f để không bị trong suốt
        Color colorToApply = highlight ? highlightColor : lastColor;

        UpdateIconColor(colorToApply);
    }

    private Color GetColorByHex(string hexNumber)
    {
        ColorUtility.TryParseHtmlString(hexNumber, out Color color);

        return color;
    }

    private void OnDisable()
    {
        if (isLocked)
            UpdateIconColor(GetColorByHex(lockColorHex));

        if (isUnlocked)
            UpdateIconColor(Color.white);
    }

    private void OnValidate()
    {
        if (skillData == null)
            return;

        skillName = skillData.displayName;
        skillIcon.sprite = skillData.icon;
        skillCost = skillData.cost;
        gameObject.name = "UI_TreeNode - " + skillData.displayName;
    }
}

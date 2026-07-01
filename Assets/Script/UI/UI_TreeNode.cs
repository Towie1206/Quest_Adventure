using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
{
    private UI ui;
    private RectTransform rect;
    private UI_SkillTree skillTree;

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

        UpdateItemColor(GetColorByHex(lockColorHex));
    }

    private void Unlock()
    {
        isUnlocked = true;
        skillTree.RemoveSkillPoints(skillData.cost);
        UpdateItemColor(Color.white);
        LockConflictNodes();
    }

    private bool CanBeUnlock()
    {
        if (isLocked || isUnlocked)
            return false;

        if(!skillTree.EnoughSkillPoints(skillData.cost))
            return false;

        foreach (var node in neededNodes) // nếu bất kì node nào trong neededNodes chưa được mở khóa thì không thể mở khóa node hiện tại
        {
            if(!node.isUnlocked)
                return false;
        }

        foreach(var node in conflictNodes) // nếu bất kì node nào trong conflictNodes đã được mở khóa thì không thể mở khóa node hiện tại
        {
            if(node.isUnlocked)
                return false;
        }

        return true;
    }

    private void LockConflictNodes()
    {
        foreach(var node in conflictNodes)
        {
            node.isLocked = true;
            node.UpdateItemColor(GetColorByHex(lockColorHex));
        }
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
        ui.skillToolTip.ShowToolTip(true, rect , this);

        if (!isUnlocked)
            UpdateItemColor(Color.white * .9f);
    }

    public void OnPointerExit(PointerEventData eventData) // khi bỏ con trỏ chuột ra 
    {
        ui.skillToolTip.ShowToolTip(false, rect);

        if (!isUnlocked)
            UpdateItemColor(lastColor);
    }

    private Color GetColorByHex(string hexNumber)
    {
        ColorUtility.TryParseHtmlString(hexNumber, out Color color);

        return color;
    }
    private void OnValidate()
    {
        if(skillData == null)
            return;

        skillName = skillData.displayName;
        skillIcon.sprite = skillData.icon;
        skillCost = skillData.cost;
        gameObject.name ="UI_TreeNode" + skillData.displayName;
    }
}

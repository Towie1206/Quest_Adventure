using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UI_TreeConnectDetails
{
    public UI_TreeConnectHandler chillNode;
    public NodeDirectionType direction;
    [Range(100f, 350f)] public float length;
    [Range(-50f, 50f)] public float rotation;
}

public class UI_TreeConnectHandler : MonoBehaviour
{
    private RectTransform rect => GetComponent<RectTransform>();
    [SerializeField] private UI_TreeConnectDetails[] connectionDetails; // sẽ nối node nào hướng nào, dài bao nhiêu, xoay bao nhiêu
    [SerializeField] private UI_TreeConnection[] connections; // chỉ là dây nối

    private Image connectionImage;
    private Color originalColor; // màu xám mặc định chưa mở khóa

    private void Awake()
    {
        if (connectionImage != null)
            originalColor = connectionImage.color;
    }

    private void OnValidate()
    {
        if (connections.Length <= 0)
            return;

        if (connectionDetails.Length != connections.Length)
            return;

        UpdateConnections();
    }

    public void UpdateConnections() // update vị trí, xoay, dài, image của connection, xếp vị trí 
    {
        for (int i = 0; i < connectionDetails.Length; i++)
        {
            var detail = connectionDetails[i]; // lấy thông tin từ connectionDetails bao gồm chillNode, direction, length, rotation
            var connection = connections[i];

            Vector2 targetPosition = connection.GetConnectionPoint(rect); // lấy vị trí của chillNode dựa trên connectionPoint của connection
            Image connectionImage = connection.GetConnectionImage(); 

            connection.DirectConnection(detail.direction, detail.length, detail.rotation); // set vị trí, xoay, dài 

            if (detail.chillNode == null) // nếu chillNode chưa được gán thì bỏ qua
                continue;

            detail.chillNode.SetPosition(targetPosition);
            detail.chillNode.SetConnectionImage(connectionImage);
            detail.chillNode.transform.SetAsLastSibling(); // đẩy chillNode xuống để ko bị đề lên parent
        }
    }

    public void UpdateAllConnections()
    {
        UpdateConnections();

        foreach (var node in connectionDetails)
        {
            if (node.chillNode == null)
                continue;
            node.chillNode.UpdateConnections();
        }
    }

    public void UnlockedConnectionImage(bool unlocked)
    {
        if (connectionImage == null)
            return;

        connectionImage.color = unlocked ? Color.white : originalColor;
    }

    public void SetConnectionImage(Image image) => connectionImage = image;

    // set vị trí của node hiện tại dựa trên vị trí của connectionPoint của connection
    public void SetPosition(Vector2 position) => rect.anchoredPosition = position; 
}

using UnityEngine;

public class UI_MiniHealthBar : MonoBehaviour
{
    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void OnEnable()
    {
        entity.OnFlipped += HandleFlip; // Đăng ký hàm HandleFlip để lắng nghe event OnFlipped có invoke ko 
    }

    private void OnDisable()
    {
        entity.OnFlipped -= HandleFlip;
    }
    //Dòng này reset rotation của GameObject về trạng thái ban đầu.
    private void HandleFlip() => transform.rotation = Quaternion.identity; //Quaternion.identity == Quaternion.Euler(0, 0, 0)

}

using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private DoorType doorType;

    public DoorType DoorType => doorType;

    public void Open()
    {
        gameObject.SetActive(false);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}

public enum DoorType
{
    Left,
    Right,
    Top,
    Down
}
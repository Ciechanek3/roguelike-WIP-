using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private DoorType doorType;

    public DoorType DoorType => doorType;
}

public enum DoorType
{
    Left,
    Right,
    Top,
    Down
}
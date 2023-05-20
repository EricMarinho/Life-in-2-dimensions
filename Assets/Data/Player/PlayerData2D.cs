using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData2D", menuName = "Data/Player/2D", order = 1)]
public class PlayerData2D : ScriptableObject
{
    public float _speed;
    public float _jumpForce;
}

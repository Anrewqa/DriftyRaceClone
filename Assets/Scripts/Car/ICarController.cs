using GameSettings;
using UnityEngine;

namespace Car
{
    public interface ICarController
    {
        ICarMover CarMover { get; }
        ICarRotator CarRotator { get; }
        Settings Settings { get; }
        CarMoveData MoveData { get; }
        Transform Transform { get; }
        Rigidbody Rigidbody { get; }
    }
}
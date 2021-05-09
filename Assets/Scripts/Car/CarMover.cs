using System;
using UnityEngine;

namespace Car
{
    [Serializable]
    public class CarMover
    {
        private CarController _carController;
        private Rigidbody _rigidbody;

        public CarMover(CarController carController)
        {
            _carController = carController;

            _rigidbody = _carController.Rigidbody;
        }

        public void MoveForward()
        {
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.velocity = Vector3.zero;

            var moveData = _carController.MoveData;
            
            _rigidbody.MovePosition(_rigidbody.position + moveData.CurrentVelocity * moveData.Speed);
        }
    }
}

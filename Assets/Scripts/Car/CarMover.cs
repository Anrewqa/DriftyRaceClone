using System;
using UnityEngine;

namespace Car
{
    [Serializable]
    public class CarMover : ICarMover
    {
        private Rigidbody _rigidbody;
        
        public ICarController Controller { get; private set; }

        public CarMover(ICarController carController)
        {
            Controller = carController;
            _rigidbody = Controller.Rigidbody;
        }

        public void Move()
        {
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.velocity = Vector3.zero;

            var moveData = Controller.MoveData;
            var speed = moveData.Speed * Time.fixedDeltaTime;

            _rigidbody.MovePosition(_rigidbody.position + moveData.CurrentVelocity * speed);
        }
    }
}

using System;
using UnityEngine;

namespace CustomInput
{
    public class StandaloneTapInput : ITapInput
    {
        public event Action Tap;
        
        public void CheckInput()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Tap?.Invoke();
            }
        }
    }
}
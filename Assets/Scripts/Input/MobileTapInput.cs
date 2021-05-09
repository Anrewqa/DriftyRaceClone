using System;
using UnityEngine;

namespace CustomInput
{
    public class MobileTapInput : ITapInput
    {
        public event Action Tap;
        
        public void CheckInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    Tap?.Invoke();
                }
            }
        }
    }
}
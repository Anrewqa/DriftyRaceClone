using System;
using UnityEngine;

namespace CustomInput
{
    public class InputController : MonoBehaviour
    {
        public ITapInput TapInput { get; private set; }

        private void Awake()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    TapInput = new StandaloneTapInput();
                    break;
                case RuntimePlatform.Android:
                case RuntimePlatform.IPhonePlayer:
                    TapInput = new MobileTapInput();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Update()
        {
            TapInput.CheckInput();
        }
    }
}
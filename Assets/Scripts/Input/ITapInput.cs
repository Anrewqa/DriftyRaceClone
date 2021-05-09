using System;

namespace CustomInput
{
    public interface ITapInput
    {
        event Action Tap;
        
        void CheckInput();
    }
}
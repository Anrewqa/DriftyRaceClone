namespace Car
{
    public interface ICarRotator
    {
        ICarController Controller { get; }

        void Rotate();
    }
}
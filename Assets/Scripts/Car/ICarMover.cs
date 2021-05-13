namespace Car
{
    public interface ICarMover
    {
        ICarController Controller { get; }
        
        void Move();
    }
}
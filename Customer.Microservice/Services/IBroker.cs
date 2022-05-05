namespace Customer.Microservice.Services
{
    public interface IBroker
    {
        bool Enqueue(string messageString);
    }
}

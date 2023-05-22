var eventBus = new EventBus();

var thePublisher = new Publisher(eventBus);
var loggerOne = new Logger();
var emailService = new EmailService();

eventBus.RegisterSubscriber(loggerOne);
eventBus.RegisterSubscriber(emailService);


//thePublisher.Send("Hello");
//eventBus.Dispatch();

thePublisher.CookingSomething();

public class EventBus
{
    private List<ISubscriber> subscribers = new List<ISubscriber>();
    private string _currentMessage = string.Empty;

    public void SetCurrentMessage(string message)
    {
        _currentMessage = message;
    }
    public void RegisterSubscriber(ISubscriber subscriber)
    {
        subscribers.Add(subscriber);
    }

    public void UnregisterSubscriber(ISubscriber subscriber) 
    {
        subscribers.Remove(subscriber);
    }  

    public void Dispatch()
    {
        foreach (var subscriber in subscribers)
        {
            subscriber.OnReceived(_currentMessage);
        }
    }
}


public class Publisher
{
    private EventBus bus;

    public Publisher(EventBus bus)
    {
        this.bus = bus;
    }
    public void Send(string message)
    {
        Console.WriteLine($"Sending message: {message}");
        bus.SetCurrentMessage(message);
    }

    public void CookingSomething()
    {
        bus.SetCurrentMessage("Started Cooking");
        bus.Dispatch(); 
        bus.SetCurrentMessage("Turning the ingredients to something amazing!");
        bus.Dispatch();

        bus.SetCurrentMessage("Waiting");
        bus.Dispatch();
        bus.SetCurrentMessage("Done!");
        bus.Dispatch();
    }
}

public class Logger : ISubscriber
{
    public void OnReceived(string message)
    {
        Console.WriteLine("--------------------------------------");
        Console.WriteLine($"Logging message: {message}");
        Console.WriteLine("--------------------------------------");
    }
}

class EmailService : ISubscriber
{
    public void OnReceived(string message)
    {
        Console.WriteLine(
            $@"
To: Victory@gmail.com;
From: The Guy;
Title: Some Title;
Content: {message}");
    }
}

public interface ISubscriber
{
    void OnReceived(string message); // some method signature
}
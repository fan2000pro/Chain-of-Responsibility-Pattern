using System;
using System.Collections.Generic;

abstract class Handler
{
    protected Handler NextHandler;

    public void SetNext(Handler nextHandler)
    {
        NextHandler = nextHandler;
    }

    public abstract void HandleRequest(Request request);
}

class Request
{
    public string Type { get; }
    public string Content { get; }

    public Request(string type, string content)
    {
        Type = type;
        Content = content;
    }
}

class ErrorHandler : Handler
{
    public override void HandleRequest(Request request)
    {
        if (request.Type == "Error")
        {
            Console.WriteLine($"ErrorHandler обработал запрос: {request.Content}");
        }
        else
        {
            NextHandler?.HandleRequest(request);
        }
    }
}

class WarningHandler : Handler
{
    public override void HandleRequest(Request request)
    {
        if (request.Type == "Warning")
        {
            Console.WriteLine($"WarningHandler обработал запрос: {request.Content}");
        }
        else
        {
            NextHandler?.HandleRequest(request);
        }
    }
}

class InfoHandler : Handler
{
    public override void HandleRequest(Request request)
    {
        if (request.Type == "Info")
        {
            Console.WriteLine($"InfoHandler обработал запрос: {request.Content}");
        }
        else
        {
            NextHandler?.HandleRequest(request);
        }
    }
}

class Program
{
    static void Main()
    {
        var errorHandler = new ErrorHandler();
        var warningHandler = new WarningHandler();
        var infoHandler = new InfoHandler();

        errorHandler.SetNext(warningHandler);
        warningHandler.SetNext(infoHandler);

        var requests = new List<Request>
        {
            new Request("Info", "Информация о процессе"),
            new Request("Warning", "Предупреждение о низком уровне ресурсов"),
            new Request("Error", "Ошибка в системе"),
            new Request("Debug", "Отладочное сообщение")
        };

        foreach (var request in requests)
        {
            errorHandler.HandleRequest(request);
        }
    }
}

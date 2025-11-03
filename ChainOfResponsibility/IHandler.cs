namespace KkxWebServer.ChainOfResponsibility;

internal interface IHandler
{
	IHandler? Next { get; set; }
	IDictionary<int, object> Items { get; set; }
	Task HandleAsync();
}
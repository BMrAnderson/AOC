using Core;
using Core.CubeConundrum;
using Core.GearRatios;
using Core.Scratchcards;
using Core.Trebuchet;
using Microsoft.Extensions.Hosting;
using Presentation.Console;


try
{
    var host = Configuration.Build(args);
    var cards = Services.Get<IScratchCards>(host);
    var documentPath = Path.Combine(Environment.CurrentDirectory, Constants.DataUrls.ScratchCards);
    var result = await cards.GetTotalWinningScratchCards(documentPath);

    Console.WriteLine(result);

    await host.RunAsync();
}
catch (Exception e)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("An error occurred:");
    Console.WriteLine(e.Message);
    Console.WriteLine(e.StackTrace);
}
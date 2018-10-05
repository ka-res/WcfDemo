using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.ServiceModel;
using WcfDemo.Contracts;

namespace WcfDemo.Client
{
    class Program
    {
        static IWindsorContainer _container;

        static Program()
        {
            _container = new WindsorContainer();
            _container
                .AddFacility<WcfFacility>()
                .Register(Component
                    .For<IMessageService>()
                    .AsWcfClient(
                        new DefaultClientModel
                        {
                            Endpoint = WcfEndpoint
                                .BoundTo(new BasicHttpBinding())
                                .At("http://localhost:62505/MessageService.svc")
                        })
                );
        }

        static void Main(string[] args)
        {
            ConsoleKeyInfo input;
            do
            {

                Console.WriteLine("=====================================================================");
                RewriteMain();
                Console.WriteLine("=====================================================================");
                Console.WriteLine();
                input = Console.ReadKey();
            } while (input.Key != ConsoleKey.Escape);

            if (_container != null)
            {
                _container.Dispose();
            }
        }

        private static void PrintConsoleLog(string logText)
        {
            var logFormattedText = $"> {DateTime.Now}: {logText}";
            Console.WriteLine(logFormattedText);
        }

        private static void RewriteMain()
        {
            Console.WriteLine("[WCF DEMO]");
            Console.Write("Ze względu na bardzo uproszczone wymagania," +
                Environment.NewLine + "proszę pamiętać o wprowadzeniu do pliku ConfigHandler.cs" +
                Environment.NewLine + "odpowiednich wartości loginu i hasła dla konta GMail," +
                Environment.NewLine + "dla którego aktywna jest opcja logowania " +
                Environment.NewLine + "do mniej bezpiecznych aplikacji" +
                Environment.NewLine + "[ka-res, 2018]" +
                Environment.NewLine);
            Console.WriteLine();

            PrintConsoleLog("Następuje przygotowanie serwisu");
            var messageService = _container.Resolve<IMessageService>();

            PrintConsoleLog("Wiadomość jest opracowywana");
            var messagRequest = new MessageRequest
            {
                FirstName = "Janusz",
                LastName = "NOsacz",
                LegalForm = LegalForm.Person,
                Contacts = new[]
                {
                    new Contact
                    {
                        ContactType = ContactType.Email,
                        Value = $"kares.inf@gmail.com"
                    }
                }
            };

            PrintConsoleLog("Następuje próba wysłania wiadomości");
            var message = messageService.Send(messagRequest);
            PrintConsoleLog("Oczekiwanie na odpowiedź serwisu");
            var returnMessage = string.Empty;

            switch (message.ReturnCode)
            {
                case ReturnCode.InternalError:
                    returnMessage = message.ErrorMessage;
                    break;

                case ReturnCode.ValidationError:
                    returnMessage = message.ErrorMessage;
                    break;

                case ReturnCode.Success:
                    returnMessage = "Wiadomość wysłana";
                    break;

                default:
                    returnMessage = "Wystąpił wewnęrzny błąd serwisu";
                    break;
            }

            PrintConsoleLog(returnMessage);
            PrintConsoleLog("Kończenie pracy serwisu");
            PrintConsoleLog($"Wciśnij dowolny przycisk inny od ESC,\r\n\t\t\tby wznowić działanie aplikacji...");
        }
    }
}
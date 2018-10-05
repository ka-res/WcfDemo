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
            Console.WriteLine("~~~ WcfDemo ~~~");
            Console.WriteLine("Następuje przygotowanie serwisu");

            var messageService = _container.Resolve<IMessageService>();

            PrintConsoleLog("Wiadomość jest opracowywana");

            var messagRequest = new MessageRequest
            {
                FirstName = "Testowy",
                LastName = "Janusz",
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
            PrintConsoleLog("Oczekiwanie na odpowiedź serwisu");

            var message = messageService.Send(messagRequest);
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

            returnMessage += message.ReturnCode == ReturnCode.Success
                ? returnMessage
                : Environment.NewLine + message.ErrorMessage;

            PrintConsoleLog(!string.IsNullOrEmpty(returnMessage)
                ? returnMessage
                : "Wiadomość została wysłana z powodzeniem");

            PrintConsoleLog("Kończenie pracy serwisu");
            PrintConsoleLog("Naciśnij dowolny przycisk...");
            Console.ReadKey();

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
    }
}
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
            var messageService = _container.Resolve<IMessageService>();
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
                    returnMessage = $"Wiadomość została wysłana!";
                    break;

                default:
                    returnMessage = null;
                    break;
            }

            PrintMessage(!string.IsNullOrEmpty(returnMessage)
                ? returnMessage
                : $"Nie udało się uzyskać odpowiedzi");

            if (_container != null)
            {
                _container.Dispose();
            }
        }

        private static void PrintMessage(string returnMessage)
        {
            Console.Write(returnMessage);
            Console.ReadKey();
        }
    }
}
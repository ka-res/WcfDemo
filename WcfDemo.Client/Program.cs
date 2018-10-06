using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.ServiceModel;
using WcfDemo.Contracts;

namespace WcfDemo
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
                Environment.NewLine + "i nie ma wielostopniowego logowania." +
                Environment.NewLine +
                Environment.NewLine + "Na pulpicie należy utworzyć plik config.txt" +
                Environment.NewLine + "z nazwą e-mail nadawcy w pierwszej linijce" +
                Environment.NewLine + "oraz hasłem w drugiej" +
                Environment.NewLine + "[ka-res, 2018]" +
                Environment.NewLine);
            Console.WriteLine();

            PrintConsoleLog("Następuje przygotowanie serwisu");
            var messageService = _container.Resolve<IMessageService>();

            PrintConsoleLog("Wiadomość będzie teraz opracowywana");
            var messageRequest = new MessageRequest();

            Console.WriteLine("Wprowadź rodzaj kontaktu");
            Console.WriteLine("Dostępne rodzaje to:\r\n\t0 - osoba\r\n\t1 - firma");
            int legalForm;
            legalForm = Convert.ToInt32(Console.ReadLine());
            var legalFormEnum = (LegalForm)Enum.ToObject(typeof(LegalForm), legalForm);
            messageRequest.LegalForm = legalFormEnum;
            
            string firstName;
            if (legalForm == (int)LegalForm.Person)
            {
                Console.WriteLine("Wprowadź imię kontaktu:");
                firstName = Console.ReadLine();
                messageRequest.FirstName = firstName;
            }           

            string lastName;
            Console.WriteLine(legalForm == (int)LegalForm.Person
                ? "Wprowadź nazwisko kontaktu:"
                : "Wprowadź nazwę firmy");
            lastName = Console.ReadLine();
            messageRequest.LastName = lastName;

            var contacts = new Contact[6];
            Console.WriteLine("Wprowadź inforamcje kontaktowe");
            ConsoleKeyInfo input;


            int counter = 0;

            do
            {
                var contact = new Contact();
                Console.WriteLine("Istnieje możliwość wprowadzaenia do 6 rodzajów informacji kontaktowych");
                Console.WriteLine("Podaj rodzaj informacji kontakowej");
                Console.WriteLine("Dostępne rodzaje to:\r\n\t0 - telefon komórkowy\r\n\t1 - fax\r\n\t2 - e-mail\r\n\t" +
                    "3 - telefon służbowy\r\n\t4 - fax służbowy\r\n\t5 - e-mail służbowy");
                int contactType;
                contactType = Convert.ToInt32(Console.ReadLine());
                var contactTypeEnum = (ContactType)Enum.ToObject(typeof(ContactType), contactType);
                contact.ContactType = contactTypeEnum;
                //contact.ContactType = (ContactType)contactType;
                Console.WriteLine("Podaj wartość informacji kontaktowej");
                string value;
                value = Console.ReadLine();
                contact.Value = value;
                contacts[counter] = contact;

                PrintConsoleLog($"Wciśnij dowolny przycisk inny od ESC,\r\n\t\t\tby dodać nową inforamcję kontaktową...");
                input = Console.ReadKey();
                counter++;
            } while (input.Key != ConsoleKey.Escape && counter < 6);

            messageRequest.Contacts = contacts;
            //string 
            //{
            //    FirstName = "Janusz",
            //    LastName = "NOsacz",
            //    LegalForm = LegalForm.Person,
            //    Contacts = new[]
            //    {
            //        new Contact
            //        {
            //            ContactType = ContactType.Email,
            //            Value = $"kares.inf@gmail.com"
            //        }
            //    }
            //};

            PrintConsoleLog("Następuje próba wysłania wiadomości");
            var message = messageService.Send(messageRequest);
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
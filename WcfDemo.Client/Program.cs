using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Configuration;
using System.ServiceModel;
using WcfDemo.Common;
using WcfDemo.Contracts;

namespace WcfDemo
{
    class Program
    {
        static IWindsorContainer _container;

        static Program()
        {
            InitializeDiContainer();
        }

        static void Main(string[] args)
        {
            DisplayAppInterface();
            DisposeDiContainer();
        }

        #region clientAppMethods
        private static void PrintConsoleLog(string logText, ConsoleDisplayType consoleDisplayType)
        {
            Console.ForegroundColor = ConsoleDisplayHelper.ReturnConsoleTextColor(consoleDisplayType);
            var logDescription = ConsoleDisplayHelper.GetEnumDescription(consoleDisplayType);
            var logFormattedText = $"> {DateTime.Now} {logDescription} {logText}";
            Console.WriteLine(logFormattedText);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void RewriteMain()
        {
            DisplayAppHeader();
            PrepareMessageRequest(out var returnMessage, out var returnConsoleDisplayType);
            DisplayResponseWithFooter(returnMessage, returnConsoleDisplayType);            
        }

        private static void DisplayAppHeader()
        {
            Console.WriteLine("[WCF DEMO]");
            Console.Write("Ze względu na bardzo uproszczone wymagania" +
                Environment.NewLine + "na pulpicie należy utworzyć plik credentials.txt" +
                Environment.NewLine + "z nazwą e-mail nadawcy w pierwszej linijce" +
                Environment.NewLine + "oraz hasłem w drugiej do konta" +
                Environment.NewLine + "dla którego aktywna jest opcja logowania " +
                Environment.NewLine + "do mniej bezpiecznych aplikacji" +
                Environment.NewLine +
                Environment.NewLine + "i nie ma wielostopniowego logowania." +
                Environment.NewLine + "Plik mailBody.txt w tej samej lokalizacji" +
                Environment.NewLine + "powinien zawierać przykładową treść e-maila." +
                Environment.NewLine + "[ka_res, 2018]" +
                Environment.NewLine + "==============================================" +
                Environment.NewLine);
            Console.WriteLine();
        }

        private static void PrepareMessageRequest(out string returnMessage, out ConsoleDisplayType consoleDisplayType)
        {
            PrintConsoleLog("Następuje przygotowanie serwisu", ConsoleDisplayType.Initialization);
            var messageService = _container.Resolve<IMessageService>();

            PrintConsoleLog("Wiadomość będzie teraz opracowywana", ConsoleDisplayType.Preparing);
            var messageRequest = new MessageRequest();

            string legalForm;
            bool isInvalid = true;
            var legalFormEnumCount = Enum.GetNames(typeof(LegalForm)).Length;
            do
            {
                PrintConsoleLog("Wprowadź rodzaj kontaktu", ConsoleDisplayType.Instruction);
                PrintConsoleLog("Dostępne rodzaje to:\r\n\t\t\t\t1 - osoba\r\n\t\t\t\t2 - firma", ConsoleDisplayType.Option);
                legalForm = AskForInput();

                isInvalid = !int.TryParse(legalForm, out int legalFormVerified) ||
                        Convert.ToInt32(legalForm) > legalFormEnumCount ||
                        Convert.ToInt32(legalForm) < 1;

                if (isInvalid)
                {
                    PrintConsoleLog("Niepoprawna wartość!", ConsoleDisplayType.Missing);
                }

            } while (isInvalid);
            var legalFormNumber = Convert.ToInt32(legalForm);
            var legalFormEnum = (LegalForm)Enum.ToObject(typeof(LegalForm), legalFormNumber);
            messageRequest.LegalForm = legalFormEnum;
            
            string firstName;
            if (legalFormEnum == LegalForm.Person)
            {
                PrintConsoleLog("Wprowadź imię kontaktu", ConsoleDisplayType.Instruction);
                firstName = AskForInput();
                messageRequest.FirstName = firstName;
            }

            string lastName;
            PrintConsoleLog(legalFormEnum == LegalForm.Person
                ? "Wprowadź nazwisko kontaktu:"
                : "Wprowadź nazwę firmy", ConsoleDisplayType.Instruction);
            lastName = AskForInput();
            messageRequest.LastName = lastName;

            AskForContacts(messageRequest);
            

            PrintConsoleLog("Następuje próba wysłania wiadomości", ConsoleDisplayType.Information);
            var message = messageService.Send(messageRequest);
            PrintConsoleLog("Oczekiwanie na odpowiedź serwisu", ConsoleDisplayType.Information);
            returnMessage = string.Empty;

            switch (message.ReturnCode)
            {
                case ReturnCode.InternalError:
                    returnMessage = message.ErrorMessage;
                    consoleDisplayType = ConsoleDisplayType.InternalError;
                    break;

                case ReturnCode.ValidationError:
                    returnMessage = message.ErrorMessage;
                    consoleDisplayType = ConsoleDisplayType.ValidationError;
                    break;

                case ReturnCode.Success:
                    returnMessage = null;
                    consoleDisplayType = ConsoleDisplayType.Success;
                    break;

                default:
                    returnMessage = "Nieznany błąd";
                    consoleDisplayType = ConsoleDisplayType.InternalError;
                    break;
            }
        }

        private static void AskForContacts(MessageRequest messageRequest)
        {
            bool isInvalidCountLimit = true;
            string contactCountLimit;
            do
            {
                PrintConsoleLog("Podaj maksymalną ilość kontaktów, które chcesz wprowadzić", ConsoleDisplayType.Instruction);
                contactCountLimit = AskForInput();

                isInvalidCountLimit = !int.TryParse(contactCountLimit, out int contactCountLimitVerified);
                if (isInvalidCountLimit)
                {
                    PrintConsoleLog("Niepoprawna wartość!", ConsoleDisplayType.Missing);
                }

            } while (isInvalidCountLimit);
            var contactCountLimitNumber = Convert.ToInt32(contactCountLimit);

            var contacts = new Contact[contactCountLimitNumber];
            PrintConsoleLog("Wprowadź inforamcje kontaktowe", ConsoleDisplayType.Instruction);
            ConsoleKeyInfo input;

            int counter = 0;
            do
            {
                var contact = new Contact();
                PrintConsoleLog($"Istnieje możliwość wprowadzaenia do {contactCountLimitNumber} rodzajów informacji kontaktowych", ConsoleDisplayType.Information);

                var contactTypeEnumCount = Enum.GetNames(typeof(ContactType)).Length;
                bool isInvalidContactType = true;
                string contactType;
                do
                {
                    PrintConsoleLog("Podaj rodzaj informacji kontakowej", ConsoleDisplayType.Instruction);
                    PrintConsoleLog("Dostępne rodzaje to:\r\n\t\t\t\t1 - telefon komórkowy\r\n\t\t\t\t2 - fax\r\n\t\t\t\t3 - e-mail\r\n\t\t\t\t" +
                        "4 - telefon służbowy\r\n\t\t\t\t5 - fax służbowy\r\n\t\t\t\t6 - e-mail służbowy", ConsoleDisplayType.Option);
                    contactType = AskForInput();
                    isInvalidContactType = !int.TryParse(contactType, out int contactTypeVerified) ||
                            Convert.ToInt32(contactType) > contactTypeEnumCount ||
                            Convert.ToInt32(contactType) < 1;

                    if (isInvalidContactType)
                    {
                        PrintConsoleLog("Niepoprawna wartość!", ConsoleDisplayType.Missing);
                    }
                } while (isInvalidContactType);
                var contactTypeNumber = Convert.ToInt32(contactType);
                var contactTypeEnum = (ContactType)Enum.ToObject(typeof(ContactType), contactTypeNumber);
                contact.ContactType = contactTypeEnum;
                               
                PrintConsoleLog("Podaj wartość informacji kontaktowej", ConsoleDisplayType.Instruction);
                string value;
                value = AskForInput();
                contact.Value = value;
                contacts[counter] = contact;

                PrintConsoleLog($"Wciśnij dowolny przycisk inny od ESC,\r\n\t\t\tby dodać nową inforamcję kontaktową...", ConsoleDisplayType.Option);
                input = Console.ReadKey();
                Console.WriteLine();
                counter++;
                if (counter == contactCountLimitNumber)
                {
                    PrintConsoleLog($"Osiągnięto zaplanowany limit {contactCountLimitNumber} kontaktów", ConsoleDisplayType.Notification);
                }
            } while (input.Key != ConsoleKey.Escape && counter < contactCountLimitNumber);

            messageRequest.Contacts = contacts;
        }

        private static void DisplayResponseWithFooter(string returnMessage, ConsoleDisplayType consoleDisplayType)
        { 
            PrintConsoleLog(returnMessage, consoleDisplayType);
            PrintConsoleLog("Kończenie pracy serwisu", ConsoleDisplayType.Exit);
            PrintConsoleLog($"Wciśnij dowolny przycisk inny od ESC,\r\n\t\t\tby wznowić działanie aplikacji...", ConsoleDisplayType.Option);
        }

        private static void InitializeDiContainer()
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
                                .At(RetrieveHostAddress())
                        })
                );
        }

        private static string AskForInput()
        {
            Console.Write("\n>> ");

            return Console.ReadLine();
        }

        private static void DisplayAppInterface()
        {
            ConsoleKeyInfo input;
            do
            {

                Console.WriteLine("==============================================");
                RewriteMain();
                Console.WriteLine("==============================================");
                Console.WriteLine();
                input = Console.ReadKey();
                Console.WriteLine();
            } while (input.Key != ConsoleKey.Escape);
        }

        private static string RetrieveHostAddress()
        {
            return ConfigurationManager.AppSettings["hostAddress"];
        }

        private static void DisposeDiContainer()
        {
            if (_container != null)
            {
                _container.Dispose();
            }
        }
        #endregion
    }
}
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using WcfDemo.Contracts;
using System.Web;

namespace WcfDemo.Service
{
    public class Global : HttpApplication
    {
        IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            _container = new WindsorContainer();

            _container.AddFacility<WcfFacility>()
              .Register
              (
                Component
                    .For<IWcfDemoDbContext>()
                    .ImplementedBy<WcfDemoDbContext>(),
                Component
                    .For<IMessageRequestRepository>()
                    .ImplementedBy<MessageRequestRepository>(),
                Component
                    .For<IMessageResponseRepository>()
                    .ImplementedBy<MessageResponseRepository>(),
                Component
                    .For<IContactRepository>()
                    .ImplementedBy<ContactRepository>(),
                Component
                    .For<IMessageService>()
                    .ImplementedBy<MessageService>()
                    .Named("MessageService")
              );
        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (_container != null)
                _container.Dispose();
        }
    }
}
namespace WcfDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<WcfDemoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WcfDemoDbContext context)
        {
            context.ContactTypes.AddOrUpdate(x => x.Id,
                new ContactTypeModel
                {
                    Description = "Telefon kom�rkowy",
                    Value = ContactType.Mobile.ToString()
                },
                new ContactTypeModel
                {
                    Description = "Fax",
                    Value = ContactType.Fax.ToString()
                },
                new ContactTypeModel
                {
                    Description = "E-mail",
                    Value = ContactType.Email.ToString()
                },
                new ContactTypeModel
                {
                    Description = "Telefon s�u�bowy",
                    Value = ContactType.OfficePhone.ToString()
                },
                new ContactTypeModel
                {
                    Description = "Fax s�u�bowy",
                    Value = ContactType.OfficeFax.ToString()
                },
                new ContactTypeModel
                {
                    Description = "E-mail s�u�bowy",
                    Value = ContactType.OfficeEmail.ToString()
                });

            context.LegalForms.AddOrUpdate(x => x.Id,
                new LegalFormModel
                {
                    Description = "Osoba",
                    Value = LegalForm.Person.ToString()
                },
                new LegalFormModel
                {
                    Description = "Firma",
                    Value = LegalForm.Company.ToString()
                });

            context.ReturnCodes.AddOrUpdate(x => x.Id,
                new ReturnCodeModel
                {
                    Description = "Sukces",
                    Value = ReturnCode.Success.ToString()
                },
                new ReturnCodeModel
                {
                    Description = "B��d walidacji",
                    Value = ReturnCode.ValidationError.ToString()
                },
                new ReturnCodeModel
                {
                    Description = "B��d wewn�trzny serwisu",
                    Value = ReturnCode.InternalError.ToString()
                });
        }
    }
}

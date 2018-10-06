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
                    Description = "Telefon komórkowy",
                    Value = ContactType.Mobile.ToString(),
                    IsSoftDeleted = false,
                    SaveDate = DateTime.Now
                },
                new ContactTypeModel
                {
                    Description = "Fax",
                    Value = ContactType.Fax.ToString(),
                    IsSoftDeleted = false,
                    SaveDate = DateTime.Now
                },
                new ContactTypeModel
                {
                    Description = "E-mail",
                    Value = ContactType.Email.ToString(),
                    IsSoftDeleted = false,
                    SaveDate = DateTime.Now
                },
                new ContactTypeModel
                {
                    Description = "Telefon s³u¿bowy",
                    Value = ContactType.OfficePhone.ToString(),
                    IsSoftDeleted = false,
                    SaveDate = DateTime.Now
                },
                new ContactTypeModel
                {
                    Description = "Fax s³u¿bowy",
                    Value = ContactType.OfficeFax.ToString(),
                    IsSoftDeleted = false,
                    SaveDate = DateTime.Now
                },
                new ContactTypeModel
                {
                    Description = "E-mail s³u¿bowy",
                    Value = ContactType.OfficeEmail.ToString(),
                    IsSoftDeleted = false,
                    SaveDate = DateTime.Now
                });

            context.LegalForms.AddOrUpdate(x => x.Id,
                new LegalFormModel
                {
                    Description = "Osoba",
                    Value = LegalForm.Person.ToString(),
                    IsSoftDeleted = false,
                    SaveDate = DateTime.Now
                },
                new LegalFormModel
                {
                    Description = "Firma",
                    Value = LegalForm.Company.ToString(),
                    IsSoftDeleted = false,
                    SaveDate = DateTime.Now
                });

            context.ReturnCodes.AddOrUpdate(x => x.Id,
                new ReturnCodeModel
                {
                    Description = "Sukces",
                    Value = ReturnCode.Success.ToString(),
                    IsSoftDeleted = false,
                    SaveDate = DateTime.Now
                },
                new ReturnCodeModel
                {
                    Description = "B³¹d walidacji",
                    Value = ReturnCode.ValidationError.ToString(),
                    IsSoftDeleted = false,
                    SaveDate = DateTime.Now
                },
                new ReturnCodeModel
                {
                    Description = "B³¹d wewnêtrzny serwisu",
                    Value = ReturnCode.InternalError.ToString(),
                    IsSoftDeleted = false,
                    SaveDate = DateTime.Now
                });
        }
    }
}

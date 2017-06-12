using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Infrastructure.Migrations
{

    [Migration(1)]
    public class V01 : Migration
    {
        public override void Up()
        {
            Create.Table("Faculty")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("FacultyName").AsString(256).NotNullable()
            .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);

            Create.Table("UserTitle")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString(128).NotNullable()
            .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);

            Create.Table("User")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserNo").AsInt32().NotNullable()
            .WithColumn("UserName").AsString(256).NotNullable()
            .WithColumn("UserSurname").AsString(256).NotNullable()
            .WithColumn("UserPassword").AsString(128).NotNullable()
            .WithColumn("UserMail").AsString(256).NotNullable()
            .WithColumn("UserAddress").AsString(256).NotNullable()
            .WithColumn("UserState").AsByte().NotNullable().WithDefaultValue(0)
            .WithColumn("ActivationCode").AsInt32().NotNullable()
            .WithColumn("FacultyID").AsInt32().ForeignKey("Faculty", "ID")
            .WithColumn("UserTitleID").AsInt32().NotNullable().ForeignKey("UserTitle", "ID")
            .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);

            Create.Table("Interest")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("InterestName").AsString(256).NotNullable()
            .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);

            Create.Table("UserInterest")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserID").AsInt32().ForeignKey("User", "ID")
            .WithColumn("InterestID").AsInt32().ForeignKey("Interest", "ID")
            .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);

            Create.Table("Saloon")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("SaloonName").AsString(256).NotNullable()
            .WithColumn("SaloonQuota").AsByte().WithDefaultValue(0)
            .WithColumn("SaloonAddress").AsString(256).NotNullable()
            .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);

            Create.Table("SaloonFeature")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("FeatureName").AsString(128).NotNullable()
            .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);

            Create.Table("Feature")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("SaloonID").AsInt32().ForeignKey("Saloon", "ID")
            .WithColumn("FeatureID").AsInt32().ForeignKey("SaloonFeature", "ID")
            .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);

            Create.Table("ActivityType")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("TypeName").AsString(128).NotNullable()
            .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);
        }

        public override void Down()
        {
            Delete.Table("ActivityType");
            Delete.Table("Feature");
            Delete.Table("SaloonFeature");
            Delete.Table("Saloon");
            Delete.Table("UserInterest");
            Delete.Table("Interest");
            Delete.Table("User");
            Delete.Table("UserTitle");
            Delete.Table("Faculty");
        }

    }

}

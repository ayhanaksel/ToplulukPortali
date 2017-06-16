using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Infrastructure.Migrations
{
    [Migration(2)]
    public class V02 : Migration
    {        
            public override void Down()
            {
                Delete.Table("ActivityWorkers");
                Delete.Table("ActivitySpeakers");
                Delete.Table("Activities");
                Delete.Table("Speakers");
                Delete.Table("Workers");
                Delete.Table("WebBackEnd");
            }

            public override void Up()
            {
                Create.Table("WebBackEnd")
                   .WithColumn("ID").AsInt32().Identity().PrimaryKey()
                   .WithColumn("AboutMe").AsString(150).NotNullable()
                   .WithColumn("Mission").AsString(256).NotNullable()
                   .WithColumn("Vision").AsString(256).NotNullable()
                   .WithColumn("Contact").AsString(256).NotNullable()
                   .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);

                Create.Table("Workers")
                   .WithColumn("ID").AsInt32().Identity().PrimaryKey()
                   .WithColumn("UserID").AsInt32().ForeignKey("User", "ID")
                   .WithColumn("WorkerMission").AsString(256).NotNullable()
                   .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);

                Create.Table("Speakers")
                   .WithColumn("ID").AsInt32().Identity().PrimaryKey()
                   .WithColumn("SpeakersName").AsString(150).NotNullable()
                   .WithColumn("SpeakerPhoto").AsString(256).NotNullable()
                   .WithColumn("SpeakerCV").AsString(256).NotNullable()
                   .WithColumn("SpeakerWorksFor").AsString(256).NotNullable()
                   .WithColumn("SpeakerSpeakAbout").AsString(256).NotNullable()
                   .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);

                Create.Table("Activities")
                    .WithColumn("ID").AsInt32().Identity().PrimaryKey()
                    .WithColumn("ActivityName").AsString(150).NotNullable()
                    .WithColumn("ActivityType").AsString().NotNullable()
                    .WithColumn("ActivityPhoto").AsString(256).NotNullable()
                    .WithColumn("Saloon").AsString(256).NotNullable()
                    .WithColumn("ActivityDate").AsDateTime().NotNullable()
                    .WithColumn("GuessLimit").AsInt32().NotNullable()
                    .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);

                Create.Table("ActivitySpeakers")
                    .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                    .WithColumn("ActivityID").AsInt32().ForeignKey("Activities", "ID")
                    .WithColumn("SpeakerID").AsInt32().ForeignKey("Speakers", "ID")
                    .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);

                Create.Table("ActivityWorkers")
                    .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                    .WithColumn("ActivityID").AsInt32().ForeignKey("Activities", "ID")
                    .WithColumn("WorkerID").AsInt32().ForeignKey("Workers", "ID")
                    .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false);
        }
    }
}

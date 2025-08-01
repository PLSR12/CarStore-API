﻿using FluentMigrator;

namespace CarStore.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_USER, "Create table to save the user's information")]
public class Version00001 : VersionBase
{

    public override void Up()
    {
        CreateTable("Users")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("Password").AsString(2000).NotNullable()
            .WithColumn("Telephone").AsString().NotNullable();

    }
}

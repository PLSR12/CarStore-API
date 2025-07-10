using FluentMigrator;

namespace CarStore.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.RENAME_COLUMNS_VEHICLE_FK, "Rename foreign key columns in Vehicles table")]
public class Version00003 : Migration
{
    private const string TABLE_NAME = "Vehicles";

    public override void Up()
    {
        Rename.Column("Brand").OnTable(TABLE_NAME).To("BrandId");
        Rename.Column("Owner").OnTable(TABLE_NAME).To("OwnerId");
        Rename.Column("Type").OnTable(TABLE_NAME).To("TypeId");
    }

    public override void Down()
    {
        Rename.Column("BrandId").OnTable(TABLE_NAME).To("Brand");
        Rename.Column("OwnerId").OnTable(TABLE_NAME).To("Owner");
        Rename.Column("TypeId").OnTable(TABLE_NAME).To("Type");
    }
}

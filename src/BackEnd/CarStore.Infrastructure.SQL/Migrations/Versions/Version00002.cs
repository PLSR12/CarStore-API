using FluentMigrator;

namespace CarStore.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_VEHICLES, "Create table to save the vehicles information")]
public class Version00002 : VersionBase
{
    private const string TABLE_VEHICLE_NAME = "Vehicles";

    public override void Up()
    {

        CreateTable("Brands").WithColumn("Name").AsString().NotNullable().WithColumn("Description").AsString(2000).NotNullable();

        CreateTable("TypesVehicle").WithColumn("Name").AsString().NotNullable().WithColumn("Description").AsString(2000).NotNullable();

        CreateTable(TABLE_VEHICLE_NAME)
            .WithColumn("Model").AsString().NotNullable()
            .WithColumn("Color").AsString().Nullable()
            .WithColumn("YearFabrication").AsInt32().Nullable()
            .WithColumn("LicensePlate").AsString().Nullable()
            .WithColumn("EnginePower").AsString().Nullable()
            .WithColumn("Mileage").AsInt32().Nullable()
            .WithColumn("Owner").AsGuid().Nullable().ForeignKey("FK_Vehicle_Owner", "Users", "Id").OnDelete(System.Data.Rule.SetNull)
            .WithColumn("Brand").AsGuid().Nullable().ForeignKey("FK_Vehicle_Brand", "Brands", "Id").OnDelete(System.Data.Rule.SetNull)
            .WithColumn("Type").AsGuid().Nullable().ForeignKey("FK_Vehicle_Types", "TypesVehicle", "Id").OnDelete(System.Data.Rule.SetNull);


    }
}

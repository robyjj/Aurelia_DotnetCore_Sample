using Hahn.ApplicatonProcess.February2021.Models;
using Hahn.ApplicatonProcess.February2021.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hahn.ApplicatonProcess.February2021.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>().HasData(
                    new Asset
                    {
                        ID = 1,
                        AssetName = "Volkswagen Polo",
                        CountryOfDepartment = "GERMANY",
                        Department = Department.Store1,
                        EMailAdressOfDepartment = "buycars@volkswagen.com",
                        PurchaseDate = new DateTime(2020, 11, 2)
                    },
                    new Asset
                    {
                        ID = 2,
                        AssetName = "HP Laptop",
                        CountryOfDepartment = "INDIA",
                        Department = Department.Store2,
                        EMailAdressOfDepartment = "support@hp.co.in",
                        PurchaseDate = new DateTime(2020, 5, 22),
                        Broken = true
                    },
                    new Asset
                    {
                        ID = 3,
                        AssetName = "Daikin Air Conditioner",
                        CountryOfDepartment = "FRANCE",
                        Department = Department.HQ,
                        EMailAdressOfDepartment = "staycool@daikin.com",
                        PurchaseDate = new DateTime(2021, 4, 3)
                    },
                    new Asset
                    {
                        ID = 4,
                        AssetName = "Gold Chain",
                        CountryOfDepartment = "GERMANY",
                        Department = Department.Store3,
                        EMailAdressOfDepartment = "jewels@hotmail.com",
                        PurchaseDate = new DateTime(2020, 10, 2)
                    },
                    new Asset
                    {
                        ID = 5,
                        AssetName = "3 Seater Sofa",
                        CountryOfDepartment = "INDIA",
                        Department = Department.MaintenanceStation,
                        EMailAdressOfDepartment = "interiordecorations@gmail.com",
                        PurchaseDate = new DateTime(2020, 8, 15)
                    }
                );;
        }
    }
}

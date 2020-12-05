using Microsoft.AspNetCore.Identity;
using Pladeco.Domain;
using Pladeco.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pladeco.Web.Data.Data
{
    public class SeedDb
    {
        private readonly ApplicationDbContext context;
        private readonly IUserHelper userHelper;
        private readonly Random random;

        public SeedDb(ApplicationDbContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            if (await this.context.Database.EnsureCreatedAsync())
            {
               
            }

            //if (!this.context.Countries.Any())
            //{
            //    await this.AddCountriesAndCitiesAsync();
            //}

            await this.CheckRolesAsync();

            await this.CheckUser("admin@consultoragrupodxas.com", "SysAdmin", "Admin");

            if (!this.context.DevAxes.Any())
            {
                await this.AddDevAxes();
            }

            if (!this.context.ResponsableUnits.Any())
            {
                await this.AddResponsableUnits();
            }

            if (!this.context.Sectors.Any())
            {
                await this.AddSectors();
            }

            if (!this.context.Areas.Any())
            {
                await this.AddAreas();
            }

            if (!this.context.Typologies.Any())
            {
                await this.AddTypologies();
            }

            //// Add products
            //if (!this.context.Products.Any())
            //{
            //    this.AddProduct("AirPods", 159, user);
            //    this.AddProduct("Blackmagic eGPU Pro", 1199, user);
            //    this.AddProduct("iPad Pro", 799, user);
            //    this.AddProduct("iMac", 1398, user);
            //    this.AddProduct("iPhone X", 749, user);
            //    this.AddProduct("iWatch Series 4", 399, user);
            //    this.AddProduct("Mac Book Air", 789, user);
            //    this.AddProduct("Mac Book Pro", 1299, user);
            //    this.AddProduct("Mac Mini", 708, user);
            //    this.AddProduct("Mac Pro", 2300, user);
            //    this.AddProduct("Magic Mouse", 47, user);
            //    this.AddProduct("Magic Trackpad 2", 140, user);
            //    this.AddProduct("USB C Multiport", 145, user);
            //    this.AddProduct("Wireless Charging Pad", 67.67M, user);
            //    await this.context.SaveChangesAsync();
            //}
        }

        private async Task<User> CheckUser(string userName, string name, string role)
        {
            // Add user
            var user = await this.userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                user = await this.AddUser(userName, name, role);
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, role);
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }

        private async Task<User> AddUser(string userName, string name, string role)
        {
            var user = new User
            {
                Name = name,
                Email = userName,
                UserName = userName,
                Active=true
            };

            var result = await this.userHelper.AddUserAsync(user, "admin1");
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            await this.userHelper.AddUserToRoleAsync(user, role);
            var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
            await this.userHelper.ConfirmEmailAsync(user, token);
            return user;
        }

        private async Task AddSectors()
        {
            this.AddSector("Agua potable, alcantarillado");
            this.AddSector("Alumbrado público");
            this.AddSector("Áreas verdes");
            this.AddSector("Capacitación");
            this.AddSector("Conectividad");
            this.AddSector("Consultoría");
            this.AddSector("Cultura");
            this.AddSector("Deporte");
            this.AddSector("Emergencia");
            this.AddSector("Equipamiento Comunitario");
            this.AddSector("Equipamiento Municipal");
            this.AddSector("Fomento productivo");
            this.AddSector("Informática");
            this.AddSector("Infraestructura comunitaria");
            this.AddSector("Infraestructura municipal");
            this.AddSector("Mantención, aseo y ornato");
            this.AddSector("Medio ambiente");
            this.AddSector("Salud");
            this.AddSector("Tránsito y transporte público");
            this.AddSector("Vialidad");
            this.AddSector("Vivienda");
            this.AddSector("Empleo");
            this.AddSector("Seguridad");
            this.AddSector("Grupos vulnerables");
            this.AddSector("Equidad de género");
            this.AddSector("Ordenamiento territorial");
            await this.context.SaveChangesAsync();
        }

        private void AddSector(string name)
        {
            this.context.Sectors.Add(new Sector
            {
                Name = name
            });
        }

        private async Task AddResponsableUnits()
        {
            this.AddResponsableUnit("Secretaría de Planificación");
            this.AddResponsableUnit("Gabinete");
            this.AddResponsableUnit("Dirección de Asesoría Jurídica");
            this.AddResponsableUnit("Dirección de Control");
            this.AddResponsableUnit("Secretaría Municipal");
            this.AddResponsableUnit("Dirección de Desarrollo Comunitario");
            this.AddResponsableUnit("Dirección de Obras Municipales");
            this.AddResponsableUnit("Dirección de Tránsito y Transporte Público");
            this.AddResponsableUnit("Dirección de Aseo, Ornato y Medio Ambiente");
            this.AddResponsableUnit("Dirección de Administración y Finanzas");
            this.AddResponsableUnit("Dirección para el Desarrollo de Personas");
            this.AddResponsableUnit("Dirección de Protección Civil");
            await this.context.SaveChangesAsync();
        }

        private void AddResponsableUnit(string name)
        {
            this.context.ResponsableUnits.Add(new ResponsableUnit
            {
                Name = name
            });
        }

        private async Task AddDevAxes()
        {
            this.AddDevAxis("Desarrollo Medio Físico, Infraestructura, Medio Ambiente");
            this.AddDevAxis("Desarrollo Sociocultural, Salud, Educación, Cultura y Deporte");
            this.AddDevAxis("Desarrollo Económico - Productivo");
            this.AddDevAxis("Gestión Municipal");
            this.AddDevAxis("Covid 19 y reactivación económica");
            this.AddDevAxis("Pudahuel comuna de deportista que promueve el deporte para una vida sana");
            this.AddDevAxis("Pudahuel comuna segura");
            this.AddDevAxis("Pudahuel comuna sustentable");
            this.AddDevAxis("La comuna de Pudahuel plataforma de negocios");
            await this.context.SaveChangesAsync();
        }

        private void AddDevAxis(string name)
        {
            this.context.DevAxes.Add(new DevAxis
            {
                Name = name
            });
        }

        private async Task AddAreas()
        {
            this.AddArea("Informática y Tecnología");
            this.AddArea("Contabilidad");
            this.AddArea("Finanzas");
            this.AddArea("Producción");
            this.AddArea("Operaciones");
            await this.context.SaveChangesAsync();
        }

        private void AddArea(string name)
        {
            this.context.Areas.Add(new Area
            {
                Name = name
            });
        }

        private async Task AddTypologies()
        {
            List<TypologyStage> stages = null;

            stages = new List<TypologyStage>();
            stages.Add(new TypologyStage()
            {
                Name = "Ejecución"
            });

            this.AddTypologie("Acción",stages);

            stages = new List<TypologyStage>();
            stages.Add(new TypologyStage()
            {
                Name = "Ejecución"
            });
            this.AddTypologie("Estudio", stages);

            stages = new List<TypologyStage>();
            stages.Add(new TypologyStage()
            {
                Name = "Ejecución"
            });
            stages.Add(new TypologyStage()
            {
                Name = "Diseño"
            });
            stages.Add(new TypologyStage()
            {
                Name = "Diseño y ejecución"
            });
            this.AddTypologie("Plan", stages);

            stages = new List<TypologyStage>();
            stages.Add(new TypologyStage()
            {
                Name = "Ejecución"
            });
            stages.Add(new TypologyStage()
            {
                Name = "Diseño"
            });
            this.AddTypologie("Programa", stages);

            stages = new List<TypologyStage>();
            stages.Add(new TypologyStage()
            {
                Name = "Ejecución"
            });
            stages.Add(new TypologyStage()
            {
                Name = "Diseño"
            });
            stages.Add(new TypologyStage()
            {
                Name = "Ejecución"
            });
            stages.Add(new TypologyStage()
            {
                Name = "Perfil"
            });
            this.AddTypologie("Proyecto", stages);

            await this.context.SaveChangesAsync();
        }

        private void AddTypologie(string name, List<TypologyStage> stages)
        {

            this.context.Typologies.Add(new Typology
            {
                Name = name,
                Stages=stages
            });
        }

        private async Task CheckRolesAsync()
        {
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Colaborador");
            await this.userHelper.CheckRoleAsync("Presupuesto");
        }

        //private void AddProduct(string name, decimal price, User user)
        //{
        //    this.context.Products.Add(new Product
        //    {
        //        Name = name,
        //        Price = price,
        //        IsAvailabe = true,
        //        Stock = this.random.Next(100),
        //        User = user,
        //        ImageUrl = $"~/images/Products/{name}.png"
        //    });
        //}
    }
}

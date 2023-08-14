using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using N5Company.Entities;
using N5Company.Persistence;
using N5Company.Repositories;

namespace N5Company.Test
{
    public class RepositoryUnitTests : BaseTest
    {
        [Fact]
        public async Task FindAllAsync()
        {
            string dbName = Guid.NewGuid().ToString();
            DataContext context = BuildContext(dbName);

            await context.Permissions.AddAsync(new Permission { NombreEmpleado = "Empleado 1", ApellidoEmpleado = "Empleado 1", FechaPermiso = DateTime.Now, TipoPermiso = 1 });
            await context.Permissions.AddAsync(new Permission { NombreEmpleado = "Empleado 2", ApellidoEmpleado = "Empleado 2", FechaPermiso = DateTime.Now, TipoPermiso = 1 });

            await context.SaveChangesAsync();

            DataContext context2 = BuildContext(dbName);

            Repository repository = new Repository(context2);
            ActionResult<List<Permission>> response = await repository.FindAllAsync<Permission>();
            List<Permission> permission = response.Value;
            Assert.Equal(2, permission.Count);
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            string dbName = Guid.NewGuid().ToString();
            DataContext context = BuildContext(dbName);

            await context.Permissions.AddAsync(new Permission { NombreEmpleado = "Empleado 1", ApellidoEmpleado = "Empleado 1", FechaPermiso = DateTime.Now, TipoPermiso = 1 });
            await context.Permissions.AddAsync(new Permission { NombreEmpleado = "Empleado 2", ApellidoEmpleado = "Empleado 2", FechaPermiso = DateTime.Now, TipoPermiso = 1 });

            await context.SaveChangesAsync();

            DataContext context2 = BuildContext(dbName);

            int id = 2;

            Repository repository = new Repository(context2);
            Permission response = await repository.GetById<Permission>(id);
            Assert.Equal(id, response.Id);
        }

        [Fact]
        public async Task AddAsync()
        {
            string dbName = Guid.NewGuid().ToString();
            DataContext context = BuildContext(dbName);

            var permission = new Permission { NombreEmpleado = "Empleado 1", ApellidoEmpleado = "Empleado 1", FechaPermiso = DateTime.Now, TipoPermiso = 1 };

            Repository repository = new Repository(context);
            repository.Add<Permission>(permission);
            await context.SaveChangesAsync();

            var context2 = BuildContext(dbName);
            var totalData = await context2.Permissions.CountAsync();
            Assert.Equal(1, totalData);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            string dbName = Guid.NewGuid().ToString();
            DataContext context = BuildContext(dbName);

            await context.Permissions.AddAsync(new Permission { NombreEmpleado = "Empleado 1", ApellidoEmpleado = "Empleado 1", FechaPermiso = DateTime.Now, TipoPermiso = 1 });
            await context.SaveChangesAsync();

            var context2 = BuildContext(dbName);

            Repository repository = new Repository(context2);

            var permissions = new Permission { Id = 1, NombreEmpleado = "Empleado 2", ApellidoEmpleado = "Empleado 1", FechaPermiso = DateTime.Now, TipoPermiso = 1 };

            var id = 1;
            repository.Update<Permission>(permissions);
            await context2.SaveChangesAsync();

            var exist = await context2.Permissions.AnyAsync(x => x.NombreEmpleado == "Empleado 2");
            Assert.True(exist);
        }
    }
}
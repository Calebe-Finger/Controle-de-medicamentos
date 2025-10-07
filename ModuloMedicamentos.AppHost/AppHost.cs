var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ControleDeMedicamentos_Dominio>("controledemedicamentos-dominio");

builder.Build().Run();

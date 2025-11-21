using eshop.application.EventHandler;
using eshop.application.Interfaces;
using eshop.application.Services;
using eshop.application.Validators;
using eshop.domain.Events;
using eshop.infrastructure.EmailSender;
using eshop.infrastructure.EventHandlers;
using eshop.infrastructure.Events;
using eshop.infrastructure.Persistence;
using eshop.infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<EshopDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

// Application DI
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IEventDispatcher, DomainEventDispatcher>();


// Event handlers
builder.Services.AddScoped<IDomainEventHandler<InvoiceCreatedEvent>, NotifyCustomerHandler>();
builder.Services.AddScoped<IDomainEventHandler<StockReducedEvent>, InventoryLogHandler>();


// Infrastructure services
builder.Services.AddScoped<IEmailSender, DummyEmailSender>();

// Application services
builder.Services.AddScoped<InvoiceService>();

// FluentValidation
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<CreateInvoiceRequestValidator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

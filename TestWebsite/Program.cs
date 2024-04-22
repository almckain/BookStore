var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<TestWebsite.IBookService, TestWebsite.BookService>();
builder.Services.AddSingleton<TestWebsite.IOrderService, TestWebsite.OrderServices>();
builder.Services.AddSingleton<TestWebsite.ICustomerService, TestWebsite.CustomerService>();
builder.Services.AddSingleton<TestWebsite.IGenreService, TestWebsite.GenreService>();
builder.Services.AddSingleton<TestWebsite.IPublisherService, TestWebsite.PublisherService>();
builder.Services.AddSingleton<TestWebsite.IAdminService, TestWebsite.AdminService>();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.Run();

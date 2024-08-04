Source
------------------------

https://www.c-sharpcorner.com/article/how-to-implement-jwt-authentication-in-web-api-using-net-6-0-asp-net-core/


Step by Step
------------------------

*** Step 1 **

=> Install-Package Microsoft.EntityFrameworkCore
=> Install-Package Microsoft.EntityFrameworkCore.SqlServer
=> Install-Package Microsoft.AspNetCore.Authentication.JwtBearer

////////////////////////////////////////////////////////////////////

*** Step 2

=> Create DBContext class and register in program.cs

////////////////////////////////////////////////////////////////////

** Step 3

=> Create JWT Token generate class and register in program.cs

////////////////////////////////////////////////////////////////////

** Step 4

=> Setup Jwt key,Issuer,Audience and Subject in the appsetting.json file

////////////////////////////////////////////////////////////////////

** Step 5

=> Add authentication middleware in the program.cs

//configured authentication middleware

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


//configured authentication middleware in here too
app.UseAuthentication();


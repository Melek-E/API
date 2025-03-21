﻿using API.Migrations;
using API.Models.Domain.Auth;
using API.Models.Domain.Extra;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Threading.Tasks;

public static class Seed
{
    public static async Task SeedSuperAdmin(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string roleName = "SuperAdmin";
        string adminEmail = "superadminmelek@admin.fr";
        string adminPassword = "SuperAdminPassword123!";
        string jsonFrameworks = "[{\"Name\":\"ASP.NET Core\", \"Version\":\"6.0\"}, {\"Name\":\"Entity Framework\", \"Version\":\"6.0\"}]";
        var adminFrameworks = JsonSerializer.Deserialize<List<Framework>>(jsonFrameworks);


        // Ensure the role exists
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        var superAdminUsers = await userManager.GetUsersInRoleAsync(roleName);
        if (superAdminUsers.Count > 0)
        {
            return;
        }

        // Check again??? fix
        var user = await userManager.FindByEmailAsync(adminEmail);
        if (user == null)
        {
            // Create the super admin user
            user = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                Frameworks = adminFrameworks
            };

            var result = await userManager.CreateAsync(user, adminPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create super admin user: " + string.Join(", ", result.Errors));
            }
        }

        if (!await userManager.IsInRoleAsync(user, roleName))
        {
            await userManager.AddToRoleAsync(user, roleName);
        }
    }




    public static async Task SeedRegularAdmin(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string roleName = "Admin";
        string adminEmail = "regularadmin@admin.fr";
        string adminPassword = "SuperAdminPassword123!";
        //string jsonFrameworks = "[{\"Name\":\"Admins\"}, {\"Name\":\"Admin\", ]";
        //var adminFrameworks = JsonSerializer.Deserialize<List<Framework>>(jsonFrameworks);

        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
        var AdminUsers = await userManager.GetUsersInRoleAsync(roleName);
        if (AdminUsers.Count > 0)
        {
            return;
        }

        // Check again??? fix
        var user = await userManager.FindByEmailAsync(adminEmail);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                Frameworks = []
            };

        }

        var result = await userManager.CreateAsync(user, adminPassword);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to create super admin user: " + string.Join(", ", result.Errors));
        }


        if (!await userManager.IsInRoleAsync(user, roleName))
        {
            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}
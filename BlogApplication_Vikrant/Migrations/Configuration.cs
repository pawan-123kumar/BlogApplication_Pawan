namespace BlogApplication_Vikrant.Migrations
{
    using BlogApplication_Vikrant.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogApplication_Vikrant.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BlogApplication_Vikrant.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Users.Add(new User { UserName = "Admin", Password = "admin", Role = "Admin" });
            context.Users.Add(new User { UserName = "Vikrant", Password = "vikrant", Role = "User" });

            string Des = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).";
            context.Blogs.Add(new Blog { Image = "~/Content/Images/client-1.png", Title="MyBob", Description= Des ,Author="Harry"});
            context.SaveChanges();
            base.Seed(context);
        }
    }
}

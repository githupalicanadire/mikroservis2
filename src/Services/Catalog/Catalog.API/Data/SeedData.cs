using Catalog.API.Models;

namespace Catalog.API.Data;

public static class SeedData
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        
        try
        {
            logger.LogInformation("Starting catalog seed data...");
            
            // Add demo products here when we know the data structure
            var demoProducts = GetDemoProducts();
            
            logger.LogInformation("Created {Count} demo products", demoProducts.Count);
            logger.LogInformation("Catalog seed data completed");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding catalog data");
        }
    }

    private static List<object> GetDemoProducts()
    {
        return new List<object>
        {
            new 
            {
                Id = Guid.NewGuid(),
                Name = "LEGO Classic Creative Bricks",
                Category = new[] { "Building Blocks", "Educational" },
                Summary = "Creative building set with colorful LEGO bricks",
                Description = "Unleash creativity with this amazing LEGO Classic set featuring 1000+ colorful bricks, wheels, windows, and doors. Perfect for building cars, houses, animals, and anything you can imagine!",
                ImageFile = "lego-classic.jpg",
                Price = 49.99m
            },
            new 
            {
                Id = Guid.NewGuid(),
                Name = "Teddy Bear Plush Toy",
                Category = new[] { "Plush Toys", "Comfort" },
                Summary = "Soft and cuddly teddy bear perfect for all ages",
                Description = "This adorable teddy bear is made from the softest materials and is perfect for cuddling. Hypoallergenic and safe for children of all ages.",
                ImageFile = "teddy-bear.jpg",
                Price = 24.99m
            },
            new 
            {
                Id = Guid.NewGuid(),
                Name = "Remote Control Racing Car",
                Category = new[] { "Remote Control", "Vehicles" },
                Summary = "High-speed RC car with amazing control",
                Description = "Experience the thrill of racing with this high-performance remote control car. Features LED lights, realistic sound effects, and can reach speeds up to 15 mph!",
                ImageFile = "rc-car.jpg",
                Price = 89.99m
            },
            new 
            {
                Id = Guid.NewGuid(),
                Name = "Educational Puzzle Set",
                Category = new[] { "Puzzles", "Educational" },
                Summary = "Brain-developing puzzles for kids",
                Description = "Set of 5 educational puzzles featuring animals, numbers, letters, shapes, and colors. Great for developing problem-solving skills and hand-eye coordination.",
                ImageFile = "puzzle-set.jpg",
                Price = 19.99m
            },
            new 
            {
                Id = Guid.NewGuid(),
                Name = "Art & Craft Mega Kit",
                Category = new[] { "Arts & Crafts", "Creative" },
                Summary = "Complete art and craft supplies for young artists",
                Description = "Everything a young artist needs! Includes colored pencils, markers, crayons, stickers, colored paper, glue, scissors, and a guidebook with 50+ project ideas.",
                ImageFile = "art-kit.jpg",
                Price = 34.99m
            },
            new 
            {
                Id = Guid.NewGuid(),
                Name = "Dinosaur Action Figures Set",
                Category = new[] { "Action Figures", "Dinosaurs" },
                Summary = "Realistic dinosaur figures for imaginative play",
                Description = "Set of 12 realistic dinosaur action figures including T-Rex, Triceratops, Stegosaurus, and more. Each figure is highly detailed and perfect for educational play.",
                ImageFile = "dinosaur-set.jpg",
                Price = 29.99m
            },
            new 
            {
                Id = Guid.NewGuid(),
                Name = "Musical Keyboard Piano",
                Category = new[] { "Musical Instruments", "Educational" },
                Summary = "Electronic keyboard with learning features",
                Description = "37-key electronic keyboard with built-in songs, sound effects, and learning modes. Includes a microphone and adjustable stand. Perfect for budding musicians!",
                ImageFile = "keyboard-piano.jpg",
                Price = 79.99m
            },
            new 
            {
                Id = Guid.NewGuid(),
                Name = "Princess Dress-Up Costume Set",
                Category = new[] { "Dress Up", "Roleplay" },
                Summary = "Beautiful princess costumes for dress-up play",
                Description = "Magical princess dress-up set with 3 different costume styles, accessories, jewelry, and a tiara. Made from high-quality, comfortable materials.",
                ImageFile = "princess-costume.jpg",
                Price = 39.99m
            },
            new 
            {
                Id = Guid.NewGuid(),
                Name = "Science Experiment Kit",
                Category = new[] { "STEM", "Educational" },
                Summary = "Fun science experiments for curious minds",
                Description = "Conduct 30+ safe science experiments at home! Kit includes all materials needed for chemistry, physics, and biology experiments with detailed instruction manual.",
                ImageFile = "science-kit.jpg",
                Price = 54.99m
            },
            new 
            {
                Id = Guid.NewGuid(),
                Name = "Board Game Collection",
                Category = new[] { "Board Games", "Family" },
                Summary = "Classic board games for family fun",
                Description = "Collection of 5 classic board games: Chess, Checkers, Backgammon, Tic-Tac-Toe, and Nine Men's Morris. Perfect for family game nights and developing strategy skills.",
                ImageFile = "board-games.jpg",
                Price = 44.99m
            }
        };
    }

    public static List<object> GetDemoUsers()
    {
        return new List<object>
        {
            new { Email = "admin@toyshop.com", Password = "Admin123!", Role = "Admin" },
            new { Email = "john.doe@example.com", Password = "User123!", Role = "User" },
            new { Email = "jane.smith@example.com", Password = "User123!", Role = "User" },
            new { Email = "mike.wilson@example.com", Password = "User123!", Role = "User" },
            new { Email = "sarah.johnson@example.com", Password = "User123!", Role = "User" }
        };
    }

    public static List<object> GetTestOrders()
    {
        var users = GetDemoUsers();
        var products = GetDemoProducts();
        var orders = new List<object>();

        // Create sample orders for each user
        foreach (var user in users.Skip(1)) // Skip admin, create orders for regular users
        {
            orders.Add(new
            {
                Id = Guid.NewGuid(),
                CustomerEmail = user.GetType().GetProperty("Email")?.GetValue(user),
                OrderDate = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 30)),
                TotalAmount = Random.Shared.Next(50, 300),
                Status = "Completed",
                Items = products.Take(Random.Shared.Next(1, 4)).ToList()
            });
        }

        return orders;
    }
}

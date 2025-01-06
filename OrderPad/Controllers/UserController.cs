// Controllers/UserController.cs
using Microsoft.AspNetCore.Mvc;
using OrderPad.Models;
using System.Collections.Generic;

public class UserController : Controller
{
    public List<User> users = new List<User>
        {
            new User { UserID = 1, Email = "user1@example.com", Password = "password1" },
            new User { UserID = 2, Email = "user2@example.com", Password = "password2" },
            new User { UserID = 3, Email = "user3@example.com", Password = "password3" }
        };

    private List<Restaurant> restaurants = new List<Restaurant>
    {
        new Restaurant { ID = 1, Name = "Pizza Palace", Email = "contact@pizzapalace.com", Phone = "123-456-7890", Address = "123 Main St, City A" },
        new Restaurant { ID = 2, Name = "Burger Bistro", Email = "info@burgerbistro.com", Phone = "098-765-4321", Address = "456 Market Ave, City B" },
        new Restaurant { ID = 3, Name = "Taco Town", Email = "hello@tacotown.com", Phone = "555-123-4567", Address = "789 King Blvd, City C" }
    };

    private List<Item> item = new List<Item>
{
    new Item { ItemID = 1, RestaurentID = 1, ItemName = "Pizza", Description = "Cheesy Margherita", Price = 12.99 },
    new Item { ItemID = 2, RestaurentID = 1, ItemName = "Burger", Description = "Classic Cheeseburger", Price = 9.99 },
    new Item { ItemID = 3, RestaurentID = 2, ItemName = "Pasta", Description = "Creamy Alfredo Pasta", Price = 11.99 },
    new Item { ItemID = 4, RestaurentID = 2, ItemName = "Salad", Description = "Caesar Salad", Price = 7.99 },
    new Item { ItemID = 5, RestaurentID = 3, ItemName = "Sushi", Description = "Fresh Salmon Sushi", Price = 14.99 }
};

    // List of Tables
    private List<Table> tables = new List<Table>
{
    new Table { TableID = 1, RestaurantID = 1, TableNumber = "T1" },
    new Table { TableID = 2, RestaurantID = 1, TableNumber = "T2" },
    new Table { TableID = 3, RestaurantID = 2, TableNumber = "A1" },
    new Table { TableID = 4, RestaurantID = 2, TableNumber = "A2" },
    new Table { TableID = 5, RestaurantID = 3, TableNumber = "S1" }
};

    // Display the registration form
    public IActionResult Register()
    {
        return View();
    }

    // Handle the form post from the registration page
    [HttpPost]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            // Normally, you'd save the user to a database here
            // (Password hashing should be implemented in a real-world app)

            return RedirectToAction("Login");
        }
        return View(user);
    }

    // Display the login form
    public IActionResult Login()
    {
        return View();
    }

    // Handle the form post from the login page
    [HttpPost]
    public IActionResult Login(User loginUser)
    {
        if (ModelState.IsValid)
        {
            var user = users.FirstOrDefault(u => u.Email == loginUser.Email && u.Password == loginUser.Password);

            if (user != null)
            {
                // Redirect to the user's restaurant details page using UserID
                return RedirectToAction("RestaurantDetails", new { id = user.UserID });
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }
        }

        return View(loginUser);
    }

    public IActionResult RestaurantDetails(int id)
    {
        // Fetch the restaurant using the ID (UserID)
        
        RestaurantDetails _db = new RestaurantDetails();
        _db.Restaurant = restaurants.FirstOrDefault(r => r.ID == id);

        if (_db.Restaurant == null)
        {
            return NotFound();
        }

        _db.Table = tables.Where(r => r.RestaurantID == id).ToList();
        _db.Item = item.Where(i=> i.RestaurentID == id).ToList();

        

        return View(_db); // Pass the restaurant data to the view
    }

    [HttpPost]
    public IActionResult AddTable(int restaurantId, string tableNumber)
    {
        // Create a new table for the restaurant
        tables.Add(new Table
        {
            TableID = tables.Count + 1,
            RestaurantID = restaurantId,
            TableNumber = tableNumber
        });

        // Redirect back to the RestaurantDetails action
        return RedirectToAction("RestaurantDetails", new { id = restaurantId });
    }

    [HttpPost]
    public IActionResult AddFoodOrder()
    {
        

        // Redirect back to the RestaurantDetails action
        return RedirectToAction("RestaurantDetails", new { id = 2 });
    }

    // Add these methods to the UserController

    public IActionResult FoodOrder(int tableId)
    {
        // Handle food order logic
        return View();  // You can pass the tableId to this view if needed
    }

    public IActionResult DrinksOrder(int tableId)
    {
        // Handle drinks order logic
        return View();
    }

    public IActionResult Payment(int tableId)
    {
        // Handle payment logic
        return View();
    }

    public IActionResult CloseOrder(int tableId)
    {
        // Handle order closing logic
        return View();
    }


    [HttpGet]
    public IActionResult GetTablesForRestaurant(int restaurantId)
    {
        var restaurantTables = tables.Where(t => t.RestaurantID == restaurantId).ToList();

        if (restaurantTables == null || !restaurantTables.Any())
        {
            return NotFound(new { Message = "No tables found for this restaurant." });
        }

        return Json(restaurantTables); // Return as JSON
    }


}
using System.ComponentModel.DataAnnotations;

namespace OrderPad.Models
{
    public class User
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        public int UserID { get; set; }

    }

    public class Restaurant_Info
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<int> UserList { get; set; }
    }

    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public int RestaurantID { get; set; }
        public int TableID { get; set; } // Optional: Null for takeout orders
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } // Optional: For takeout orders
        public List<Item> Items { get; set; } = new List<Item>();
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; } // E.g., "Pending", "Completed", "Cancelled"
    }

    public class Item
    {
        public int ItemID { get; set; }
        public int RestaurantID { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }

    public class Table
    {
        [Key]
        [Required]
        public int TableID { get; set; }
        public int RestaurantID { get; set; }  // To associate with Restaurant
        public string TableNumber { get; set; }  // To identify the table
    }

    public class RestaurantDetails
    {
        public Restaurant_Info Restaurant { get; set; }
        public List<Item> Items { get; set; }
        public List<Table> Tables { get; set; }

        public List<Order> Orders { get; set; }


    }

    public class RestaurantStaff
    {
        [Key]
        [Required]
        public int UserID { get; set; }
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string UserPhone { get; set; }

        [Required]
        public int RestaurantID { get; set; }

        private enum Role
        {
            Local_Admin,
            Staff
        }

        [Required]
        private Role UserRole { get; set; }

        public void AssignRole(string role)
        {
            if (Enum.TryParse(role, true, out Role parsedRole))
            {
                UserRole = parsedRole;
            }
            else
            {
                throw new ArgumentException("Invalid role");
            }
        }

        public string GetRole()
        {
            return UserRole.ToString();
        }
    }
}

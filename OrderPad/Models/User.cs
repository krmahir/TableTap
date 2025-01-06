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

    public class Restaurant 
    {
        public int ID { get; set;}
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

    }

    public class Item 
    {
        public int ItemID { get; set; }

        public int RestaurentID { get; set; }

        public string ItemName { get; set; }

        public string Description { get; set; }

        public Double Price { get; set; }

    }

    // Models/Table.cs
    // Models/Table.cs
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
        public Restaurant Restaurant { get; set; }
        public List<Item> Item { get; set; }
        public List<Table> Table { get; set; }

    }

    public class Restaurent_Staff 
    {
        [Key]
        [Required]
        public int UserID { get; set; }
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPhone { get; set; }
        [Required]
        public int RestaurantID { get; set; }
 
        private enum Role
        {
            Local_Admin,
            Staff // Correcting "Stuff" to "Staff" if that's intended
        }

        // Example usage inside the model
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

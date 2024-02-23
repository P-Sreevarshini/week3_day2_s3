using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dotnetapp.Models
{
    public class Login
    {
         [ForeignKey(nameof(Username))]
    public User? User { get; set; }
        public string Username { get; set; }

     [ForeignKey(nameof(Email))]
    public User? User { get; set; }

        public string Email { get; set; }
 [ForeignKey(nameof(Password))]
    public User? User { get; set; }
        public string Password { get; set; }
    }
}

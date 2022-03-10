global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel;
using System;
namespace BookApi.Models
{
    public class RegisterModel

    {

        public  enum IRole
        {
            Administrator, Superuser, user

        }
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "UserName is required ")]
        public string username { get; set; }


        [Required(ErrorMessage = "password is required ")]
        public byte[] hashedpassword { get; set; }

        [Required]
        public byte[] passwordsalt { get; set; }    

        [Required(ErrorMessage = "firstname is required ")]
        public string firstname { set; get; } = string.Empty;

        [Required(ErrorMessage = "lastname is required ")]
        public string lastname { set; get; } = string.Empty;

        [Required(ErrorMessage = "email is required ")]
        public string email { set; get; } = string.Empty;

        public System.DateTime datecreated { set; get; } = System.DateTime.Now;

        [DefaultValue(IRole.user)]
        public IRole role { set; get; }


    }
}

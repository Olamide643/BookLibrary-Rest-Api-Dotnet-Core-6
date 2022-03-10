namespace BookApi.Models
{
    public class UserSignUp
    {
        public enum Role
        {
            Administrator, Superuser, user

        }
         
    [Required(ErrorMessage = "UserName is required ")]
    public string username { get; set; }


    [Required(ErrorMessage = "password is required ")]
    public string password { get; set; }


    [Compare("password", ErrorMessage = "confirm password must match with password")]
    public string confirm_password { get; set; }


    [Required(ErrorMessage = "firstname is required ")]
    public string firstname { set; get; } = string.Empty;

    [Required(ErrorMessage = "lastname is required ")]
    public string lastname { set; get; } = string.Empty;

    [Required(ErrorMessage = "email is required ")]
    public string email { set; get; } = string.Empty;

    public System.DateTime datecreated { set; get; } = System.DateTime.Now;

    [DefaultValue(Role.user)]
    public Role role { set; get; }
}
}

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BusnesLogic
{
    public class User
    {
        // Not Delete!!!!!!!!  Need without parametric constructor. 
        public User() {}

        private  int _id;
        private  string _name;
        private  string _email;
        private  string _skype;
        private  string _signature;
        private  byte[] _userImage;

        public User(int id, string name, string email, string skype, string signature = "", byte[] userImage = null)
        {
            _id = id;
            _name = name;
            _email = email;
            _skype = skype;
            _signature = signature;
            _userImage = userImage;
        }


        //please not reed this class "satana tam pravit bag"
        [HiddenInput(DisplayValue = false)]
        public int Id { get { return _id; } set { _id = value; } }
        [Required]
        [MinLength(6)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Russian hackers???")]
        [Display(Name = "Name")]
        public string Name { get { return _name; } set { _name = value; } }
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Wrong Email")]
        public string Email { get { return _email; } set { _email = value; } }
        [Display(Name = "Skype")]
        [MinLength(0)]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_.,-]{5,31}$", ErrorMessage = "Wrong Skype Login")]
        public string Skype { get { return _skype; } set { _skype = value; } }
        [Display(Name = "Signatyre")]
        public string Signature { get { return _signature; } set { _signature = value; } }
        public byte[] UserImage { get { return _userImage; } set { _userImage = value; } }
    }
}

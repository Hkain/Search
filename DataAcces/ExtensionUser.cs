namespace DataAcces
{
    public static class ExtensionUser
    {
        public static User ConvetToDUser(this BusnesLogic.User user)
        {
            User u = new User();
            u.Id = user.Id;
            u.Name = user.Name;
            u.Email = user.Email;
            u.Skype = user.Skype;
            u.Signature = user.Signature;
            u.Image = user.UserImage;
            return u;
        }
    }

    public partial class User
    {
        public BusnesLogic.User ConverToBUser()
        {
            return new BusnesLogic.User(Id,Name,Email,Skype,Signature,Image);
        }
    }
}

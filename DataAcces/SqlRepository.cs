using BusnesLogic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces
{
    [InfoAttributes(Name = NameSignatyre.SqlRepository)]
    public class SqlRepository : UserRepository , IDisposable
    {
        private Database db;
        
        public SqlRepository(string conn)
        {
            if (string.IsNullOrEmpty(conn))
            {
                throw new ArgumentNullException("Huston we Have a problem!!! Maybe connect string is empty :( ");
            }
            db = new Database(conn);
        }

        // us need more users
        public override void Add(BusnesLogic.User user)
        {
            try
            {
                db.Users.Add(user.ConvetToDUser());
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string error = "";
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    error += "\nObject: " + validationError.Entry.Entity.ToString();
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        error += "\n" + err.ErrorMessage.ToString();
                    }
                }
                throw new Exception(error);
            }
        }

        public override BusnesLogic.User GetUser(int id)
        {
            return db.Users.Find(id).ConverToBUser();
        }

        public override IEnumerable<BusnesLogic.User> GetUsers()
        {
            var user = (from u in db.Users select u).AsEnumerable();
            return from u in user select u.ConverToBUser(); ;
            
        }

        public override void Update(BusnesLogic.User user)
        {
            try
            {
                var locaUser = (from p in this.db.Users
                                where p.Id == user.Id
                                select p).First();
                locaUser.Name = user.Name;
                locaUser.Email = user.Email;
                locaUser.Skype = user.Skype;
                locaUser.Signature = user.Signature;
                locaUser.Image = user.UserImage;

                db.SaveChanges();
            }
            catch(DbEntityValidationException ex)
            {
                string error = "";
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    error += "\nObject: " + validationError.Entry.Entity.ToString();
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        error +=  "\n" + err.ErrorMessage.ToString();
                    }
                }
                throw new Exception(error);
            }
        }

        // I know this bad practicle :P 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
        }
    }

}

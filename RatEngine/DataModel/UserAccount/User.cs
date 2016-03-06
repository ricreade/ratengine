using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.UserAccount
{
    /// <summary>
    /// This class represents an application user on the client.  This class provides some context
    /// to PlayerCharacter.  Its main purpose is to identify a user on the system.  It can also
    /// provide a means to identify players who are using two characters at once - something that
    /// is usually frowned upon.
    /// </summary>
    public class User : GameElement
    {
        // The user id of the player.  This value remains constant to identify the player.  It may
        // be obsolete as the GameElement.ID already accomplishes this.
        private string _userid;

        // The user name of the player.  This is typically the value associated with the 
        // PlayerCharacter.
        private string _username;

        // The email of the user in case there is a problem in the game.
        private string _email;

        // The class diagram has Password as well, but on second thought, that's probably a bad idea.

        public string UserID
        {
            get { return _userid; }
        }

        public string UserName
        {
            get { return _username; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override void LoadDataRow(System.Data.DataRow Row)
        {
            throw new NotImplementedException();
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }
    }
}

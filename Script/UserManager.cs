using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BL
{
    public class UserManager 
    {
        private static UserManager current;
        private Dictionary<String, User> users;

        public static UserManager Current
        {
            get
            {
                if (current == null)
                {
                    current = new UserManager();
                }

                return current;
            }
        }

        public UserManager()
        {
            this.users = new Dictionary<string, User>();
        }

        public void AddUser(User user)
        {
            this.users[user.Id] = user;
        }

        public User EnsureUser(String id)
        {
            if (this.users.ContainsKey(id))
            {
                return this.users[id];
            }

            User user = new User();

            user.Id = id;

            this.AddUser(user);

            return user;
        }
    }
}

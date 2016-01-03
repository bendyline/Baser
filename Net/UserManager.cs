using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Bendyline.Base
{
    public class UserManager 
    {
        private static UserManager current;
        private Dictionary<String, User> usersByUniqueKey;
        private Dictionary<long, User> usersById;

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
            this.usersByUniqueKey = new Dictionary<string, User>();
            this.usersById = new Dictionary<long, User>();
        }

        public void AddUser(User user)
        {
            if (user.UniqueKey != null)
            {
                this.usersByUniqueKey[user.UniqueKey] = user;
            }

            if (user.Id != null)
            {
                this.usersById[(long)user.Id] = user;
            }
        }

        public User EnsureUserById(long id)
        {
            if (this.usersById.ContainsKey(id))
            {
                return this.usersById[id];
            }

            User user = new User();

            user.Id = id;

            this.AddUser(user);

            return user;
        }

        public User EnsureUserByUniqueKey(String uniqueKey)
        {
            if (this.usersByUniqueKey.ContainsKey(uniqueKey))
            {
                return this.usersByUniqueKey[uniqueKey];
            }

            User user = new User();

            user.UniqueKey = uniqueKey;

            this.AddUser(user);

            return user;
        }
    }
}

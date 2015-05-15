namespace GiveawayApp.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public static class UserRegistry
    {
        private static readonly IDictionary<string, SignalRUser> UserTable = new Dictionary<string, SignalRUser>();

        public static void AddUser(SignalRUser user)
        {
            UserTable[user.UserId] = user;
        }

        public static void RemoveUser(SignalRUser user)
        {
            UserTable.Remove(user.UserId);
        }

        public static SignalRUser GetRandomUser()
        {
            var keys = UserTable.Keys.ToList();
            var userCount = keys.Count;

            var random = RandomHelper.Instance;
            var index = random.Next(userCount);

            var key = keys[index];
            return UserTable[key];
        }
    }
}

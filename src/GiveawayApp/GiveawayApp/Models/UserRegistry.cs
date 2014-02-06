namespace GiveawayApp.Models {
    using System.Collections.Generic;
    using System.Linq;

    public static class UserRegistry {
        private static readonly IDictionary<string, SignalRUser> userTable = new Dictionary<string, SignalRUser>();

        public static void AddUser(SignalRUser user) {
            userTable[user.UserId] = user;
        }

        public static void RemoveUser(SignalRUser user) {
            userTable.Remove(user.UserId);
        }

        public static SignalRUser GetRandomUser() {
            var keys = userTable.Keys.ToList();
            var userCount = keys.Count;

            var random = RandomHelper.Instance;
            var index = random.Next(userCount);

            var key = keys[index];
            return userTable[key];
        }
    }
}

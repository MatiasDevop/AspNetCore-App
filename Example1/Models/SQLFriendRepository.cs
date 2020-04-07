using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.Models
{
    public class SQLFriendRepository : IFriendStore
    {
        private readonly AppDbContext context;
        private List<Friend> friendList;

        public SQLFriendRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Friend delete(int Id)
        {
            Friend friend = context.Friends.Find(Id);
            if (friend != null)
            {
                context.Friends.Remove(friend);
                context.SaveChanges();
            }
            return friend;
        }

        public List<Friend> GetAllFriends()
        {
            friendList = context.Friends.ToList<Friend>();

            return friendList;
        }

        public Friend getFriendData(int Id)
        {
            return context.Friends.Find(Id);
        }

        public Friend modify(Friend friend)
        {
            var employee = context.Friends.Attach(friend);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return friend;
        }

        public Friend nuevo(Friend friend)
        {
            context.Friends.Add(friend);
            context.SaveChanges();
            return friend;
        }
    }
}

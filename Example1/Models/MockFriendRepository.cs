using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.Models
{
    public class MockFriendRepository : IFriendStore
    {
        private List<Friend> friendList;

        public MockFriendRepository()
        {
            friendList = new List<Friend>();
            friendList.Add(new Friend() { Id = 1, Name = "Pedro", City = Province.Alava, Email = "pedro@mail.com" });
            friendList.Add(new Friend() { Id = 2, Name = "Juan", City = Province.Alicante, Email = "juan@mail.com" });
            friendList.Add(new Friend() { Id = 3, Name = "Sara", City = Province.Almeria, Email = "sara@mail.com" });
        }
        public Friend getFriendData(int Id)
        {
            return this.friendList.FirstOrDefault(e => e.Id == Id);
        }
        public List<Friend> GetAllFriends()
        {
            return friendList;
        }

        public Friend nuevo(Friend friend)
        {
            friend.Id = friendList.Max(a => a.Id) + 1;
            friendList.Add(friend);
            return friend;
        }

        public Friend modify(Friend modifyFriend)
        {
            Friend friend = friendList.FirstOrDefault(e => e.Id == modifyFriend.Id);
            if (friend != null)
            {
                friend.Name = modifyFriend.Name;
                friend.Email = modifyFriend.Email;
                friend.City = modifyFriend.City;
            }
            return friend;
        }

        public Friend delete(int Id)
        {
            Friend friend = friendList.FirstOrDefault(e => e.Id == Id);
            if (friend != null)
            {
                friendList.Remove(friend);
            }
            return friend;
        }
    }
}

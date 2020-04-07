using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.Models
{
    public interface IFriendStore
    {
        Friend getFriendData(int Id);
        List<Friend> GetAllFriends();
        Friend nuevo(Friend friend);

        Friend modify(Friend modifyFriend);
        Friend delete(int id);
    }
}

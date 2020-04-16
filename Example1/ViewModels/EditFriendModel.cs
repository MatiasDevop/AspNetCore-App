using Example1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.ViewModels
{
    public class EditFriendModel:CreateFriendModel
    {
        public int Id { get; set; }
        public string routePhotoLast { get; set; }
    }
}

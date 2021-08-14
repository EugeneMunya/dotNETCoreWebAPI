using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotNETCoreWebAPI.Models
{
    public class User
    {

        public int Id{get;set;}
        public string UserName{get;set;}
        public byte[] PasswordHash{get;set;}
        public byte[] HasSalt{get;set;}
        public List<Character> Characters{get;set;}

        [Required]
        public string Role{get;set;}

    }
}
using System.Collections.Generic;

namespace RestWebApi.Entities
{
    public class LoginUserResponse
    {
        public string Token { get; set; }
        public bool Login { get; set; }
        public List<string> Errors { get; set; }
    }
}
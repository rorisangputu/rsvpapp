using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rsvp.data.Models;

namespace rsvp.api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
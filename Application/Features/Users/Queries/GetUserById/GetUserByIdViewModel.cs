using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdViewModel
    {
        public int Id { get; set; }
        public string MyProperty { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

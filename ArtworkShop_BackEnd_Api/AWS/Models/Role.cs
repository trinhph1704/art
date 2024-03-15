using System;
using System.Collections.Generic;

namespace AWS.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<Usertb>();
        }

        public string RoleId { get; set; } = null!;
        public string RoleName { get; set; } = null!;

        public virtual ICollection<Usertb> Users { get; set; }
    }
}

using LxDp.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LxDp.Domain.ViewModels;

public class CreateUserDto
{
    public User User { get; set; }
    public bool IsRootUser { get; set; }
    public ServerViewModel Server { get; set; }
}

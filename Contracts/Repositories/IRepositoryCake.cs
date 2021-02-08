using Bakery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Contracts.Repositories
{
    public interface IRepositoryCake
    {
        List<Cake> GetCakes();
    }
}
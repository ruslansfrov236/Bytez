using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Abstract
{
    public interface IBasketService
    {
        Task<bool> Add(string id , int quantity);
        Task Remove(string id);
    }
}

using bytez.business.Dto.Wishlist;
using bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Abstract
{
    public interface IWishlistService
    {
        Task<List<Wishlist>> GetWishlistsAllAsync();
        Task<bool> Add(CreateWishlistDto model);
        Task Remove(string id);
    }
}

using bytez.business.Dto.Messages;
using bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Abstract
{
    public interface IMessageService
    {
        Task<List<Message>> GetMessagesAllAsync();

        Task<Message> GetMessagesById(string id);
        Task<bool> Create(CreateMessageDto model);
    }
}

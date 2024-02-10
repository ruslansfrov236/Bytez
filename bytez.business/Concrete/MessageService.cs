using bytez.business.Abstract;
using bytez.business.Dto.Messages;
using bytez.data.Abstract;
using bytez.entity.Entities;
using Microsoft.EntityFrameworkCore;


namespace bytez.business.Concrete
{
    public class MessageService : IMessageService
    {
        readonly private IMessageReadRepository _messageReadRepository;
        readonly private IMessageWriteRepository _messageWriteRepository;

        public MessageService(IMessageReadRepository messageReadRepository, IMessageWriteRepository messageWriteRepository)
        {
            _messageReadRepository = messageReadRepository;
            _messageWriteRepository = messageWriteRepository;
        }
        public async Task<bool> Create(CreateMessageDto model)
        {
            Message message = new Message()
            {
                Email = model.Email,
                Name = model.Name,
                MessageInfo = model.MessageInfo,
                Phone = model.Phone

            };

            await _messageWriteRepository.AddAsync(message);
            await _messageWriteRepository.SaveAsync();
            return true;
        }

        public async Task<List<Message>> GetMessagesAllAsync()
        {
            List<Message> messages = await _messageReadRepository.GetAll()
                                                                .OrderByDescending(a => a.CreatedDate)
                                                                .Select(p => new Message
                                                                {
                                                                    Id = p.Id,
                                                                    Name = p.Name,
                                                                    Email = p.Email,
                                                                    MessageInfo = p.MessageInfo,
                                                                    Phone = p.Phone,
                                                                }).ToListAsync();

            return messages;

        }

        public async Task<Message> GetMessagesById(string id)
        => await _messageReadRepository.GetByIdAsync(id);
    }
}

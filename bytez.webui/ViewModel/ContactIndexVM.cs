using bytez.business.Dto.ContactCall;
using bytez.business.Dto.Messages;
using bytez.entity.Entities;

namespace bytez.webui.ViewModel
{
    public class ContactIndexVM
    {
        public ContactCall ContactCall { get; set; }    

        public ContactWall ContactWall { get; set; }

        public List<Email> Emails { get; set; }

        public CreateMessageDto CreateMessageDto { get; set; }
    }
}

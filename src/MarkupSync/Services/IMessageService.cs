using MarkupSync.Models;

namespace MarkupSync.Services
{
    public interface IMessageService
    {
        void SetActiveMessage(Message message);
        Message GetActiveMessage();
    }
}

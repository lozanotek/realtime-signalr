using System;
using System.Windows.Forms;

namespace ChatWindows {
    using System.Configuration;

    public partial class ChatForm : Form {

        private ChatService chatService;

        public ChatForm() {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, EventArgs e) {
            var name = NameTextBox.Text;
            var message = MessageTextBox.Text;

            chatService.Send(name, message);
        }

        private void ChatForm_Load(object sender, EventArgs e) {
            var value = ConfigurationManager.AppSettings["service.Url"];

            if (string.IsNullOrEmpty(value)) {
                return;
            }

            chatService = new ChatService();
            chatService.Connect(value);
            chatService.Received += ChatServiceOnReceived;
        }

        private void ChatServiceOnReceived(string sender, string message) {
            if (ChatTextBox.InvokeRequired) {
                var callback = new SetTextCallback(ChatServiceOnReceived);
                Invoke(callback, new object[] { sender, message });
            }
            else {
                var chatMessage = string.Format("{0}: {1}{2}", sender, message, Environment.NewLine);
                ChatTextBox.AppendText(chatMessage);
            }
        }
    }
}

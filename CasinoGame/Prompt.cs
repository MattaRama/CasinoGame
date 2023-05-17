using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CasinoGame
{
    public class PromptResponse
    {
        public string message { get; private set; }
        public int exitCode { get; private set; }

        public PromptResponse(string message, int exitCode)
        {
            this.message = message;
            this.exitCode = exitCode;
        }
    }

    public static class Prompt
    {
        public static PromptResponse ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            { 
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top=20, Text=text };
            TextBox textBox = new TextBox() { Left = 50, Top=50, Width=400 };
            Button confirmation = new Button() { Text = "Ok", Left=350, Width=100, Top=70, DialogResult = DialogResult.OK };
            Button exit = new Button() { Text = "Exit", Left = 50, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            int exitCode = 0;
            confirmation.Click += (sender, e) => { 
                prompt.Close();
            };
            exit.Click += (sender, e) => {
                prompt.Close();
                exitCode = -1;
            };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(exit);
            prompt.AcceptButton = confirmation;

            return new PromptResponse(
                prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "",
                exitCode
            );
        }
    }
}

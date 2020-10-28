using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Hangman
{
    public class HangmanForm : Form
    {
        public Button button;

        public HangmanForm(){
            create_button();
        }

        //helper method to initialize the button
        private void create_button(){
            button = new Button();
            button.Size = new Size(40, 40);
            button.Location = new Point(30, 30);
            button.Text = "Click me";
            this.Controls.Add(button);
            button.Click += new EventHandler(button_Click);
        }

        private void button_Click(object sender, EventArgs e){
            MessageBox.Show("Hey ya'll");
        }

        [STAThread]
        static void Main(){
            Application.EnableVisualStyles();
            Application.Run(new HangmanForm());
        }
    }
}

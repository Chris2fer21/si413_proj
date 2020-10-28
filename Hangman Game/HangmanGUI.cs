using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Hangman
{
    public class HangmanForm : Form
    {
        private Button button;
        private Button button2;

        public HangmanForm(){
            this.StartPosition = FormStartPosition.CenterScreen;
            create_button();
            create_button2();
            this.Size = new Size(500, 500);
        }

        //helper method to initialize the button
        private void create_button(){
            button = new Button();
            button.Size = new Size(40, 40);
            button.Location = new Point(this.Size.Width - 40, 30);
            button.Text = "Click me";
            this.Controls.Add(button);
            button.Click += new EventHandler(button_Click);
        }
        
        //helper method to initialize the button
        private void create_button2(){
            button2 = new Button();
            button2.Size = new Size(40, 40);
            button2.Location = new Point(50, 30);
            button2.Padding = new Padding(10);
            button2.Text = "Click me";
            this.Controls.Add(button2);
            button2.Click += new EventHandler(button_Click);
        }

        private void update_Button(){
            button.Location = new Point(this.Size.Width - 41, 30);
        } 

        protected override void OnResize(EventArgs e){
            base.OnResize(e);
            update_Button();
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

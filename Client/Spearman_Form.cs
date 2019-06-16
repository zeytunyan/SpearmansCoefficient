using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Security.Principal;

namespace Client
{
    public partial class Spearman_Form : Form
    {
        /// <summary>Initializes a new instance of the <see cref="T:Client.Spearman_Form"/> class.</summary>
        public Spearman_Form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the OK button on first window. Displays columns for entering variables that are filled by the user.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FirstOk_Click(object sender, EventArgs e)
        {
            int numberOfVars = Convert.ToInt32(this.Number_vars.Value);
            Controls.Clear();

            AddLabel(20, new Point(60, 20), "Label_X", "X");

            TextBox[] tbX = new TextBox[numberOfVars];
            for (int i = 0; i < tbX.Length; i++)
            {
                tbX[i] = new TextBox();
                tbX[i].Location = new Point(20, 50 + i * 35);
                tbX[i].Name = "X" + i.ToString();
                tbX[i].Size = new Size(100, 20);
                tbX[i].KeyPress += textBox_KeyPress;
                Controls.Add(tbX[i]);
            }

            AddLabel(20, new Point(180, 20), "Label_Y", "Y");

            TextBox[] tbY = new TextBox[numberOfVars];
            for (int i = 0; i < tbX.Length; i++)
            {
                tbY[i] = new TextBox();
                tbY[i].Location = new Point(140, 50 + i * 35);
                tbY[i].Name = "Y" + i.ToString();
                tbY[i].Size = new Size(100, 20);
                tbY[i].KeyPress += textBox_KeyPress;
                Controls.Add(tbY[i]);
            }

            Button SecondOK = new Button();
            SecondOK.Name = "SecondOK";
            SecondOK.Text = "OK";
            SecondOK.Location = new Point(92, 60 + numberOfVars * 35);
            SecondOK.Click += SecondOk_Click;
            Controls.Add(SecondOK);

            this.Width = 275;
            this.Height = numberOfVars * 35 + 150;
        }

        /// <summary>
        /// Handles the KeyPress event of the textBox control.
        /// Checks if the user has entered a number.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        /// <summary>
        /// Handles the Click event of the OK button on a second window.
        /// Sends entered data to the server and receiving a response, displays it in the window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SecondOk_Click(object sender, EventArgs e)
        {
            string[] xValues = new string[Convert.ToInt32(Number_vars.Value)];
            string[] yValues = new string[Convert.ToInt32(Number_vars.Value)];
            for (int i = 0; i < xValues.Length; i++)
            {
                TextBox TxtBox = Controls["X" + i.ToString()] as TextBox;
                xValues[i] = TxtBox.Text;

                TxtBox = Controls["Y" + i.ToString()] as TextBox;
                yValues[i] = TxtBox.Text;
            }

            string message = String.Join(",", xValues) + "|" + String.Join(",", yValues);
            string name = WindowsIdentity.GetCurrent().Name;

            ClientObject clientObject = new ClientObject(IPAddress.Loopback, 8001);
            clientObject.Send(message);
            clientObject.Send(name);
            string answer = clientObject.Recieve();
            clientObject.Close();

            Controls.Clear();
            this.Width = 500;
            this.Height = 160;

            char[] chars = { '=', '>' };
            string[] pair = answer.Split(chars, StringSplitOptions.RemoveEmptyEntries);

            AddLabel(64, new Point(15, 10), "Label_Conclusion", "Вывод:");
            AddLabel(350, new Point(15, 50), "Coefficient", "Коэффициент Спирмена: " + pair[0]);
            AddLabel(425, new Point(15, 80), "Conclusion", pair[1].Trim());

        }

        /// <summary>Adds the label with the specified text.</summary>
        /// <param name="width">Label width.</param>
        /// <param name="location">  Label location.</param>
        /// <param name="name">Label name.</param>
        /// <param name="text">  Label text.</param>
        private void AddLabel(int width, Point location, string name, string text) {
            Label newLbl = new Label();
            newLbl.Height = 20;
            newLbl.Width = width;
            newLbl.Font = new Font("Microsoft Sans Serif", 12);
            newLbl.Location = location;
            newLbl.Name = name;
            newLbl.Text = text;
            Controls.Add(newLbl);
        }
    }
}

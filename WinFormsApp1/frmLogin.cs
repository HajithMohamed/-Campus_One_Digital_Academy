using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class frmLogin : Form
    {
        // Hard-coded administrator credentials as required by the assignment.
        private const string AdminUsername = "Admin";
        private const string AdminPassword = "Campusone@123";

        private PictureBox picLogo;
        private Label lblTitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnClear;
        private Button btnExit;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Login - Campus One Digital Academy";
            this.Size = new Size(500, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title label
            lblTitle = new Label();
            lblTitle.Text = "Campus One Digital Academy";
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(80, 20);
            this.Controls.Add(lblTitle);

            // Logo picture box
            picLogo = new PictureBox();
            picLogo.Size = new Size(120, 120);
            picLogo.Location = new Point(180, 60);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.BackColor = Color.LightGray;
            // Placeholder text when no image is available.
            picLogo.Paint += (sender, e) =>
            {
                if (picLogo.Image == null)
                {
                                    TextRenderer.DrawText(e.Graphics, "LOGO", new Font("Segoe UI", 12, FontStyle.Bold),
                        new Rectangle(0, 0, picLogo.Width, picLogo.Height), Color.DimGray,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            };
            this.Controls.Add(picLogo);

            // Username label
            lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(80, 210);
            this.Controls.Add(lblUsername);

            // Username text box
            txtUsername = new TextBox();
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(300, 25);
            txtUsername.Location = new Point(80, 235);
            this.Controls.Add(txtUsername);

            // Password label
            lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(80, 280);
            this.Controls.Add(lblPassword);

            // Password text box
            txtPassword = new TextBox();
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(300, 25);
            txtPassword.Location = new Point(80, 305);
            txtPassword.PasswordChar = '*';
            txtPassword.UseSystemPasswordChar = true;
            this.Controls.Add(txtPassword);

            // Login button
            btnLogin = new Button();
            btnLogin.Name = "btnLogin";
            btnLogin.Text = "Login";
            btnLogin.Size = new Size(90, 35);
            btnLogin.Location = new Point(80, 380);
            btnLogin.Click += new EventHandler(this.btnLogin_Click);
            this.Controls.Add(btnLogin);

            // Clear button
            btnClear = new Button();
            btnClear.Name = "btnClear";
            btnClear.Text = "Clear";
            btnClear.Size = new Size(90, 35);
            btnClear.Location = new Point(185, 380);
            btnClear.Click += new EventHandler(this.btnClear_Click);
            this.Controls.Add(btnClear);

            // Exit button
            btnExit = new Button();
            btnExit.Name = "btnExit";
            btnExit.Text = "Exit";
            btnExit.Size = new Size(90, 35);
            btnExit.Location = new Point(290, 380);
            btnExit.Click += new EventHandler(this.btnExit_Click);
            this.Controls.Add(btnExit);

            // Set initial focus to username field.
            this.Load += (sender, e) => txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validation: both fields empty.
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Username and Password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            // Validation: username empty.
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter your Username.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            // Validation: password empty.
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your Password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Validate credentials against the hard-coded administrator credentials.
            if (username == AdminUsername && password == AdminPassword)
            {
                this.Hide();

                // Open RegistrationForm if it exists.
                frmRegistration registrationForm = new frmRegistration();
                registrationForm.FormClosed += (s, args) => this.Close();
                registrationForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password. Please try again.", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}

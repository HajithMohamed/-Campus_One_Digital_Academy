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
        private GroupBox grpLogin;
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
            this.ClientSize = new Size(520, 430);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(250, 247, 248);
            this.Font = new Font("Segoe UI", 9F);
            SetWindowIcon(this);

            // Title label
            lblTitle = new Label();
            lblTitle.Text = "Campus One Digital Academy";
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(103, 118);
            this.Controls.Add(lblTitle);

            // Logo picture box
            picLogo = new PictureBox();
            picLogo.Size = new Size(100, 88);
            picLogo.Location = new Point(210, 18);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            string logoPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Assets", "CampusOneLogo.png");
            if (System.IO.File.Exists(logoPath))
                picLogo.Image = Image.FromFile(logoPath);
            this.Controls.Add(picLogo);

            grpLogin = new GroupBox();
            grpLogin.Text = "Login";
            grpLogin.Location = new Point(90, 170);
            grpLogin.Size = new Size(340, 170);
            this.Controls.Add(grpLogin);

            // Username label
            lblUsername = new Label();
            lblUsername.Text = "Username";
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(36, 45);
            grpLogin.Controls.Add(lblUsername);

            // Username text box
            txtUsername = new TextBox();
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(185, 27);
            txtUsername.Location = new Point(120, 40);
            grpLogin.Controls.Add(txtUsername);

            // Password label
            lblPassword = new Label();
            lblPassword.Text = "Password";
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(36, 84);
            grpLogin.Controls.Add(lblPassword);

            // Password text box
            txtPassword = new TextBox();
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(185, 27);
            txtPassword.Location = new Point(120, 79);
            txtPassword.PasswordChar = '*';
            txtPassword.UseSystemPasswordChar = true;
            grpLogin.Controls.Add(txtPassword);

            // Login button
            btnLogin = new Button();
            btnLogin.Name = "btnLogin";
            btnLogin.Text = "Login";
            btnLogin.Size = new Size(70, 30);
            btnLogin.Location = new Point(235, 123);
            btnLogin.Click += new EventHandler(this.btnLogin_Click);
            grpLogin.Controls.Add(btnLogin);

            // Clear button
            btnClear = new Button();
            btnClear.Name = "btnClear";
            btnClear.Text = "Clear";
            btnClear.Size = new Size(70, 30);
            btnClear.Location = new Point(36, 123);
            btnClear.Click += new EventHandler(this.btnClear_Click);
            grpLogin.Controls.Add(btnClear);

            // Exit button
            btnExit = new Button();
            btnExit.Name = "btnExit";
            btnExit.Text = "Exit";
            btnExit.Size = new Size(70, 30);
            btnExit.Location = new Point(12, 385);
            btnExit.Click += new EventHandler(this.btnExit_Click);
            this.Controls.Add(btnExit);

            // Set initial focus to username field.
            this.Load += (sender, e) => txtUsername.Focus();
        }

        private static void SetWindowIcon(Form form)
        {
            string iconPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Assets", "CampusOneLogo.ico");
            if (System.IO.File.Exists(iconPath))
                form.Icon = new Icon(iconPath);
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

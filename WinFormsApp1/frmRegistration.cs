using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class frmRegistration : Form
    {
        // Connection string to connect to the Student database on SQL Server (LocalDB/SQLEXPRESS).
        private readonly string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";

        // Form controls
        private Label lblTitle;
        private PictureBox picLogo;
        private GroupBox grpStudentRegistration;
        private Label lblRegNo;
        private ComboBox cmbRegNo;
        private GroupBox grpBasicDetails;
        private Label lblFirstName;
        private TextBox txtFirstName;
        private Label lblLastName;
        private TextBox txtLastName;
        private Label lblDateOfBirth;
        private DateTimePicker dtpDateOfBirth;
        private Label lblGender;
        private RadioButton rdoMale;
        private RadioButton rdoFemale;
        private Label lblAddress;
        private TextBox txtAddress;
        private GroupBox grpContactDetails;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblMobilePhone;
        private TextBox txtMobilePhone;
        private Label lblHomePhone;
        private TextBox txtHomePhone;
        private GroupBox grpParentDetails;
        private Label lblParentName;
        private TextBox txtParentName;
        private Label lblNIC;
        private TextBox txtNIC;
        private Label lblContactNo;
        private TextBox txtContactNo;
        private Button btnRegister;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnViewRecords;
        private LinkLabel lnkLogout;
        private LinkLabel lnkExit;
        private ErrorProvider validationErrors;

        public frmRegistration()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Student Registration - Campus One Digital Academy";
            this.ClientSize = new Size(600, 740);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(250, 247, 248);
            this.Font = new Font("Segoe UI", 9F);
            SetWindowIcon(this);
            validationErrors = new ErrorProvider { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };

            // Title label
            lblTitle = new Label();
            lblTitle.Text = "Campus One Digital Academy";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(112, 12);
            this.Controls.Add(lblTitle);

            // Logo picture box
            picLogo = new PictureBox();
            picLogo.Visible = false;
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            LoadLogo(picLogo);
            this.Controls.Add(picLogo);

            // Student Registration group
            grpStudentRegistration = new GroupBox();
            grpStudentRegistration.Text = "Student Registration";
            grpStudentRegistration.Location = new Point(10, 58);
            grpStudentRegistration.Size = new Size(580, 72);
            this.Controls.Add(grpStudentRegistration);

            lblRegNo = new Label();
            lblRegNo.Text = "Reg No:";
            lblRegNo.AutoSize = true;
            lblRegNo.Location = new Point(40, 31);
            grpStudentRegistration.Controls.Add(lblRegNo);

            cmbRegNo = new ComboBox();
            cmbRegNo.Name = "cmbRegNo";
            cmbRegNo.Size = new Size(150, 27);
            cmbRegNo.Location = new Point(120, 27);
            cmbRegNo.DropDownStyle = ComboBoxStyle.DropDown;
            cmbRegNo.SelectedIndexChanged += new EventHandler(this.cmbRegNo_SelectedIndexChanged);
            cmbRegNo.Leave += new EventHandler(this.cmbRegNo_Leave);
            grpStudentRegistration.Controls.Add(cmbRegNo);

            // Basic Details group
            grpBasicDetails = new GroupBox();
            grpBasicDetails.Text = "Basic Details";
            grpBasicDetails.Location = new Point(28, 140);
            grpBasicDetails.Size = new Size(544, 180);
            this.Controls.Add(grpBasicDetails);

            lblFirstName = new Label();
            lblFirstName.Text = "First Name:";
            lblFirstName.AutoSize = true;
            lblFirstName.Location = new Point(20, 29);
            grpBasicDetails.Controls.Add(lblFirstName);

            txtFirstName = new TextBox();
            txtFirstName.Name = "txtFirstName";
            txtFirstName.MaxLength = 50;
            txtFirstName.Size = new Size(400, 27);
            txtFirstName.Location = new Point(120, 27);
            grpBasicDetails.Controls.Add(txtFirstName);

            lblLastName = new Label();
            lblLastName.Text = "Last Name:";
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(20, 67);
            grpBasicDetails.Controls.Add(lblLastName);

            txtLastName = new TextBox();
            txtLastName.Name = "txtLastName";
            txtLastName.MaxLength = 50;
            txtLastName.Size = new Size(400, 27);
            txtLastName.Location = new Point(120, 63);
            grpBasicDetails.Controls.Add(txtLastName);

            lblDateOfBirth = new Label();
            lblDateOfBirth.Text = "Date of Birth:";
            lblDateOfBirth.AutoSize = true;
            lblDateOfBirth.Location = new Point(20, 103);
            grpBasicDetails.Controls.Add(lblDateOfBirth);

            dtpDateOfBirth = new DateTimePicker();
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(180, 27);
            dtpDateOfBirth.Location = new Point(120, 99);
            dtpDateOfBirth.Format = DateTimePickerFormat.Short;
            grpBasicDetails.Controls.Add(dtpDateOfBirth);

            lblGender = new Label();
            lblGender.Text = "Gender:";
            lblGender.AutoSize = true;
            lblGender.Location = new Point(20, 139);
            grpBasicDetails.Controls.Add(lblGender);

            rdoMale = new RadioButton();
            rdoMale.Name = "rdoMale";
            rdoMale.Text = "Male";
            rdoMale.AutoSize = true;
            rdoMale.Location = new Point(120, 137);
            grpBasicDetails.Controls.Add(rdoMale);

            rdoFemale = new RadioButton();
            rdoFemale.Name = "rdoFemale";
            rdoFemale.Text = "Female";
            rdoFemale.AutoSize = true;
            rdoFemale.Location = new Point(220, 137);
            grpBasicDetails.Controls.Add(rdoFemale);

            lblAddress = new Label();
            lblAddress.Text = "Address:";
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(20, 33);

            // Contact Details group
            grpContactDetails = new GroupBox();
            grpContactDetails.Text = "Contact Details";
            grpContactDetails.Location = new Point(28, 330);
            grpContactDetails.Size = new Size(544, 175);
            this.Controls.Add(grpContactDetails);

            lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.AutoSize = true;
            txtAddress = new TextBox();
            txtAddress.Name = "txtAddress";
            txtAddress.MaxLength = 50;
            txtAddress.Multiline = true;
            txtAddress.Size = new Size(400, 50);
            txtAddress.Location = new Point(120, 25);
            grpContactDetails.Controls.Add(lblAddress);
            grpContactDetails.Controls.Add(txtAddress);

            lblEmail.Location = new Point(20, 87);
            grpContactDetails.Controls.Add(lblEmail);

            txtEmail = new TextBox();
            txtEmail.Name = "txtEmail";
            txtEmail.MaxLength = 50;
            txtEmail.Size = new Size(400, 27);
            txtEmail.Location = new Point(120, 83);
            grpContactDetails.Controls.Add(txtEmail);

            lblMobilePhone = new Label();
            lblMobilePhone.Text = "Mobile Phone:";
            lblMobilePhone.AutoSize = true;
            lblMobilePhone.Location = new Point(20, 128);
            grpContactDetails.Controls.Add(lblMobilePhone);

            txtMobilePhone = new TextBox();
            txtMobilePhone.Name = "txtMobilePhone";
            txtMobilePhone.MaxLength = 10;
            txtMobilePhone.Size = new Size(145, 27);
            txtMobilePhone.Location = new Point(120, 123);
            grpContactDetails.Controls.Add(txtMobilePhone);

            lblHomePhone = new Label();
            lblHomePhone.Text = "Home Phone:";
            lblHomePhone.AutoSize = true;
            lblHomePhone.Location = new Point(300, 128);
            grpContactDetails.Controls.Add(lblHomePhone);

            txtHomePhone = new TextBox();
            txtHomePhone.Name = "txtHomePhone";
            txtHomePhone.MaxLength = 10;
            txtHomePhone.Size = new Size(130, 27);
            txtHomePhone.Location = new Point(390, 123);
            grpContactDetails.Controls.Add(txtHomePhone);

            // Parent Details group
            grpParentDetails = new GroupBox();
            grpParentDetails.Text = "Parent Details";
            grpParentDetails.Location = new Point(28, 515);
            grpParentDetails.Size = new Size(544, 145);
            this.Controls.Add(grpParentDetails);

            lblParentName = new Label();
            lblParentName.Text = "Parent Name:";
            lblParentName.AutoSize = true;
            lblParentName.Location = new Point(20, 35);
            grpParentDetails.Controls.Add(lblParentName);

            txtParentName = new TextBox();
            txtParentName.Name = "txtParentName";
            txtParentName.MaxLength = 50;
            txtParentName.Size = new Size(400, 27);
            txtParentName.Location = new Point(120, 29);
            grpParentDetails.Controls.Add(txtParentName);

            lblNIC = new Label();
            lblNIC.Text = "NIC:";
            lblNIC.AutoSize = true;
            lblNIC.Location = new Point(20, 73);
            grpParentDetails.Controls.Add(lblNIC);

            txtNIC = new TextBox();
            txtNIC.Name = "txtNIC";
            txtNIC.MaxLength = 50;
            txtNIC.Size = new Size(160, 27);
            txtNIC.Location = new Point(120, 67);
            grpParentDetails.Controls.Add(txtNIC);

            lblContactNo = new Label();
            lblContactNo.Text = "Contact No:";
            lblContactNo.AutoSize = true;
            lblContactNo.Location = new Point(20, 109);
            grpParentDetails.Controls.Add(lblContactNo);

            txtContactNo = new TextBox();
            txtContactNo.Name = "txtContactNo";
            txtContactNo.MaxLength = 10;
            txtContactNo.Size = new Size(160, 27);
            txtContactNo.Location = new Point(120, 103);
            grpParentDetails.Controls.Add(txtContactNo);

            // Action buttons
            btnRegister = new Button();
            btnRegister.Name = "btnRegister";
            btnRegister.Text = "Register";
            btnRegister.Size = new Size(75, 30);
            btnRegister.Location = new Point(28, 675);
            btnRegister.Click += new EventHandler(this.btnRegister_Click);
            this.Controls.Add(btnRegister);

            btnUpdate = new Button();
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Text = "Update";
            btnUpdate.Size = new Size(75, 30);
            btnUpdate.Location = new Point(112, 675);
            btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
            this.Controls.Add(btnUpdate);

            btnDelete = new Button();
            btnDelete.Name = "btnDelete";
            btnDelete.Text = "Delete";
            btnDelete.Size = new Size(75, 30);
            btnDelete.Location = new Point(497, 675);
            btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.Controls.Add(btnDelete);

            btnClear = new Button();
            btnClear.Name = "btnClear";
            btnClear.Text = "Clear";
            btnClear.Size = new Size(75, 30);
            btnClear.Location = new Point(413, 675);
            btnClear.Click += new EventHandler(this.btnClear_Click);
            this.Controls.Add(btnClear);

            btnViewRecords = new Button();
            btnViewRecords.Name = "btnViewRecords";
            btnViewRecords.Text = "View Students";
            btnViewRecords.Size = new Size(120, 30);
            btnViewRecords.Location = new Point(240, 675);
            btnViewRecords.Click += new EventHandler(this.btnViewRecords_Click);
            this.Controls.Add(btnViewRecords);

            // Link labels
            lnkLogout = new LinkLabel();
            lnkLogout.Name = "lnkLogout";
            lnkLogout.Text = "Logout";
            lnkLogout.AutoSize = true;
            lnkLogout.Location = new Point(12, 32);
            lnkLogout.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkLogout_LinkClicked);
            this.Controls.Add(lnkLogout);

            lnkExit = new LinkLabel();
            lnkExit.Name = "lnkExit";
            lnkExit.Text = "Exit";
            lnkExit.AutoSize = true;
            lnkExit.Location = new Point(562, 718);
            lnkExit.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkExit_LinkClicked);
            this.Controls.Add(lnkExit);

            this.Load += new EventHandler(this.frmRegistration_Load);
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            if (!EnsureDatabaseSchema())
                return;
            // Populate registration numbers from the database when the form loads.
            LoadRegistrationNumbers();
            ClearAllFields();
            txtFirstName.Focus();
        }

        private static void LoadLogo(PictureBox pictureBox)
        {
            string logoPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Assets", "CampusOneLogo.png");
            if (System.IO.File.Exists(logoPath))
                pictureBox.Image = Image.FromFile(logoPath);
        }

        private static void SetWindowIcon(Form form)
        {
            string iconPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Assets", "CampusOneLogo.ico");
            if (System.IO.File.Exists(iconPath))
                form.Icon = new Icon(iconPath);
        }

        private bool EnsureDatabaseSchema()
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString) { InitialCatalog = "master" };
                using (var master = new SqlConnection(builder.ConnectionString))
                {
                    master.Open();
                    using var createDatabase = new SqlCommand("IF DB_ID(N'Student') IS NULL CREATE DATABASE Student", master);
                    createDatabase.ExecuteNonQuery();
                }

                using var connection = new SqlConnection(connectionString);
                connection.Open();
                const string sql = @"IF OBJECT_ID(N'dbo.Registration', N'U') IS NULL
BEGIN
 CREATE TABLE dbo.Registration (
  regNo INT NOT NULL PRIMARY KEY, firstName VARCHAR(50) NOT NULL, lastName VARCHAR(50) NOT NULL,
  dateOfBirth DATETIME NOT NULL, gender VARCHAR(50) NOT NULL, address VARCHAR(50) NOT NULL,
  email VARCHAR(50) NOT NULL, mobilePhone INT NOT NULL, homePhone INT NOT NULL,
  parentName VARCHAR(50) NOT NULL, nic VARCHAR(50) NOT NULL, contactNo INT NOT NULL
 )
END";
                using var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not prepare the Student database.\n\n{ex.Message}\n\nMake sure SQL Server Express is installed and the SQLEXPRESS service is running.",
                    "Database connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Loads existing registration numbers (regNo) from the Registration table into the ComboBox.
        /// </summary>
        private void LoadRegistrationNumbers()
        {
            cmbRegNo.Items.Clear();
            cmbRegNo.Text = string.Empty;

            try
            {
                // SELECT query retrieves all regNo values from the Registration table.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT regNo FROM Registration ORDER BY regNo";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbRegNo.Items.Add(reader["regNo"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading registration numbers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Fetches student data from the Registration table based on the selected regNo and fills the form fields.
        /// </summary>
        private void LoadStudentByRegNo(string regNo)
        {
            try
            {
                // SELECT query retrieves the student record matching the provided regNo.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Registration WHERE regNo = @regNo";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@regNo", regNo);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtFirstName.Text = reader["firstName"].ToString();
                                txtLastName.Text = reader["lastName"].ToString();
                                dtpDateOfBirth.Value = Convert.ToDateTime(reader["dateOfBirth"]);

                                string gender = reader["gender"].ToString();
                                rdoMale.Checked = (gender == "Male");
                                rdoFemale.Checked = (gender == "Female");

                                txtAddress.Text = reader["address"].ToString();
                                txtEmail.Text = reader["email"].ToString();
                                txtMobilePhone.Text = reader["mobilePhone"].ToString();
                                txtHomePhone.Text = reader["homePhone"].ToString();
                                txtParentName.Text = reader["parentName"].ToString();
                                txtNIC.Text = reader["nic"].ToString();
                                txtContactNo.Text = reader["contactNo"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Checks whether the provided registration number already exists in the database.
        /// </summary>
        private bool RegNoExists(string regNo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT COUNT(*) FROM Registration WHERE regNo = @regNo";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@regNo", regNo);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking registration number: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void cmbRegNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRegNo.SelectedItem != null)
            {
                LoadStudentByRegNo(cmbRegNo.SelectedItem.ToString());
            }
        }

        private void cmbRegNo_Leave(object sender, EventArgs e)
        {
            // Fetch data if the user types a value and leaves the ComboBox.
            if (!string.IsNullOrEmpty(cmbRegNo.Text))
            {
                LoadStudentByRegNo(cmbRegNo.Text.Trim());
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Validate that all required fields are filled.
            if (!ValidateFields())
                return;

            string regNo = cmbRegNo.Text.Trim();

            // Check whether the Reg No already exists.
            if (RegNoExists(regNo))
            {
                MessageBox.Show("The entered Reg No already exists. Please use a different Reg No.", "Duplicate Reg No",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRegNo.Focus();
                return;
            }

            try
            {
                // INSERT query adds a new student record into the Registration table using parameters.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO Registration 
                                    (regNo, firstName, lastName, dateOfBirth, gender, address, email, mobilePhone, homePhone, parentName, nic, contactNo)
                                    VALUES 
                                    (@regNo, @firstName, @lastName, @dateOfBirth, @gender, @address, @email, @mobilePhone, @homePhone, @parentName, @nic, @contactNo)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        AddStudentParameters(command);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Record Registered Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRegistrationNumbers();
                ClearAllFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registering record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Validate that all required fields are filled.
            if (!ValidateFields())
                return;

            try
            {
                // UPDATE query modifies the existing student record based on regNo.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"UPDATE Registration SET
                                    firstName = @firstName,
                                    lastName = @lastName,
                                    dateOfBirth = @dateOfBirth,
                                    gender = @gender,
                                    address = @address,
                                    email = @email,
                                    mobilePhone = @mobilePhone,
                                    homePhone = @homePhone,
                                    parentName = @parentName,
                                    nic = @nic,
                                    contactNo = @contactNo
                                    WHERE regNo = @regNo";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        AddStudentParameters(command);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadRegistrationNumbers();
                            ClearAllFields();
                        }
                        else
                        {
                            MessageBox.Show("No matching record found to update.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbRegNo.Text))
            {
                MessageBox.Show("Please select or enter a Registration Number to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            try
            {
                // DELETE query removes the student record where regNo matches.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM Registration WHERE regNo = @regNo";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@regNo", cmbRegNo.Text.Trim());
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadRegistrationNumbers();
                            ClearAllFields();
                        }
                        else
                        {
                            MessageBox.Show("No matching record found to delete.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
            txtFirstName.Focus();
        }

        private void btnViewRecords_Click(object sender, EventArgs e)
        {
            using var recordsForm = new frmStudentRecords(connectionString);
            if (recordsForm.ShowDialog(this) == DialogResult.OK && recordsForm.SelectedRegNo.HasValue)
            {
                cmbRegNo.Text = recordsForm.SelectedRegNo.Value.ToString();
                LoadStudentByRegNo(cmbRegNo.Text);
            }
        }

        private void lnkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Close registration form and return to login form.
            this.Hide();
            frmLogin loginForm = new frmLogin();
            loginForm.FormClosed += (s, args) => this.Close();
            loginForm.Show();
        }

        private void lnkExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Confirm before exiting the application.
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Adds parameters for student data to the provided SqlCommand.
        /// </summary>
        private void AddStudentParameters(SqlCommand command)
        {
            command.Parameters.Add("@regNo", System.Data.SqlDbType.Int).Value = int.Parse(cmbRegNo.Text.Trim());
            command.Parameters.AddWithValue("@firstName", txtFirstName.Text.Trim());
            command.Parameters.AddWithValue("@lastName", txtLastName.Text.Trim());
            command.Parameters.Add("@dateOfBirth", System.Data.SqlDbType.DateTime).Value = dtpDateOfBirth.Value;
            command.Parameters.AddWithValue("@gender", rdoMale.Checked ? "Male" : (rdoFemale.Checked ? "Female" : string.Empty));
            command.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
            command.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            command.Parameters.Add("@mobilePhone", System.Data.SqlDbType.Int).Value = int.Parse(txtMobilePhone.Text.Trim());
            command.Parameters.Add("@homePhone", System.Data.SqlDbType.Int).Value = int.Parse(txtHomePhone.Text.Trim());
            command.Parameters.AddWithValue("@parentName", txtParentName.Text.Trim());
            command.Parameters.AddWithValue("@nic", txtNIC.Text.Trim());
            command.Parameters.Add("@contactNo", System.Data.SqlDbType.Int).Value = int.Parse(txtContactNo.Text.Trim());
        }

        /// <summary>
        /// Validates that all required fields are filled.
        /// </summary>
        private bool ValidateFields()
        {
            validationErrors.Clear();
            Control invalidControl = null;

            void Require(Control control, bool invalid, string message)
            {
                if (!invalid) return;
                validationErrors.SetError(control, message);
                invalidControl ??= control;
            }

            Require(cmbRegNo, !int.TryParse(cmbRegNo.Text.Trim(), out int regNo) || regNo <= 0,
                "Enter a positive numeric registration number.");
            Require(txtFirstName, string.IsNullOrWhiteSpace(txtFirstName.Text), "First name is required.");
            Require(txtLastName, string.IsNullOrWhiteSpace(txtLastName.Text), "Last name is required.");
            Require(dtpDateOfBirth, dtpDateOfBirth.Value.Date > DateTime.Today, "Date of birth cannot be in the future.");
            Require(rdoFemale, !rdoMale.Checked && !rdoFemale.Checked, "Select a gender.");
            Require(txtAddress, string.IsNullOrWhiteSpace(txtAddress.Text), "Address is required.");

            bool validEmail = System.Net.Mail.MailAddress.TryCreate(txtEmail.Text.Trim(), out var emailAddress)
                && emailAddress.Address == txtEmail.Text.Trim();
            Require(txtEmail, !validEmail, "Enter a valid email address.");
            Require(txtMobilePhone, !IsValidPhoneNumber(txtMobilePhone.Text), "Enter a numeric phone number with up to 10 digits.");
            Require(txtHomePhone, !IsValidPhoneNumber(txtHomePhone.Text), "Enter a numeric phone number with up to 10 digits.");
            Require(txtParentName, string.IsNullOrWhiteSpace(txtParentName.Text), "Parent name is required.");
            Require(txtNIC, string.IsNullOrWhiteSpace(txtNIC.Text), "NIC is required.");
            Require(txtContactNo, !IsValidPhoneNumber(txtContactNo.Text), "Enter a numeric phone number with up to 10 digits.");

            if (invalidControl == null) return true;
            MessageBox.Show("Please correct the highlighted fields before continuing.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            invalidControl.Focus();
            return false;
        }

        private static bool IsValidPhoneNumber(string value)
        {
            string text = value.Trim();
            return text.Length >= 7 && text.Length <= 10 && int.TryParse(text, out int number) && number >= 0;
        }

        /// <summary>
        /// Clears all input fields and resets the ComboBox selection.
        /// </summary>
        private void ClearAllFields()
        {
            validationErrors.Clear();
            cmbRegNo.Text = string.Empty;
            txtFirstName.Clear();
            txtLastName.Clear();
            dtpDateOfBirth.Value = DateTime.Now;
            rdoMale.Checked = false;
            rdoFemale.Checked = false;
            txtAddress.Clear();
            txtEmail.Clear();
            txtMobilePhone.Clear();
            txtHomePhone.Clear();
            txtParentName.Clear();
            txtNIC.Clear();
            txtContactNo.Clear();
        }
    }
}

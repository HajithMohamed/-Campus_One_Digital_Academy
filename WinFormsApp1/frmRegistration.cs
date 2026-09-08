using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class frmRegistration : Form
    {
        // Connection string to connect to the Student database on SQL Server (LocalDB/SQLEXPRESS).
        private readonly string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True;Trust Server Certificate=True";

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
        private LinkLabel lnkLogout;
        private LinkLabel lnkExit;

        public frmRegistration()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Student Registration - Campus One Digital Academy";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title label
            lblTitle = new Label();
            lblTitle.Text = "Campus One Digital Academy";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkSlateBlue;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(300, 15);
            this.Controls.Add(lblTitle);

            // Logo picture box
            picLogo = new PictureBox();
            picLogo.Size = new Size(80, 80);
            picLogo.Location = new Point(210, 5);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.BackColor = Color.LightGray;
            picLogo.Paint += (sender, e) =>
            {
                if (picLogo.Image == null)
                {
                    TextRenderer.DrawText(e.Graphics, "LOGO", new Font("Segoe UI", 10, FontStyle.Bold),
                        new Rectangle(0, 0, picLogo.Width, picLogo.Height), Color.DimGray,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            };
            this.Controls.Add(picLogo);

            // Student Registration group
            grpStudentRegistration = new GroupBox();
            grpStudentRegistration.Text = "Student Registration";
            grpStudentRegistration.Location = new Point(30, 90);
            grpStudentRegistration.Size = new Size(870, 80);
            grpStudentRegistration.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.Controls.Add(grpStudentRegistration);

            lblRegNo = new Label();
            lblRegNo.Text = "Reg No:";
            lblRegNo.AutoSize = true;
            lblRegNo.Location = new Point(20, 35);
            grpStudentRegistration.Controls.Add(lblRegNo);

            cmbRegNo = new ComboBox();
            cmbRegNo.Name = "cmbRegNo";
            cmbRegNo.Size = new Size(200, 25);
            cmbRegNo.Location = new Point(90, 32);
            cmbRegNo.DropDownStyle = ComboBoxStyle.DropDown;
            cmbRegNo.SelectedIndexChanged += new EventHandler(this.cmbRegNo_SelectedIndexChanged);
            cmbRegNo.Leave += new EventHandler(this.cmbRegNo_Leave);
            grpStudentRegistration.Controls.Add(cmbRegNo);

            // Basic Details group
            grpBasicDetails = new GroupBox();
            grpBasicDetails.Text = "Basic Details";
            grpBasicDetails.Location = new Point(30, 180);
            grpBasicDetails.Size = new Size(430, 240);
            grpBasicDetails.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.Controls.Add(grpBasicDetails);

            lblFirstName = new Label();
            lblFirstName.Text = "First Name:";
            lblFirstName.AutoSize = true;
            lblFirstName.Location = new Point(20, 35);
            grpBasicDetails.Controls.Add(lblFirstName);

            txtFirstName = new TextBox();
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(250, 25);
            txtFirstName.Location = new Point(130, 32);
            grpBasicDetails.Controls.Add(txtFirstName);

            lblLastName = new Label();
            lblLastName.Text = "Last Name:";
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(20, 75);
            grpBasicDetails.Controls.Add(lblLastName);

            txtLastName = new TextBox();
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(250, 25);
            txtLastName.Location = new Point(130, 72);
            grpBasicDetails.Controls.Add(txtLastName);

            lblDateOfBirth = new Label();
            lblDateOfBirth.Text = "Date of Birth:";
            lblDateOfBirth.AutoSize = true;
            lblDateOfBirth.Location = new Point(20, 115);
            grpBasicDetails.Controls.Add(lblDateOfBirth);

            dtpDateOfBirth = new DateTimePicker();
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(250, 25);
            dtpDateOfBirth.Location = new Point(130, 112);
            dtpDateOfBirth.Format = DateTimePickerFormat.Short;
            grpBasicDetails.Controls.Add(dtpDateOfBirth);

            lblGender = new Label();
            lblGender.Text = "Gender:";
            lblGender.AutoSize = true;
            lblGender.Location = new Point(20, 155);
            grpBasicDetails.Controls.Add(lblGender);

            rdoMale = new RadioButton();
            rdoMale.Name = "rdoMale";
            rdoMale.Text = "Male";
            rdoMale.AutoSize = true;
            rdoMale.Location = new Point(130, 153);
            grpBasicDetails.Controls.Add(rdoMale);

            rdoFemale = new RadioButton();
            rdoFemale.Name = "rdoFemale";
            rdoFemale.Text = "Female";
            rdoFemale.AutoSize = true;
            rdoFemale.Location = new Point(220, 153);
            grpBasicDetails.Controls.Add(rdoFemale);

            lblAddress = new Label();
            lblAddress.Text = "Address:";
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(20, 195);
            grpBasicDetails.Controls.Add(lblAddress);

            txtAddress = new TextBox();
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(250, 25);
            txtAddress.Location = new Point(130, 192);
            grpBasicDetails.Controls.Add(txtAddress);

            // Contact Details group
            grpContactDetails = new GroupBox();
            grpContactDetails.Text = "Contact Details";
            grpContactDetails.Location = new Point(470, 180);
            grpContactDetails.Size = new Size(430, 240);
            grpContactDetails.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.Controls.Add(grpContactDetails);

            lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(20, 35);
            grpContactDetails.Controls.Add(lblEmail);

            txtEmail = new TextBox();
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(250, 25);
            txtEmail.Location = new Point(130, 32);
            grpContactDetails.Controls.Add(txtEmail);

            lblMobilePhone = new Label();
            lblMobilePhone.Text = "Mobile Phone:";
            lblMobilePhone.AutoSize = true;
            lblMobilePhone.Location = new Point(20, 75);
            grpContactDetails.Controls.Add(lblMobilePhone);

            txtMobilePhone = new TextBox();
            txtMobilePhone.Name = "txtMobilePhone";
            txtMobilePhone.Size = new Size(250, 25);
            txtMobilePhone.Location = new Point(130, 72);
            grpContactDetails.Controls.Add(txtMobilePhone);

            lblHomePhone = new Label();
            lblHomePhone.Text = "Home Phone:";
            lblHomePhone.AutoSize = true;
            lblHomePhone.Location = new Point(20, 115);
            grpContactDetails.Controls.Add(lblHomePhone);

            txtHomePhone = new TextBox();
            txtHomePhone.Name = "txtHomePhone";
            txtHomePhone.Size = new Size(250, 25);
            txtHomePhone.Location = new Point(130, 112);
            grpContactDetails.Controls.Add(txtHomePhone);

            // Parent Details group
            grpParentDetails = new GroupBox();
            grpParentDetails.Text = "Parent Details";
            grpParentDetails.Location = new Point(30, 430);
            grpParentDetails.Size = new Size(870, 120);
            grpParentDetails.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.Controls.Add(grpParentDetails);

            lblParentName = new Label();
            lblParentName.Text = "Parent Name:";
            lblParentName.AutoSize = true;
            lblParentName.Location = new Point(20, 35);
            grpParentDetails.Controls.Add(lblParentName);

            txtParentName = new TextBox();
            txtParentName.Name = "txtParentName";
            txtParentName.Size = new Size(220, 25);
            txtParentName.Location = new Point(130, 32);
            grpParentDetails.Controls.Add(txtParentName);

            lblNIC = new Label();
            lblNIC.Text = "NIC:";
            lblNIC.AutoSize = true;
            lblNIC.Location = new Point(400, 35);
            grpParentDetails.Controls.Add(lblNIC);

            txtNIC = new TextBox();
            txtNIC.Name = "txtNIC";
            txtNIC.Size = new Size(180, 25);
            txtNIC.Location = new Point(480, 32);
            grpParentDetails.Controls.Add(txtNIC);

            lblContactNo = new Label();
            lblContactNo.Text = "Contact No:";
            lblContactNo.AutoSize = true;
            lblContactNo.Location = new Point(20, 75);
            grpParentDetails.Controls.Add(lblContactNo);

            txtContactNo = new TextBox();
            txtContactNo.Name = "txtContactNo";
            txtContactNo.Size = new Size(220, 25);
            txtContactNo.Location = new Point(130, 72);
            grpParentDetails.Controls.Add(txtContactNo);

            // Action buttons
            btnRegister = new Button();
            btnRegister.Name = "btnRegister";
            btnRegister.Text = "Register";
            btnRegister.Size = new Size(90, 35);
            btnRegister.Location = new Point(470, 75);
            btnRegister.Click += new EventHandler(this.btnRegister_Click);
            grpParentDetails.Controls.Add(btnRegister);

            btnUpdate = new Button();
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Text = "Update";
            btnUpdate.Size = new Size(90, 35);
            btnUpdate.Location = new Point(570, 75);
            btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
            grpParentDetails.Controls.Add(btnUpdate);

            btnDelete = new Button();
            btnDelete.Name = "btnDelete";
            btnDelete.Text = "Delete";
            btnDelete.Size = new Size(90, 35);
            btnDelete.Location = new Point(670, 75);
            btnDelete.Click += new EventHandler(this.btnDelete_Click);
            grpParentDetails.Controls.Add(btnDelete);

            btnClear = new Button();
            btnClear.Name = "btnClear";
            btnClear.Text = "Clear";
            btnClear.Size = new Size(90, 35);
            btnClear.Location = new Point(770, 75);
            btnClear.Click += new EventHandler(this.btnClear_Click);
            grpParentDetails.Controls.Add(btnClear);

            // Link labels
            lnkLogout = new LinkLabel();
            lnkLogout.Name = "lnkLogout";
            lnkLogout.Text = "Logout";
            lnkLogout.AutoSize = true;
            lnkLogout.Location = new Point(800, 560);
            lnkLogout.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkLogout_LinkClicked);
            this.Controls.Add(lnkLogout);

            lnkExit = new LinkLabel();
            lnkExit.Name = "lnkExit";
            lnkExit.Text = "Exit";
            lnkExit.AutoSize = true;
            lnkExit.Location = new Point(880, 560);
            lnkExit.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkExit_LinkClicked);
            this.Controls.Add(lnkExit);

            this.Load += new EventHandler(this.frmRegistration_Load);
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            // Populate registration numbers from the database when the form loads.
            LoadRegistrationNumbers();
            ClearAllFields();
            txtFirstName.Focus();
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
                                txtNIC.Text = reader["NIC"].ToString();
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
                                    (regNo, firstName, lastName, dateOfBirth, gender, address, email, mobilePhone, homePhone, parentName, NIC, contactNo)
                                    VALUES 
                                    (@regNo, @firstName, @lastName, @dateOfBirth, @gender, @address, @email, @mobilePhone, @homePhone, @parentName, @NIC, @contactNo)";

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
                                    NIC = @NIC,
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
            command.Parameters.AddWithValue("@regNo", cmbRegNo.Text.Trim());
            command.Parameters.AddWithValue("@firstName", txtFirstName.Text.Trim());
            command.Parameters.AddWithValue("@lastName", txtLastName.Text.Trim());
            command.Parameters.AddWithValue("@dateOfBirth", dtpDateOfBirth.Value);
            command.Parameters.AddWithValue("@gender", rdoMale.Checked ? "Male" : (rdoFemale.Checked ? "Female" : string.Empty));
            command.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
            command.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            command.Parameters.AddWithValue("@mobilePhone", txtMobilePhone.Text.Trim());
            command.Parameters.AddWithValue("@homePhone", txtHomePhone.Text.Trim());
            command.Parameters.AddWithValue("@parentName", txtParentName.Text.Trim());
            command.Parameters.AddWithValue("@NIC", txtNIC.Text.Trim());
            command.Parameters.AddWithValue("@contactNo", txtContactNo.Text.Trim());
        }

        /// <summary>
        /// Validates that all required fields are filled.
        /// </summary>
        private bool ValidateFields()
        {
            if (string.IsNullOrEmpty(cmbRegNo.Text.Trim()) ||
                !int.TryParse(cmbRegNo.Text.Trim(), out _) ||
                string.IsNullOrEmpty(txtFirstName.Text.Trim()) ||
                string.IsNullOrEmpty(txtLastName.Text.Trim()) ||
                string.IsNullOrEmpty(txtAddress.Text.Trim()) ||
                string.IsNullOrEmpty(txtEmail.Text.Trim()) ||
                string.IsNullOrEmpty(txtMobilePhone.Text.Trim()) ||
                string.IsNullOrEmpty(txtParentName.Text.Trim()) ||
                string.IsNullOrEmpty(txtNIC.Text.Trim()) ||
                string.IsNullOrEmpty(txtContactNo.Text.Trim()) ||
                (!rdoMale.Checked && !rdoFemale.Checked))
            {
                MessageBox.Show("Please fill in all required fields correctly. Reg No must be a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Clears all input fields and resets the ComboBox selection.
        /// </summary>
        private void ClearAllFields()
        {
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

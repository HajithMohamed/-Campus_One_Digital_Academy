using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class frmStudentRecords : Form
    {
        private readonly string connectionString;
        private readonly TextBox txtSearch = new TextBox();
        private readonly DataGridView dgvStudents = new DataGridView();
        private readonly Label lblTotal = new Label();
        private readonly Label lblMale = new Label();
        private readonly Label lblFemale = new Label();
        public int? SelectedRegNo { get; private set; }

        public frmStudentRecords(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Student Records - Campus One Digital Academy";
            ClientSize = new Size(980, 590);
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(850, 520);
            BackColor = Color.FromArgb(250, 247, 248);
            Font = new Font("Segoe UI", 9F);

            string iconPath = Path.Combine(AppContext.BaseDirectory, "Assets", "CampusOneLogo.ico");
            if (File.Exists(iconPath)) Icon = new Icon(iconPath);

            var title = new Label
            {
                Text = "Registered Students",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(24, 18)
            };

            var searchLabel = new Label { Text = "Search by Reg No, name or NIC", AutoSize = true, Location = new Point(26, 79) };
            txtSearch.Name = "txtSearch";
            txtSearch.Location = new Point(250, 74);
            txtSearch.Size = new Size(300, 27);
            txtSearch.PlaceholderText = "Start typing to filter records";
            txtSearch.TextChanged += (s, e) => LoadStudents(txtSearch.Text.Trim());

            var btnRefresh = CreateButton("Refresh", new Point(565, 72), (s, e) => { txtSearch.Clear(); LoadStudents(); });
            var btnExport = CreateButton("Export CSV", new Point(665, 72), btnExport_Click);
            var btnEdit = CreateButton("Edit Selected", new Point(780, 72), btnEdit_Click);
            btnEdit.Size = new Size(110, 30);

            lblTotal.Location = new Point(27, 117);
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotal.Text = "Total Students: 0";
            lblMale.Location = new Point(165, 117);
            lblMale.AutoSize = true;
            lblMale.Text = "Male: 0";
            lblFemale.Location = new Point(275, 117);
            lblFemale.AutoSize = true;
            lblFemale.Text = "Female: 0";

            dgvStudents.Name = "dgvStudents";
            dgvStudents.Location = new Point(25, 145);
            dgvStudents.Size = new Size(930, 390);
            dgvStudents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvStudents.ReadOnly = true;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.MultiSelect = false;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvStudents.BackgroundColor = Color.White;
            dgvStudents.BorderStyle = BorderStyle.Fixed3D;
            dgvStudents.CellDoubleClick += (s, e) => SelectCurrentRecord();

            var btnClose = CreateButton("Close", new Point(855, 548), (s, e) => Close());
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            Controls.AddRange(new Control[] { title, searchLabel, txtSearch, btnRefresh, btnExport, btnEdit,
                lblTotal, lblMale, lblFemale, dgvStudents, btnClose });
            Load += (s, e) => LoadStudents();
        }

        private static Button CreateButton(string text, Point location, EventHandler click)
        {
            var button = new Button { Text = text, Location = location, Size = new Size(95, 30) };
            button.Click += click;
            return button;
        }

        private void LoadStudents(string search = "")
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                const string sql = @"SELECT regNo AS [Reg No], firstName AS [First Name], lastName AS [Last Name],
 dateOfBirth AS [Date of Birth], gender AS Gender, address AS Address, email AS Email,
 mobilePhone AS [Mobile Phone], homePhone AS [Home Phone], parentName AS [Parent Name], nic AS NIC,
 contactNo AS [Contact No]
FROM Registration
WHERE @search = '' OR CONVERT(VARCHAR(20), regNo) LIKE @pattern
 OR firstName LIKE @pattern OR lastName LIKE @pattern OR nic LIKE @pattern
ORDER BY regNo";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@search", search);
                command.Parameters.AddWithValue("@pattern", "%" + search + "%");
                using var adapter = new SqlDataAdapter(command);
                var table = new DataTable();
                adapter.Fill(table);
                dgvStudents.DataSource = table;
                UpdateDashboard(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load student records.\n\n" + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDashboard(DataTable table)
        {
            int male = 0;
            int female = 0;
            foreach (DataRow row in table.Rows)
            {
                string gender = Convert.ToString(row["Gender"]);
                if (gender == "Male") male++;
                if (gender == "Female") female++;
            }
            lblTotal.Text = $"Total Students: {table.Rows.Count}";
            lblMale.Text = $"Male: {male}";
            lblFemale.Text = $"Female: {female}";
        }

        private void btnEdit_Click(object sender, EventArgs e) => SelectCurrentRecord();

        private void SelectCurrentRecord()
        {
            if (dgvStudents.CurrentRow?.Cells["Reg No"].Value == null)
            {
                MessageBox.Show("Please select a student record first.", "Select Student",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SelectedRegNo = Convert.ToInt32(dgvStudents.CurrentRow.Cells["Reg No"].Value);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvStudents.Rows.Count == 0)
            {
                MessageBox.Show("There are no student records to export.", "Export CSV",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = $"CampusOneStudents_{DateTime.Now:yyyyMMdd}.csv"
            };
            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                using var writer = new StreamWriter(dialog.FileName, false, new UTF8Encoding(true));
                for (int column = 0; column < dgvStudents.Columns.Count; column++)
                {
                    if (column > 0) writer.Write(',');
                    writer.Write(EscapeCsv(dgvStudents.Columns[column].HeaderText));
                }
                writer.WriteLine();

                foreach (DataGridViewRow row in dgvStudents.Rows)
                {
                    for (int column = 0; column < dgvStudents.Columns.Count; column++)
                    {
                        if (column > 0) writer.Write(',');
                        writer.Write(EscapeCsv(Convert.ToString(row.Cells[column].Value) ?? ""));
                    }
                    writer.WriteLine();
                }
                MessageBox.Show("Student records exported successfully.", "Export CSV",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not export the records.\n\n" + ex.Message, "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string EscapeCsv(string value) => "\"" + value.Replace("\"", "\"\"") + "\"";
    }
}

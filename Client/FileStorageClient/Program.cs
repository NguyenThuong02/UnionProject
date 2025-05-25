using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouthUnionManagement
{
    // Models
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Admin, Manager, Member
        public string FullName { get; set; }
    }

    public class Member
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Class { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public int TrainingPoints { get; set; }
    }

    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public int MaxParticipants { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }
    }

    public class Participation
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int ActivityId { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsAttended { get; set; }
        public int PointsEarned { get; set; }
    }

    public class Award
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime AwardDate { get; set; }
        public int Points { get; set; }
    }

    // Data Manager
    public static class DataManager
    {
        public static List<User> Users = new List<User>();
        public static List<Member> Members = new List<Member>();
        public static List<Activity> Activities = new List<Activity>();
        public static List<Participation> Participations = new List<Participation>();
        public static List<Award> Awards = new List<Award>();
        public static User CurrentUser = null;

        static DataManager()
        {
            InitializeData();
        }

        private static void InitializeData()
        {
            // Initialize sample users
            Users.Add(new User { Id = 1, Username = "admin", Password = "admin", Role = "Admin", FullName = "Quản trị viên" });
            Users.Add(new User { Id = 2, Username = "manager", Password = "manager", Role = "Manager", FullName = "Bí thư Đoàn" });
            Users.Add(new User { Id = 3, Username = "member", Password = "member", Role = "Member", FullName = "Đoàn viên" });

            // Initialize sample members
            Members.Add(new Member 
            { 
                Id = 1, Code = "DV001", FullName = "Nguyễn Văn An", 
                DateOfBirth = new DateTime(2000, 1, 15), Class = "CNTT01", 
                Department = "Công nghệ thông tin", Phone = "0123456789", 
                Email = "an@email.com", JoinDate = DateTime.Now.AddYears(-2), 
                IsActive = true, TrainingPoints = 85 
            });
            Members.Add(new Member 
            { 
                Id = 2, Code = "DV002", FullName = "Trần Thị Bình", 
                DateOfBirth = new DateTime(2001, 3, 20), Class = "KT02", 
                Department = "Kinh tế", Phone = "0987654321", 
                Email = "binh@email.com", JoinDate = DateTime.Now.AddYears(-1), 
                IsActive = true, TrainingPoints = 92 
            });

            // Initialize sample activities
            Activities.Add(new Activity 
            { 
                Id = 1, Name = "Tình nguyện mùa hè", 
                Description = "Hoạt động tình nguyện hè 2024", 
                StartDate = DateTime.Now.AddDays(10), 
                EndDate = DateTime.Now.AddDays(15), 
                Location = "Vùng cao", MaxParticipants = 50, 
                Points = 10, IsActive = true 
            });
            Activities.Add(new Activity 
            { 
                Id = 2, Name = "Học tập chính trị", 
                Description = "Sinh hoạt học tập chính trị tháng 5", 
                StartDate = DateTime.Now.AddDays(-5), 
                EndDate = DateTime.Now.AddDays(-3), 
                Location = "Hội trường A", MaxParticipants = 100, 
                Points = 5, IsActive = true 
            });
        }
    }

    // Login Form
    public partial class LoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblTitle;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Đăng nhập - Quản lý Đoàn Sở";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            lblTitle = new Label();
            lblTitle.Text = "ĐĂNG NHẬP HỆ THỐNG";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkBlue;
            lblTitle.Location = new Point(80, 30);
            lblTitle.Size = new Size(250, 30);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            Label lblUsername = new Label();
            lblUsername.Text = "Tên đăng nhập:";
            lblUsername.Location = new Point(50, 80);
            lblUsername.Size = new Size(100, 20);

            txtUsername = new TextBox();
            txtUsername.Location = new Point(160, 78);
            txtUsername.Size = new Size(180, 20);

            Label lblPassword = new Label();
            lblPassword.Text = "Mật khẩu:";
            lblPassword.Location = new Point(50, 120);
            lblPassword.Size = new Size(100, 20);

            txtPassword = new TextBox();
            txtPassword.Location = new Point(160, 118);
            txtPassword.Size = new Size(180, 20);
            txtPassword.UseSystemPasswordChar = true;

            btnLogin = new Button();
            btnLogin.Text = "Đăng nhập";
            btnLogin.Location = new Point(160, 160);
            btnLogin.Size = new Size(80, 30);
            btnLogin.BackColor = Color.LightBlue;
            btnLogin.Click += BtnLogin_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var user = DataManager.Users.FirstOrDefault(u => 
                u.Username == txtUsername.Text && u.Password == txtPassword.Text);

            if (user != null)
            {
                DataManager.CurrentUser = user;
                MessageBox.Show($"Đăng nhập thành công! Chào mừng {user.FullName}", "Thông báo");
                
                MainForm mainForm = new MainForm();
                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Main Form
    public partial class MainForm : Form
    {
        private MenuStrip menuStrip;
        private StatusStrip statusStrip;
        private Panel mainPanel;

        public MainForm()
        {
            InitializeComponent();
            LoadUserInterface();
        }

        private void InitializeComponent()
        {
            this.Text = "Hệ thống Quản lý Hoạt động Đoàn Sở";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            // Menu Strip
            menuStrip = new MenuStrip();
            
            // Status Strip
            statusStrip = new StatusStrip();
            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel();
            statusLabel.Text = $"Xin chào: {DataManager.CurrentUser.FullName} ({DataManager.CurrentUser.Role})";
            statusStrip.Items.Add(statusLabel);

            // Main Panel
            mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.LightGray;

            this.Controls.Add(mainPanel);
            this.Controls.Add(statusStrip);
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;
        }

        private void LoadUserInterface()
        {
            // System Menu
            ToolStripMenuItem systemMenu = new ToolStripMenuItem("Hệ thống");
            systemMenu.DropDownItems.Add("Đăng xuất", null, Logout_Click);
            systemMenu.DropDownItems.Add("Thoát", null, Exit_Click);

            // Member Management Menu
            ToolStripMenuItem memberMenu = new ToolStripMenuItem("Quản lý Đoàn viên");
            memberMenu.DropDownItems.Add("Danh sách Đoàn viên", null, MemberList_Click);
            if (DataManager.CurrentUser.Role == "Admin" || DataManager.CurrentUser.Role == "Manager")
            {
                memberMenu.DropDownItems.Add("Thêm Đoàn viên", null, AddMember_Click);
            }

            // Activity Management Menu
            ToolStripMenuItem activityMenu = new ToolStripMenuItem("Quản lý Hoạt động");
            activityMenu.DropDownItems.Add("Danh sách Hoạt động", null, ActivityList_Click);
            if (DataManager.CurrentUser.Role == "Admin" || DataManager.CurrentUser.Role == "Manager")
            {
                activityMenu.DropDownItems.Add("Thêm Hoạt động", null, AddActivity_Click);
                activityMenu.DropDownItems.Add("Quản lý Tham gia", null, ManageParticipation_Click);
            }

            // Training Points Menu
            ToolStripMenuItem pointsMenu = new ToolStripMenuItem("Điểm rèn luyện");
            pointsMenu.DropDownItems.Add("Xem điểm rèn luyện", null, ViewPoints_Click);
            if (DataManager.CurrentUser.Role == "Admin" || DataManager.CurrentUser.Role == "Manager")
            {
                pointsMenu.DropDownItems.Add("Cập nhật điểm", null, UpdatePoints_Click);
                pointsMenu.DropDownItems.Add("Quản lý Khen thưởng", null, ManageAward_Click);
            }

            // Report Menu
            if (DataManager.CurrentUser.Role == "Admin" || DataManager.CurrentUser.Role == "Manager")
            {
                ToolStripMenuItem reportMenu = new ToolStripMenuItem("Báo cáo");
                reportMenu.DropDownItems.Add("Thống kê Đoàn viên", null, MemberReport_Click);
                reportMenu.DropDownItems.Add("Thống kê Hoạt động", null, ActivityReport_Click);
                reportMenu.DropDownItems.Add("Báo cáo Điểm rèn luyện", null, PointsReport_Click);
                menuStrip.Items.Add(reportMenu);
            }

            menuStrip.Items.Add(systemMenu);
            menuStrip.Items.Add(memberMenu);
            menuStrip.Items.Add(activityMenu);
            menuStrip.Items.Add(pointsMenu);

            ShowWelcomeScreen();
        }

        private void ShowWelcomeScreen()
        {
            mainPanel.Controls.Clear();
            
            Label welcomeLabel = new Label();
            welcomeLabel.Text = $"Chào mừng đến với Hệ thống Quản lý Hoạt động Đoàn Sở\n\n" +
                               $"Người dùng: {DataManager.CurrentUser.FullName}\n" +
                               $"Vai trò: {DataManager.CurrentUser.Role}\n\n" +
                               "Vui lòng chọn chức năng từ menu phía trên.";
            welcomeLabel.Font = new Font("Arial", 14);
            welcomeLabel.ForeColor = Color.DarkBlue;
            welcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            welcomeLabel.Dock = DockStyle.Fill;
            
            mainPanel.Controls.Add(welcomeLabel);
        }

        // Event Handlers
        private void Logout_Click(object sender, EventArgs e)
        {
            DataManager.CurrentUser = null;
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Close();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MemberList_Click(object sender, EventArgs e)
        {
            ShowMemberList();
        }

        private void AddMember_Click(object sender, EventArgs e)
        {
            MemberForm memberForm = new MemberForm();
            if (memberForm.ShowDialog() == DialogResult.OK)
            {
                ShowMemberList();
            }
        }

        private void ActivityList_Click(object sender, EventArgs e)
        {
            ShowActivityList();
        }

        private void AddActivity_Click(object sender, EventArgs e)
        {
            ActivityForm activityForm = new ActivityForm();
            if (activityForm.ShowDialog() == DialogResult.OK)
            {
                ShowActivityList();
            }
        }

        private void ManageParticipation_Click(object sender, EventArgs e)
        {
            ShowParticipationManagement();
        }

        private void ViewPoints_Click(object sender, EventArgs e)
        {
            ShowPointsView();
        }

        private void UpdatePoints_Click(object sender, EventArgs e)
        {
            ShowPointsUpdate();
        }

        private void ManageAward_Click(object sender, EventArgs e)
        {
            ShowAwardManagement();
        }

        private void MemberReport_Click(object sender, EventArgs e)
        {
            ShowMemberReport();
        }

        private void ActivityReport_Click(object sender, EventArgs e)
        {
            ShowActivityReport();
        }

        private void PointsReport_Click(object sender, EventArgs e)
        {
            ShowPointsReport();
        }

        // UI Methods
        private void ShowMemberList()
        {
            mainPanel.Controls.Clear();
            
            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Code", HeaderText = "Mã ĐV", Width = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FullName", HeaderText = "Họ tên", Width = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Class", HeaderText = "Lớp", Width = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Department", HeaderText = "Khoa", Width = 120 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Phone", HeaderText = "SĐT", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrainingPoints", HeaderText = "Điểm RL", Width = 80 });

            dgv.DataSource = DataManager.Members.Where(m => m.IsActive).ToList();
            
            mainPanel.Controls.Add(dgv);
        }

        private void ShowActivityList()
        {
            mainPanel.Controls.Clear();
            
            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Tên hoạt động", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StartDate", HeaderText = "Ngày bắt đầu", Width = 120 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EndDate", HeaderText = "Ngày kết thúc", Width = 120 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Location", HeaderText = "Địa điểm", Width = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaxParticipants", HeaderText = "Số lượng", Width = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Points", HeaderText = "Điểm", Width = 60 });

            dgv.DataSource = DataManager.Activities.Where(a => a.IsActive).ToList();
            
            mainPanel.Controls.Add(dgv);
        }

        private void ShowParticipationManagement()
        {
            mainPanel.Controls.Clear();
            
            Label title = new Label();
            title.Text = "QUẢN LÝ THAM GIA HOẠT ĐỘNG";
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.Location = new Point(20, 20);
            title.Size = new Size(300, 30);
            
            mainPanel.Controls.Add(title);
            
            // This would contain participation management interface
            Label info = new Label();
            info.Text = "Chức năng quản lý tham gia hoạt động đang được phát triển...";
            info.Location = new Point(20, 60);
            info.Size = new Size(400, 20);
            
            mainPanel.Controls.Add(info);
        }

        private void ShowPointsView()
        {
            mainPanel.Controls.Clear();
            
            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Code", HeaderText = "Mã ĐV", Width = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FullName", HeaderText = "Họ tên", Width = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Class", HeaderText = "Lớp", Width = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrainingPoints", HeaderText = "Điểm rèn luyện", Width = 100 });

            var memberPoints = DataManager.Members.Where(m => m.IsActive).Select(m => new
            {
                Code = m.Code,
                FullName = m.FullName,
                Class = m.Class,
                TrainingPoints = m.TrainingPoints
            }).ToList();

            dgv.DataSource = memberPoints;
            mainPanel.Controls.Add(dgv);
        }

        private void ShowPointsUpdate()
        {
            mainPanel.Controls.Clear();
            
            Label title = new Label();
            title.Text = "CẬP NHẬT ĐIỂM RÈN LUYỆN";
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.Location = new Point(20, 20);
            title.Size = new Size(300, 30);
            
            mainPanel.Controls.Add(title);
            
            Label info = new Label();
            info.Text = "Chức năng cập nhật điểm rèn luyện đang được phát triển...";
            info.Location = new Point(20, 60);
            info.Size = new Size(400, 20);
            
            mainPanel.Controls.Add(info);
        }

        private void ShowAwardManagement()
        {
            mainPanel.Controls.Clear();
            
            Label title = new Label();
            title.Text = "QUẢN LÝ KHEN THƯỞNG";
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.Location = new Point(20, 20);
            title.Size = new Size(300, 30);
            
            mainPanel.Controls.Add(title);
            
            Label info = new Label();
            info.Text = "Chức năng quản lý khen thưởng đang được phát triển...";
            info.Location = new Point(20, 60);
            info.Size = new Size(400, 20);
            
            mainPanel.Controls.Add(info);
        }

        private void ShowMemberReport()
        {
            mainPanel.Controls.Clear();
            
            Label title = new Label();
            title.Text = "BÁO CÁO THỐNG KÊ ĐOÀN VIÊN";
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.Location = new Point(20, 20);
            title.Size = new Size(350, 30);
            
            Label stats = new Label();
            stats.Text = $"Tổng số đoàn viên: {DataManager.Members.Count(m => m.IsActive)}\n" +
                        $"Đoàn viên mới trong năm: {DataManager.Members.Count(m => m.JoinDate.Year == DateTime.Now.Year)}\n" +
                        $"Điểm rèn luyện trung bình: {(DataManager.Members.Any() ? DataManager.Members.Average(m => m.TrainingPoints):0):F1}";
            stats.Location = new Point(20, 60);
            stats.Size = new Size(400, 100);
            
            mainPanel.Controls.Add(title);
            mainPanel.Controls.Add(stats);
        }

        private void ShowActivityReport()
        {
            mainPanel.Controls.Clear();
            
            Label title = new Label();
            title.Text = "BÁO CÁO THỐNG KÊ HOẠT ĐỘNG";
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.Location = new Point(20, 20);
            title.Size = new Size(350, 30);
            
            Label stats = new Label();
            stats.Text = $"Tổng số hoạt động: {DataManager.Activities.Count(a => a.IsActive)}\n" +
                        $"Hoạt động trong tháng: {DataManager.Activities.Count(a => a.StartDate.Month == DateTime.Now.Month)}\n" +
                        $"Tổng số tham gia: {DataManager.Participations.Count}";
            stats.Location = new Point(20, 60);
            stats.Size = new Size(400, 100);
            
            mainPanel.Controls.Add(title);
            mainPanel.Controls.Add(stats);
        }

        private void ShowPointsReport()
        {
            mainPanel.Controls.Clear();
            
            Label title = new Label();
            title.Text = "BÁO CÁO ĐIỂM RÈN LUYỆN";
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.Location = new Point(20, 20);
            title.Size = new Size(350, 30);
            
            var excellent = DataManager.Members.Count(m => m.TrainingPoints >= 90);
            var good = DataManager.Members.Count(m => m.TrainingPoints >= 80 && m.TrainingPoints < 90);
            var average = DataManager.Members.Count(m => m.TrainingPoints >= 65 && m.TrainingPoints < 80);
            var weak = DataManager.Members.Count(m => m.TrainingPoints < 65);
            
            Label stats = new Label();
            stats.Text = $"Xuất sắc (90-100): {excellent} đoàn viên\n" +
                        $"Tốt (80-89): {good} đoàn viên\n" +
                        $"Khá (65-79): {average} đoàn viên\n" +
                        $"Yếu (<65): {weak} đoàn viên";
            stats.Location = new Point(20, 60);
            stats.Size = new Size(400, 120);
            
            mainPanel.Controls.Add(title);
            mainPanel.Controls.Add(stats);
        }
    }

    // Member Form
    public partial class MemberForm : Form
    {
        private TextBox txtCode, txtFullName, txtClass, txtDepartment, txtPhone, txtEmail;
        private DateTimePicker dtpDateOfBirth, dtpJoinDate;
        private NumericUpDown numTrainingPoints;
        private CheckBox chkIsActive;
        private Button btnSave, btnCancel;

        public MemberForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Thêm/Sửa Đoàn viên";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            int y = 20;
            int labelWidth = 120;
            int controlWidth = 200;
            int spacing = 35;

            // Code
            Label lblCode = new Label { Text = "Mã đoàn viên:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            txtCode = new TextBox { Location = new Point(150, y), Size = new Size(controlWidth, 20) };
            this.Controls.Add(lblCode);
            this.Controls.Add(txtCode);
            y += spacing;

            // Full Name
            Label lblFullName = new Label { Text = "Họ và tên:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            txtFullName = new TextBox { Location = new Point(150, y), Size = new Size(controlWidth, 20) };
            this.Controls.Add(lblFullName);
            this.Controls.Add(txtFullName);
            y += spacing;

            // Date of Birth
            Label lblDOB = new Label { Text = "Ngày sinh:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            dtpDateOfBirth = new DateTimePicker { Location = new Point(150, y), Size = new Size(controlWidth, 20) };
            this.Controls.Add(lblDOB);
            this.Controls.Add(dtpDateOfBirth);
            y += spacing;

            // Class
            Label lblClass = new Label { Text = "Lớp:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            txtClass = new TextBox { Location = new Point(150, y), Size = new Size(controlWidth, 20) };
            this.Controls.Add(lblClass);
            this.Controls.Add(txtClass);
            y += spacing;

            // Department
            Label lblDepartment = new Label { Text = "Khoa:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            txtDepartment = new TextBox { Location = new Point(150, y), Size = new Size(controlWidth, 20) };
            this.Controls.Add(lblDepartment);
            this.Controls.Add(txtDepartment);
            y += spacing;

            // Phone
            Label lblPhone = new Label { Text = "Số điện thoại:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            txtPhone = new TextBox { Location = new Point(150, y), Size = new Size(controlWidth, 20) };
            this.Controls.Add(lblPhone);
            this.Controls.Add(txtPhone);
            y += spacing;

            // Email
            Label lblEmail = new Label { Text = "Email:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            txtEmail = new TextBox { Location = new Point(150, y), Size = new Size(controlWidth, 20) };
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            y += spacing;

            // Join Date
            Label lblJoinDate = new Label { Text = "Ngày vào Đoàn:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            dtpJoinDate = new DateTimePicker { Location = new Point(150, y), Size = new Size(controlWidth, 20) };
            this.Controls.Add(lblJoinDate);
            this.Controls.Add(dtpJoinDate);
            y += spacing;

            // Training Points
            Label lblPoints = new Label { Text = "Điểm rèn luyện:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            numTrainingPoints = new NumericUpDown { Location = new Point(150, y), Size = new Size(controlWidth, 20), Maximum = 100, Minimum = 0, Value = 0 };
            this.Controls.Add(lblPoints);
            this.Controls.Add(numTrainingPoints);
            y += spacing;

            // Is Active
            chkIsActive = new CheckBox { Text = "Đang hoạt động", Location = new Point(150, y), Size = new Size(controlWidth, 20), Checked = true };
            this.Controls.Add(chkIsActive);
            y += spacing + 10;

            // Buttons
            btnSave = new Button { Text = "Lưu", Location = new Point(200, y), Size = new Size(70, 30), BackColor = Color.LightGreen };
            btnCancel = new Button { Text = "Hủy", Location = new Point(280, y), Size = new Size(70, 30), BackColor = Color.LightCoral };
            
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if code already exists
            if (DataManager.Members.Any(m => m.Code == txtCode.Text))
            {
                MessageBox.Show("Mã đoàn viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var member = new Member
            {
                Id = DataManager.Members.Count > 0 ? DataManager.Members.Max(m => m.Id) + 1 : 1,
                Code = txtCode.Text,
                FullName = txtFullName.Text,
                DateOfBirth = dtpDateOfBirth.Value,
                Class = txtClass.Text,
                Department = txtDepartment.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text,
                JoinDate = dtpJoinDate.Value,
                TrainingPoints = (int)numTrainingPoints.Value,
                IsActive = chkIsActive.Checked
            };

            DataManager.Members.Add(member);
            MessageBox.Show("Thêm đoàn viên thành công!", "Thông báo");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    // Activity Form
    public partial class ActivityForm : Form
    {
        private TextBox txtName, txtDescription, txtLocation;
        private DateTimePicker dtpStartDate, dtpEndDate;
        private NumericUpDown numMaxParticipants, numPoints;
        private CheckBox chkIsActive;
        private Button btnSave, btnCancel;

        public ActivityForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Thêm/Sửa Hoạt động";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            int y = 20;
            int labelWidth = 120;
            int controlWidth = 300;
            int spacing = 35;

            // Name
            Label lblName = new Label { Text = "Tên hoạt động:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            txtName = new TextBox { Location = new Point(150, y), Size = new Size(controlWidth, 20) };
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            y += spacing;

            // Description
            Label lblDescription = new Label { Text = "Mô tả:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            txtDescription = new TextBox { Location = new Point(150, y), Size = new Size(controlWidth, 60), Multiline = true };
            this.Controls.Add(lblDescription);
            this.Controls.Add(txtDescription);
            y += 70;

            // Start Date
            Label lblStartDate = new Label { Text = "Ngày bắt đầu:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            dtpStartDate = new DateTimePicker { Location = new Point(150, y), Size = new Size(200, 20) };
            this.Controls.Add(lblStartDate);
            this.Controls.Add(dtpStartDate);
            y += spacing;

            // End Date
            Label lblEndDate = new Label { Text = "Ngày kết thúc:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            dtpEndDate = new DateTimePicker { Location = new Point(150, y), Size = new Size(200, 20) };
            this.Controls.Add(lblEndDate);
            this.Controls.Add(dtpEndDate);
            y += spacing;

            // Location
            Label lblLocation = new Label { Text = "Địa điểm:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            txtLocation = new TextBox { Location = new Point(150, y), Size = new Size(controlWidth, 20) };
            this.Controls.Add(lblLocation);
            this.Controls.Add(txtLocation);
            y += spacing;

            // Max Participants
            Label lblMaxParticipants = new Label { Text = "Số người tối đa:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            numMaxParticipants = new NumericUpDown { Location = new Point(150, y), Size = new Size(100, 20), Maximum = 1000, Minimum = 1, Value = 50 };
            this.Controls.Add(lblMaxParticipants);
            this.Controls.Add(numMaxParticipants);
            y += spacing;

            // Points
            Label lblPoints = new Label { Text = "Điểm thưởng:", Location = new Point(20, y), Size = new Size(labelWidth, 20) };
            numPoints = new NumericUpDown { Location = new Point(150, y), Size = new Size(100, 20), Maximum = 50, Minimum = 0, Value = 5 };
            this.Controls.Add(lblPoints);
            this.Controls.Add(numPoints);
            y += spacing;

            // Is Active
            chkIsActive = new CheckBox { Text = "Đang hoạt động", Location = new Point(150, y), Size = new Size(200, 20), Checked = true };
            this.Controls.Add(chkIsActive);
            y += spacing + 10;

            // Buttons
            btnSave = new Button { Text = "Lưu", Location = new Point(200, y), Size = new Size(70, 30), BackColor = Color.LightGreen };
            btnCancel = new Button { Text = "Hủy", Location = new Point(280, y), Size = new Size(70, 30), BackColor = Color.LightCoral };
            
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên hoạt động!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpEndDate.Value < dtpStartDate.Value)
            {
                MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var activity = new Activity
            {
                Id = DataManager.Activities.Count > 0 ? DataManager.Activities.Max(a => a.Id) + 1 : 1,
                Name = txtName.Text,
                Description = txtDescription.Text,
                StartDate = dtpStartDate.Value,
                EndDate = dtpEndDate.Value,
                Location = txtLocation.Text,
                MaxParticipants = (int)numMaxParticipants.Value,
                Points = (int)numPoints.Value,
                IsActive = chkIsActive.Checked
            };

            DataManager.Activities.Add(activity);
            MessageBox.Show("Thêm hoạt động thành công!", "Thông báo");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    // Program Entry Point
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            LoginForm loginForm = new LoginForm();
            Application.Run(loginForm);
        }
    }
}
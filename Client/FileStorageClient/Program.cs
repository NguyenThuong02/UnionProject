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
            this.BackColor = Color.WhiteSmoke;

            // Tạo panel chứa form đăng nhập
            Panel loginPanel = new Panel();
            loginPanel.Size = new Size(320, 220);
            loginPanel.Location = new Point((this.ClientSize.Width - loginPanel.Width) / 2, (this.ClientSize.Height - loginPanel.Height) / 2);
            loginPanel.BackColor = Color.White;
            loginPanel.BorderStyle = BorderStyle.FixedSingle;

            lblTitle = new Label();
            lblTitle.Text = "ĐĂNG NHẬP HỆ THỐNG";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkBlue;
            lblTitle.Location = new Point(30, 20);
            lblTitle.Size = new Size(260, 30);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            Label lblUsername = new Label();
            lblUsername.Text = "Tên đăng nhập:";
            lblUsername.Location = new Point(30, 70);
            lblUsername.Size = new Size(100, 20);

            txtUsername = new TextBox();
            txtUsername.Location = new Point(140, 68);
            txtUsername.Size = new Size(150, 20);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;

            Label lblPassword = new Label();
            lblPassword.Text = "Mật khẩu:";
            lblPassword.Location = new Point(30, 110);
            lblPassword.Size = new Size(100, 20);

            txtPassword = new TextBox();
            txtPassword.Location = new Point(140, 108);
            txtPassword.Size = new Size(150, 20);
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;

            btnLogin = new Button();
            btnLogin.Text = "Đăng nhập";
            btnLogin.Location = new Point(140, 150);
            btnLogin.Size = new Size(100, 35);
            btnLogin.BackColor = Color.RoyalBlue;
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Click += BtnLogin_Click;

            loginPanel.Controls.Add(lblTitle);
            loginPanel.Controls.Add(lblUsername);
            loginPanel.Controls.Add(txtUsername);
            loginPanel.Controls.Add(lblPassword);
            loginPanel.Controls.Add(txtPassword);
            loginPanel.Controls.Add(btnLogin);

            this.Controls.Add(loginPanel);
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
        private Panel mainPanel = new Panel();

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
            this.Icon = SystemIcons.Application;

            // Menu Strip
            menuStrip = new MenuStrip();
            menuStrip.BackColor = Color.RoyalBlue;
            menuStrip.ForeColor = Color.White;
            menuStrip.Padding = new Padding(10, 5, 0, 5);
            menuStrip.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            
            // Status Strip
            statusStrip = new StatusStrip();
            statusStrip.BackColor = Color.LightSteelBlue;
            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel();
            statusLabel.Text = $"Xin chào: {DataManager.CurrentUser.FullName} ({DataManager.CurrentUser.Role})";
            statusLabel.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            ToolStripStatusLabel timeLabel = new ToolStripStatusLabel();
            timeLabel.Alignment = ToolStripItemAlignment.Right;
            timeLabel.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            
            statusStrip.Items.Add(statusLabel);
            statusStrip.Items.Add(timeLabel);

            // Main Panel
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.WhiteSmoke;
            mainPanel.Padding = new Padding(15);

            this.Controls.Add(mainPanel);
            this.Controls.Add(statusStrip);
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;

            // Timer để cập nhật thời gian
            // Timer để cập nhật thời gian
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 60000; // 1 phút
            timer.Tick += (s, e) => {
                timeLabel.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            };
            timer.Start();
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
            
            Panel welcomePanel = new Panel();
            welcomePanel.Size = new Size(600, 400);
            welcomePanel.Location = new Point((mainPanel.Width - 600) / 2, (mainPanel.Height - 400) / 2);
            welcomePanel.BackColor = Color.White;
            welcomePanel.BorderStyle = BorderStyle.None;
            
            Label titleLabel = new Label();
            titleLabel.Text = "HỆ THỐNG QUẢN LÝ HOẠT ĐỘNG ĐOÀN SỞ";
            titleLabel.Font = new Font("Arial", 18, FontStyle.Bold);
            titleLabel.ForeColor = Color.RoyalBlue;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Padding = new Padding(0, 30, 0, 30);
            
            Label welcomeLabel = new Label();
            welcomeLabel.Text = $"Chào mừng, {DataManager.CurrentUser.FullName}!";
            welcomeLabel.Font = new Font("Arial", 14, FontStyle.Regular);
            welcomeLabel.ForeColor = Color.DarkBlue;
            welcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            welcomeLabel.Dock = DockStyle.Top;
            welcomeLabel.Padding = new Padding(0, 10, 0, 20);
            
            Label roleLabel = new Label();
            roleLabel.Text = $"Vai trò: {DataManager.CurrentUser.Role}";
            roleLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            roleLabel.ForeColor = Color.DarkSlateGray;
            roleLabel.TextAlign = ContentAlignment.MiddleCenter;
            roleLabel.Dock = DockStyle.Top;
            roleLabel.Padding = new Padding(0, 0, 0, 30);
            
            Label infoLabel = new Label();
            infoLabel.Text = "Vui lòng chọn chức năng từ menu phía trên để bắt đầu.";
            infoLabel.Font = new Font("Arial", 11, FontStyle.Regular);
            infoLabel.ForeColor = Color.DimGray;
            infoLabel.TextAlign = ContentAlignment.MiddleCenter;
            infoLabel.Dock = DockStyle.Top;
            
            // Thêm thông tin thống kê
            FlowLayoutPanel statsPanel = new FlowLayoutPanel();
            statsPanel.FlowDirection = FlowDirection.LeftToRight;
            statsPanel.WrapContents = false;
            statsPanel.AutoSize = true;
            statsPanel.Dock = DockStyle.Bottom;
            statsPanel.Padding = new Padding(0, 30, 0, 0);
            
            AddStatCard(statsPanel, "Đoàn viên", DataManager.Members.Count.ToString(), Color.DodgerBlue);
            AddStatCard(statsPanel, "Hoạt động", DataManager.Activities.Count.ToString(), Color.ForestGreen);
            AddStatCard(statsPanel, "Hoạt động sắp tới", 
                DataManager.Activities.Count(a => a.StartDate > DateTime.Now).ToString(), Color.OrangeRed);
            
            welcomePanel.Controls.Add(statsPanel);
            welcomePanel.Controls.Add(infoLabel);
            welcomePanel.Controls.Add(roleLabel);
            welcomePanel.Controls.Add(welcomeLabel);
            welcomePanel.Controls.Add(titleLabel);
            
            mainPanel.Controls.Add(welcomePanel);
        }

        private void AddStatCard(FlowLayoutPanel panel, string title, string value, Color color)
        {
            Panel card = new Panel();
            card.Size = new Size(150, 100);
            card.Margin = new Padding(15, 0, 15, 0);
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.None;
            
            // Tạo hiệu ứng đổ bóng
            card.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle, 
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid);
            };
            
            Label titleLabel = new Label();
            titleLabel.Text = title;
            titleLabel.Font = new Font("Arial", 10, FontStyle.Regular);
            titleLabel.ForeColor = Color.DimGray;
            titleLabel.Location = new Point(0, 15);
            titleLabel.Size = new Size(150, 20);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            
            Label valueLabel = new Label();
            valueLabel.Text = value;
            valueLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            valueLabel.ForeColor = color;
            valueLabel.Location = new Point(0, 40);
            valueLabel.Size = new Size(150, 40);
            valueLabel.TextAlign = ContentAlignment.MiddleCenter;
            
            card.Controls.Add(titleLabel);
            card.Controls.Add(valueLabel);
            
            panel.Controls.Add(card);
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
        title.Font = new Font("Arial", 16, FontStyle.Bold);
        title.ForeColor = Color.DarkBlue;
        title.Location = new Point(20, 20);
        title.Size = new Size(400, 30);
        
        // Chọn hoạt động
        Label lblActivity = new Label();
        lblActivity.Text = "Chọn hoạt động:";
        lblActivity.Location = new Point(20, 70);
        lblActivity.Size = new Size(120, 20);
        
        ComboBox cboActivities = new ComboBox();
        cboActivities.Location = new Point(150, 68);
        cboActivities.Size = new Size(300, 25);
        cboActivities.DropDownStyle = ComboBoxStyle.DropDownList;
        cboActivities.DisplayMember = "Name";
        cboActivities.ValueMember = "Id";
        cboActivities.DataSource = DataManager.Activities.Where(a => a.IsActive).ToList();
        
        // DataGridView cho danh sách đoàn viên
        DataGridView dgvMembers = new DataGridView();
        dgvMembers.Location = new Point(20, 110);
        dgvMembers.Size = new Size(700, 300);
        dgvMembers.AutoGenerateColumns = false;
        dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvMembers.BackgroundColor = Color.White;
        dgvMembers.BorderStyle = BorderStyle.Fixed3D;
        dgvMembers.AllowUserToAddRows = false;
        dgvMembers.ReadOnly = false;
        
        dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MemberId", HeaderText = "ID", Width = 50, ReadOnly = true });
        dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Code", HeaderText = "Mã ĐV", Width = 80, ReadOnly = true });
        dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FullName", HeaderText = "Họ tên", Width = 150, ReadOnly = true });
        dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Class", HeaderText = "Lớp", Width = 80, ReadOnly = true });
        dgvMembers.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "IsRegistered", HeaderText = "Đăng ký", Width = 70 });
        dgvMembers.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "IsAttended", HeaderText = "Tham gia", Width = 70 });
        dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PointsEarned", HeaderText = "Điểm", Width = 60 });
        
        Button btnSave = new Button();
        btnSave.Text = "Lưu thay đổi";
        btnSave.Location = new Point(20, 420);
        btnSave.Size = new Size(120, 35);
        btnSave.BackColor = Color.LightGreen;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Cursor = Cursors.Hand;
        
        // Xử lý sự kiện khi chọn hoạt động
        cboActivities.SelectedIndexChanged += (s, e) => {
            if (cboActivities.SelectedItem != null)
            {
                var activity = (Activity)cboActivities.SelectedItem;
                LoadParticipationData(dgvMembers, activity.Id);
            }
        };
        
        // Xử lý sự kiện khi lưu thay đổi
        btnSave.Click += (s, e) => {
            if (cboActivities.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn hoạt động!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            SaveParticipationData(dgvMembers, ((Activity)cboActivities.SelectedItem).Id);
            MessageBox.Show("Lưu thông tin tham gia thành công!", "Thông báo");
        };
        
        mainPanel.Controls.Add(title);
        mainPanel.Controls.Add(lblActivity);
        mainPanel.Controls.Add(cboActivities);
        mainPanel.Controls.Add(dgvMembers);
        mainPanel.Controls.Add(btnSave);
        
        // Nếu có hoạt động, tải dữ liệu cho hoạt động đầu tiên
        if (cboActivities.Items.Count > 0)
        {
            cboActivities.SelectedIndex = 0;
        }
    }

    private void LoadParticipationData(DataGridView dgv, int activityId)
    {
        var participationData = new List<dynamic>();
        
        foreach (var member in DataManager.Members.Where(m => m.IsActive))
        {
            var participation = DataManager.Participations
                .FirstOrDefault(p => p.MemberId == member.Id && p.ActivityId == activityId);
            
            participationData.Add(new
            {
                MemberId = member.Id,
                Code = member.Code,
                FullName = member.FullName,
                Class = member.Class,
                IsRegistered = participation != null,
                IsAttended = participation != null ? participation.IsAttended : false,
                PointsEarned = participation != null ? participation.PointsEarned : 0
            });
        }
        
        dgv.DataSource = participationData;
    }

    private void SaveParticipationData(DataGridView dgv, int activityId)
    {
        foreach (DataGridViewRow row in dgv.Rows)
        {
            int memberId = Convert.ToInt32(row.Cells["MemberId"].Value);
            bool isRegistered = Convert.ToBoolean(row.Cells["IsRegistered"].Value);
            bool isAttended = Convert.ToBoolean(row.Cells["IsAttended"].Value);
            int pointsEarned = Convert.ToInt32(row.Cells["PointsEarned"].Value);
            
            var participation = DataManager.Participations
                .FirstOrDefault(p => p.MemberId == memberId && p.ActivityId == activityId);
            
            if (isRegistered)
            {
                if (participation == null)
                {
                    // Thêm mới
                    participation = new Participation
                    {
                        Id = DataManager.Participations.Count > 0 ? DataManager.Participations.Max(p => p.Id) + 1 : 1,
                        MemberId = memberId,
                        ActivityId = activityId,
                        RegisterDate = DateTime.Now,
                        IsAttended = isAttended,
                        PointsEarned = pointsEarned
                    };
                    DataManager.Participations.Add(participation);
                }
                else
                {
                    // Cập nhật
                    participation.IsAttended = isAttended;
                    participation.PointsEarned = pointsEarned;
                }
            }
            else if (participation != null)
            {
                // Xóa
                DataManager.Participations.Remove(participation);
            }
        }
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
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.ForeColor = Color.DarkBlue;
            title.Location = new Point(20, 20);
            title.Size = new Size(350, 30);
            
            // Tìm kiếm đoàn viên
            Label lblSearch = new Label();
            lblSearch.Text = "Tìm kiếm:";
            lblSearch.Location = new Point(20, 70);
            lblSearch.Size = new Size(80, 20);
            
            TextBox txtSearch = new TextBox();
            txtSearch.Location = new Point(110, 68);
            txtSearch.Size = new Size(200, 25);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            
            Button btnSearch = new Button();
            btnSearch.Text = "Tìm";
            btnSearch.Location = new Point(320, 67);
            btnSearch.Size = new Size(60, 25);
            btnSearch.BackColor = Color.LightBlue;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            
            // DataGridView cho danh sách đoàn viên
            DataGridView dgvMembers = new DataGridView();
            dgvMembers.Location = new Point(20, 110);
            dgvMembers.Size = new Size(700, 300);
            dgvMembers.AutoGenerateColumns = false;
            dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.BackgroundColor = Color.White;
            dgvMembers.BorderStyle = BorderStyle.Fixed3D;
            dgvMembers.AllowUserToAddRows = false;
            dgvMembers.ReadOnly = false;
            
            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Width = 50, ReadOnly = true });
            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Code", HeaderText = "Mã ĐV", Width = 80, ReadOnly = true });
            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FullName", HeaderText = "Họ tên", Width = 150, ReadOnly = true });
            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Class", HeaderText = "Lớp", Width = 80, ReadOnly = true });
            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrainingPoints", HeaderText = "Điểm rèn luyện", Width = 100 });
            
            Button btnSave = new Button();
            btnSave.Text = "Lưu thay đổi";
            btnSave.Location = new Point(20, 420);
            btnSave.Size = new Size(120, 35);
            btnSave.BackColor = Color.LightGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Cursor = Cursors.Hand;
            
            // Xử lý sự kiện tìm kiếm
            btnSearch.Click += (s, e) => {
                string searchText = txtSearch.Text.Trim().ToLower();
                var filteredMembers = DataManager.Members.Where(m => 
                    m.IsActive && (m.Code.ToLower().Contains(searchText) || 
                                  m.FullName.ToLower().Contains(searchText) ||
                                  m.Class.ToLower().Contains(searchText))).ToList();
                dgvMembers.DataSource = filteredMembers;
            };
            
            // Xử lý sự kiện lưu thay đổi
            btnSave.Click += (s, e) => {
                foreach (DataGridViewRow row in dgvMembers.Rows)
                {
                    int id = Convert.ToInt32(row.Cells["Id"].Value);
                    int points = Convert.ToInt32(row.Cells["TrainingPoints"].Value);
                    
                    var member = DataManager.Members.FirstOrDefault(m => m.Id == id);
                    if (member != null)
                    {
                        member.TrainingPoints = points;
                    }
                }
                
                MessageBox.Show("Cập nhật điểm rèn luyện thành công!", "Thông báo");
            };
            
            mainPanel.Controls.Add(title);
            mainPanel.Controls.Add(lblSearch);
            mainPanel.Controls.Add(txtSearch);
            mainPanel.Controls.Add(btnSearch);
            mainPanel.Controls.Add(dgvMembers);
            mainPanel.Controls.Add(btnSave);
            
            // Hiển thị tất cả đoàn viên ban đầu
            dgvMembers.DataSource = DataManager.Members.Where(m => m.IsActive).ToList();
        }

        private void ShowAwardManagement()
        {
            mainPanel.Controls.Clear();
            
            Label title = new Label();
            title.Text = "QUẢN LÝ KHEN THƯỞNG";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.ForeColor = Color.DarkBlue;
            title.Location = new Point(20, 20);
            title.Size = new Size(300, 30);
            
            // Chọn đoàn viên
            Label lblMember = new Label();
            lblMember.Text = "Chọn đoàn viên:";
            lblMember.Location = new Point(20, 70);
            lblMember.Size = new Size(120, 20);
            
            ComboBox cboMembers = new ComboBox();
            cboMembers.Location = new Point(150, 68);
            cboMembers.Size = new Size(250, 25);
            cboMembers.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMembers.DisplayMember = "FullName";
            cboMembers.ValueMember = "Id";
            cboMembers.DataSource = DataManager.Members.Where(m => m.IsActive).ToList();
            
            // Panel thêm khen thưởng
            GroupBox addAwardGroup = new GroupBox();
            addAwardGroup.Text = "Thêm khen thưởng mới";
            addAwardGroup.Location = new Point(20, 110);
            addAwardGroup.Size = new Size(380, 200);
            
            Label lblType = new Label();
            lblType.Text = "Loại khen thưởng:";
            lblType.Location = new Point(20, 30);
            lblType.Size = new Size(120, 20);
            
            ComboBox cboType = new ComboBox();
            cboType.Location = new Point(150, 28);
            cboType.Size = new Size(200, 25);
            cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboType.Items.AddRange(new string[] { "Giấy khen", "Bằng khen", "Huy chương", "Khác" });
            
            Label lblDescription = new Label();
            lblDescription.Text = "Mô tả:";
            lblDescription.Location = new Point(20, 70);
            lblDescription.Size = new Size(120, 20);
            
            TextBox txtDescription = new TextBox();
            txtDescription.Location = new Point(150, 68);
            txtDescription.Size = new Size(200, 60);
            txtDescription.Multiline = true;
            
            Label lblPoints = new Label();
            lblPoints.Text = "Điểm thưởng:";
            lblPoints.Location = new Point(20, 110);
            lblPoints.Size = new Size(120, 20);
            
            NumericUpDown numPoints = new NumericUpDown();
            numPoints.Location = new Point(150, 108);
            numPoints.Size = new Size(80, 25);
            numPoints.Minimum = 0;
            numPoints.Maximum = 50;
            numPoints.Value = 5;
            
            Button btnAddAward = new Button();
            btnAddAward.Text = "Thêm khen thưởng";
            btnAddAward.Location = new Point(150, 150);
            btnAddAward.Size = new Size(120, 30);
            btnAddAward.BackColor = Color.LightGreen;
            btnAddAward.FlatStyle = FlatStyle.Flat;
            btnAddAward.FlatAppearance.BorderSize = 0;
            btnAddAward.Cursor = Cursors.Hand;
            
            addAwardGroup.Controls.Add(lblType);
            addAwardGroup.Controls.Add(cboType);
            addAwardGroup.Controls.Add(lblDescription);
            addAwardGroup.Controls.Add(txtDescription);
            addAwardGroup.Controls.Add(lblPoints);
            addAwardGroup.Controls.Add(numPoints);
            addAwardGroup.Controls.Add(btnAddAward);
            
            // DataGridView cho danh sách khen thưởng
            Label lblAwardList = new Label();
            lblAwardList.Text = "Danh sách khen thưởng:";
            lblAwardList.Location = new Point(20, 320);
            lblAwardList.Size = new Size(200, 20);
            
            DataGridView dgvAwards = new DataGridView();
            dgvAwards.Location = new Point(20, 350);
            dgvAwards.Size = new Size(700, 200);
            dgvAwards.AutoGenerateColumns = false;
            dgvAwards.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAwards.BackgroundColor = Color.White;
            dgvAwards.BorderStyle = BorderStyle.Fixed3D;
            dgvAwards.AllowUserToAddRows = false;
            dgvAwards.ReadOnly = true;
            
            dgvAwards.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Width = 50 });
            dgvAwards.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MemberName", HeaderText = "Đoàn viên", Width = 150 });
            dgvAwards.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Type", HeaderText = "Loại khen thưởng", Width = 120 });
            dgvAwards.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Mô tả", Width = 200 });
            dgvAwards.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "AwardDate", HeaderText = "Ngày khen thưởng", Width = 100 });
            dgvAwards.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Points", HeaderText = "Điểm", Width = 50 });
            
            // Xử lý sự kiện thêm khen thưởng
            btnAddAward.Click += (s, e) => {
                if (cboMembers.SelectedItem == null || cboType.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn đoàn viên và loại khen thưởng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                var award = new Award
                {
                    Id = DataManager.Awards.Count > 0 ? DataManager.Awards.Max(a => a.Id) + 1 : 1,
                    MemberId = (int)cboMembers.SelectedValue,
                    Type = cboType.SelectedItem.ToString(),
                    Description = txtDescription.Text,
                    AwardDate = DateTime.Now,
                    Points = (int)numPoints.Value
                };
                
                DataManager.Awards.Add(award);
                
                // Cập nhật điểm rèn luyện cho đoàn viên
                var member = DataManager.Members.FirstOrDefault(m => m.Id == award.MemberId);
                if (member != null)
                {
                    member.TrainingPoints += award.Points;
                }
                
                MessageBox.Show("Thêm khen thưởng thành công!", "Thông báo");
                LoadAwardData(dgvAwards);
            };
            
            // Hàm tải dữ liệu khen thưởng
            void LoadAwardData(DataGridView dgv)
            {
                var awardData = DataManager.Awards.Select(a => new
                {
                    a.Id,
                    MemberName = DataManager.Members.FirstOrDefault(m => m.Id == a.MemberId)?.FullName,
                    a.Type,
                    a.Description,
                    AwardDate = a.AwardDate.ToString("dd/MM/yyyy"),
                    a.Points
                }).ToList();
                
                dgv.DataSource = awardData;
            }
            
            // Tải dữ liệu ban đầu
            LoadAwardData(dgvAwards);
            
            mainPanel.Controls.Add(title);
            mainPanel.Controls.Add(lblMember);
            mainPanel.Controls.Add(cboMembers);
            mainPanel.Controls.Add(addAwardGroup);
            mainPanel.Controls.Add(lblAwardList);
            mainPanel.Controls.Add(dgvAwards);
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
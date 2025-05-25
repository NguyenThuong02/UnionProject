using System;
using System.Windows.Forms;
using YouthUnionManagement.Models;
using YouthUnionManagement.Data;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace YouthUnionManagement.Forms
{
    public partial class MainForm : Form
    {
        private User _currentUser;

        public MainForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            LoadUserInfo();
            ConfigureMenuAccess();
        }

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMembers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMembersList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMembersAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActivities = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActivitiesList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActivitiesAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActivitiesParticipation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTrainingPoints = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTrainingPointsList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTrainingPointsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRewards = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRewardsList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRewardsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportMembers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportActivities = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportTrainingPoints = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportRewards = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdminUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSystem,
            this.mnuMembers,
            this.mnuActivities,
            this.mnuTrainingPoints,
            this.mnuRewards,
            this.mnuReports,
            this.mnuAdmin});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // mnuSystem
            // 
            this.mnuSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChangePassword,
            this.mnuLogout,
            this.mnuExit});
            this.mnuSystem.Name = "mnuSystem";
            this.mnuSystem.Size = new System.Drawing.Size(69, 20);
            this.mnuSystem.Text = "Hệ thống";
            // 
            // mnuChangePassword
            // 
            this.mnuChangePassword.Name = "mnuChangePassword";
            this.mnuChangePassword.Size = new System.Drawing.Size(180, 22);
            this.mnuChangePassword.Text = "Đổi mật khẩu";
            // 
            // mnuLogout
            // 
            this.mnuLogout.Name = "mnuLogout";
            this.mnuLogout.Size = new System.Drawing.Size(180, 22);
            this.mnuLogout.Text = "Đăng xuất";
            this.mnuLogout.Click += new System.EventHandler(this.mnuLogout_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(180, 22);
            this.mnuExit.Text = "Thoát";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuMembers
            // 
            this.mnuMembers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMembersList,
            this.mnuMembersAdd});
            this.mnuMembers.Name = "mnuMembers";
            this.mnuMembers.Size = new System.Drawing.Size(115, 20);
            this.mnuMembers.Text = "Quản lý đoàn viên";
            // 
            // mnuMembersList
            // 
            this.mnuMembersList.Name = "mnuMembersList";
            this.mnuMembersList.Size = new System.Drawing.Size(180, 22);
            this.mnuMembersList.Text = "Danh sách đoàn viên";
            // 
            // mnuMembersAdd
            // 
            this.mnuMembersAdd.Name = "mnuMembersAdd";
            this.mnuMembersAdd.Size = new System.Drawing.Size(180, 22);
            this.mnuMembersAdd.Text = "Thêm đoàn viên";
            // 
            // mnuActivities
            // 
            this.mnuActivities.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuActivitiesList,
            this.mnuActivitiesAdd,
            this.mnuActivitiesParticipation});
            this.mnuActivities.Name = "mnuActivities";
            this.mnuActivities.Size = new System.Drawing.Size(123, 20);
            this.mnuActivities.Text = "Quản lý hoạt động";
            // 
            // mnuActivitiesList
            // 
            this.mnuActivitiesList.Name = "mnuActivitiesList";
            this.mnuActivitiesList.Size = new System.Drawing.Size(180, 22);
            this.mnuActivitiesList.Text = "Danh sách hoạt động";
            // 
            // mnuActivitiesAdd
            // 
            this.mnuActivitiesAdd.Name = "mnuActivitiesAdd";
            this.mnuActivitiesAdd.Size = new System.Drawing.Size(180, 22);
            this.mnuActivitiesAdd.Text = "Thêm hoạt động";
            // 
            // mnuActivitiesParticipation
            // 
            this.mnuActivitiesParticipation.Name = "mnuActivitiesParticipation";
            this.mnuActivitiesParticipation.Size = new System.Drawing.Size(180, 22);
            this.mnuActivitiesParticipation.Text = "Điểm danh tham gia";
            // 
            // mnuTrainingPoints
            // 
            this.mnuTrainingPoints.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTrainingPointsList,
            this.mnuTrainingPointsAdd});
            this.mnuTrainingPoints.Name = "mnuTrainingPoints";
            this.mnuTrainingPoints.Size = new System.Drawing.Size(139, 20);
            this.mnuTrainingPoints.Text = "Quản lý điểm rèn luyện";
            // 
            // mnuTrainingPointsList
            // 
            this.mnuTrainingPointsList.Name = "mnuTrainingPointsList";
            this.mnuTrainingPointsList.Size = new System.Drawing.Size(180, 22);
            this.mnuTrainingPointsList.Text = "Danh sách điểm";
            // 
            // mnuTrainingPointsAdd
            // 
            this.mnuTrainingPointsAdd.Name = "mnuTrainingPointsAdd";
            this.mnuTrainingPointsAdd.Size = new System.Drawing.Size(180, 22);
            this.mnuTrainingPointsAdd.Text = "Nhập điểm";
            // 
            // mnuRewards
            // 
            this.mnuRewards.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRewardsList,
            this.mnuRewardsAdd});
            this.mnuRewards.Name = "mnuRewards";
            this.mnuRewards.Size = new System.Drawing.Size(125, 20);
            this.mnuRewards.Text = "Quản lý khen thưởng";
            // 
            // mnuRewardsList
            // 
            this.mnuRewardsList.Name = "mnuRewardsList";
            this.mnuRewardsList.Size = new System.Drawing.Size(180, 20);
        }

        #region Event Handlers

        private void mnuLogout_Click(object sender, EventArgs e)
        {
            // Đăng xuất và quay lại form đăng nhập
            CurrentUser.User = null;
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            // Thoát ứng dụng
            Application.Exit();
        }

        private void mnuChangePassword_Click(object sender, EventArgs e)
        {
            // Hiển thị form đổi mật khẩu
            MessageBox.Show("Chức năng đổi mật khẩu sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuMembersList_Click(object sender, EventArgs e)
        {
            // Hiển thị danh sách đoàn viên
            MessageBox.Show("Chức năng xem danh sách đoàn viên sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuMembersAdd_Click(object sender, EventArgs e)
        {
            // Hiển thị form thêm đoàn viên
            MessageBox.Show("Chức năng thêm đoàn viên sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuActivitiesList_Click(object sender, EventArgs e)
        {
            // Hiển thị danh sách hoạt động
            MessageBox.Show("Chức năng xem danh sách hoạt động sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuActivitiesAdd_Click(object sender, EventArgs e)
        {
            // Hiển thị form thêm hoạt động
            MessageBox.Show("Chức năng thêm hoạt động sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuActivitiesParticipation_Click(object sender, EventArgs e)
        {
            // Hiển thị form điểm danh tham gia hoạt động
            MessageBox.Show("Chức năng điểm danh tham gia hoạt động sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuTrainingPointsList_Click(object sender, EventArgs e)
        {
            // Hiển thị danh sách điểm rèn luyện
            MessageBox.Show("Chức năng xem danh sách điểm rèn luyện sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuTrainingPointsAdd_Click(object sender, EventArgs e)
        {
            // Hiển thị form nhập điểm rèn luyện
            MessageBox.Show("Chức năng nhập điểm rèn luyện sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuRewardsList_Click(object sender, EventArgs e)
        {
            // Hiển thị danh sách khen thưởng
            MessageBox.Show("Chức năng xem danh sách khen thưởng sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuRewardsAdd_Click(object sender, EventArgs e)
        {
            // Hiển thị form thêm khen thưởng
            MessageBox.Show("Chức năng thêm khen thưởng sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuReportMembers_Click(object sender, EventArgs e)
        {
            // Hiển thị báo cáo thống kê đoàn viên
            MessageBox.Show("Chức năng báo cáo thống kê đoàn viên sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuReportActivities_Click(object sender, EventArgs e)
        {
            // Hiển thị báo cáo thống kê hoạt động
            MessageBox.Show("Chức năng báo cáo thống kê hoạt động sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuReportTrainingPoints_Click(object sender, EventArgs e)
        {
            // Hiển thị báo cáo thống kê điểm rèn luyện
            MessageBox.Show("Chức năng báo cáo thống kê điểm rèn luyện sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuReportRewards_Click(object sender, EventArgs e)
        {
            // Hiển thị báo cáo thống kê khen thưởng
            MessageBox.Show("Chức năng báo cáo thống kê khen thưởng sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuAdminUsers_Click(object sender, EventArgs e)
        {
            // Hiển thị form quản lý người dùng
            MessageBox.Show("Chức năng quản lý người dùng sẽ được triển khai sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Helper Methods

        private void LoadUserInfo()
        {
            // Hiển thị thông tin người dùng đăng nhập trên thanh trạng thái
            CurrentUser.User = _currentUser;
            lblUser.Text = $"Người dùng: {_currentUser.Username} | Vai trò: {_currentUser.Role?.Name ?? "Chưa xác định"}";
            lblStatus.Text = "Sẵn sàng";
        }

        private void ConfigureMenuAccess()
        {
            // Cấu hình quyền truy cập menu dựa trên vai trò người dùng
            mnuAdmin.Visible = CurrentUser.IsAdmin;
            
            // Các menu khác có thể được cấu hình tùy theo yêu cầu
            // Ví dụ: chỉ quản lý mới có thể thêm hoạt động
            mnuActivitiesAdd.Visible = CurrentUser.IsManager;
            mnuRewardsAdd.Visible = CurrentUser.IsManager;
        }

        #endregion
    }
}
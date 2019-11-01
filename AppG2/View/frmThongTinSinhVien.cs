using AppG2.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppG2.Model;

namespace AppG2.View
{
    public partial class frmThongTinSinhVien : Form
    {
        Image img;
        string maSinhVien;
        string pathDirectoryImg;
        string pathAvatarImg;

        #region Path Data File
        string pathStudentDataFile;
        string pathHistoryFile;
        #endregion

        public frmThongTinSinhVien(string maSinhVien)
        {
            InitializeComponent();
            this.maSinhVien = maSinhVien;
            pictureBox1.AllowDrop = true;
            pathDirectoryImg = Application.StartupPath + "/img";
            pathAvatarImg = pathDirectoryImg + "/avatar.png";
            pathStudentDataFile = Application.StartupPath + @"\Data\student.txt"; // ki tu @ giup xem cac ki tu dac biet nhu ki tu binh thuong
            pathHistoryFile = Application.StartupPath + @"\Data\HistoryLearning.txt";
            if (File.Exists(pathAvatarImg))
            {
                //pictureBox1.Image = Image.FromFile(pathAvatarImg);
                FileStream fileStream = new FileStream(pathAvatarImg, FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(fileStream);
                fileStream.Close();
                reloadDataHistory(maSinhVien);
            }
        }

        
        
        private void reloadDataHistory(string maSinhVien)
        {
            bdsQuaTrinhHocTap.DataSource = null;
            dtgQuaTrinhHocTap.AutoGenerateColumns = false;
            //var student = StudentService.GetStudent(maSinhVien);
            var student = StudentService.GetStudent(pathStudentDataFile, pathHistoryFile, maSinhVien);
            if (student == null)
                throw new Exception("Khong ton tai sinh vien nay");
            else
            {
                txtMaSinhVien.Text = student.IDStudent;
                txtHo.Text = student.LastName;
                txtTen.Text = student.FirstName;
                txtQueQuan.Text = student.POB;
                dtNgaySinh.Value = student.DOB;
                cbGioiTinh.Checked = student.Gender == Model.GENDER.Male;
                if (student.ListHistoryLearning != null)
                {
                    bdsQuaTrinhHocTap.DataSource = student.ListHistoryLearning;
                }
            }
            dtgQuaTrinhHocTap.DataSource = bdsQuaTrinhHocTap;
        } 

        private void LinkLabel1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();
            openfileDialog.Title = "Chon anh dai dien";
            openfileDialog.Filter = "File anh(*.png; *.jpg)|*.png; *.jpg";
            if (openfileDialog.ShowDialog() == DialogResult.OK)
            {
                img = Image.FromFile(openfileDialog.FileName);
                pictureBox1.Image = img;
            }
        }

        private void ButtonCapNhat_Click(object sender, EventArgs e)
        {
            #region Cap_nhat_anh_dai_dien
            bool imageSave = false;
            if (img != null )
            {
                if (!Directory.Exists(pathDirectoryImg))
                {
                    Directory.CreateDirectory(pathDirectoryImg);
                }
                //System.GC.Collect();
                //System.GC.WaitForPendingFinalizers();
                //File.Delete(pathAvatarImg);
                img.Save(pathAvatarImg);
                imageSave = true;
            }
            #endregion
            if (imageSave)
            {
                MessageBox.Show("Da cap nhat thong tin thanh cong",
                                "Thong bao",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void PictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            var rs = (string[])e.Data.GetData(DataFormats.FileDrop);
            var filePath = rs.FirstOrDefault(); // lay thang dau tien neu co, ng lai null
            img = Image.FromFile(filePath);
            pictureBox1.Image = img;
        }

        private void PictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void MniXoaAvt_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Xoa Anh Dai Dien");
            pictureBox1.Image = Properties.Resources.avatar;
            File.Delete(pathAvatarImg);
        }

        private void FrmThongTinSinhVien_Load(object sender, EventArgs e)
        {

        }

        private void DtgQuaTrinhHocTap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var historyID = dtgQuaTrinhHocTap.CurrentRow.Cells[0].Value.ToString();
            StudentService.DeleteHistoryLearning(pathHistoryFile, historyID);
            dtgQuaTrinhHocTap.Rows.RemoveAt(dtgQuaTrinhHocTap.CurrentRow.Index);
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var f = new frmQuaTrinhHocTapChiTiet(null, maSinhVien, pathHistoryFile);
            if(f.ShowDialog() == DialogResult.OK)
            {
                // Tiến hành nạp lại dữ liệu lên lưới
                reloadDataHistory(maSinhVien);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            var history = bdsQuaTrinhHocTap.Current as HistoryLearning;
            if(history != null)
            {
                var f = new frmQuaTrinhHocTapChiTiet(history, maSinhVien, pathHistoryFile);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    // Tiến hành nạp lại dữ liệu lên lưới
                    reloadDataHistory(maSinhVien);
                }
            }
        }
    }
}

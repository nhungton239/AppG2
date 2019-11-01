using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppG2.Model;
using AppG2.Controller;

namespace AppG2.View
{
    public partial class frmQuaTrinhHocTapChiTiet : Form
    {
        HistoryLearning history;
        string pathHistoryFile;
        string maSinhVien;
        public frmQuaTrinhHocTapChiTiet(HistoryLearning history = null, string maSinhVien = null, string pathHistoryFile = null)
        {
            InitializeComponent();
            this.history = history;
            this.maSinhVien = maSinhVien;
            this.pathHistoryFile = pathHistoryFile;
            if(history != null)
            {
                // Chỉnh sửa
                this.Text = "Chỉnh sửa QTHT";
                numTuNam.Value = history.YearFrom;
                numDenNam.Value = history.YearEnd;
                txtNoiHoc.Text = history.Address;
            }
            else
            {
                // Thêm mới
                this.Text = "Thêm mới QTHT";
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnDongY_Click(object sender, EventArgs e)
        {
            if (history != null)
            {
                // Cập nhật
                var yearFrom = numTuNam.Value;
                var yearEnd = numDenNam.Value;
                var address = txtNoiHoc.Text;
                StudentService.EditHistoryLearning(history.IDHistoryLearning, (int)yearFrom, (int)yearEnd, address, pathHistoryFile);
            }
            else
            {
                // Thêm mới
                var yearFrom = numTuNam.Value;
                var yearEnd = numDenNam.Value;
                var address = txtNoiHoc.Text;
                StudentService.CreateHistoryLearning((int)yearFrom, (int)yearEnd, address, maSinhVien, pathHistoryFile);
            }
            MessageBox.Show("Đã cập nhật dữ liệu thành công");
            DialogResult = DialogResult.OK; // Đóng Form
        }

        private void FrmQuaTrinhHocTapChiTiet_Load(object sender, EventArgs e)
        {

        }
    }
}

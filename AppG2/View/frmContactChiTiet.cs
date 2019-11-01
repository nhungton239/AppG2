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
    public partial class frmContactChiTiet : Form
    {
        Contact contact;
        string pathDataFile;
        public frmContactChiTiet(Contact contact = null, string pathDataFile = null)
        {
            InitializeComponent();
            this.pathDataFile = pathDataFile;
            this.contact = contact;
            if(contact != null)
            {
                txtContactName.Text = contact.ContactName;
                txtContactPhone.Text = contact.ContactPhone;
                txtEmail.Text = contact.Email;
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (contact != null)
            {
                // Cập nhật
                var contactID = contact.ID;
                var contactName = txtContactName.Text;
                var contactPhone = txtContactPhone.Text;
                var email = txtEmail.Text;
                ContactService.editContact(contactID, contactName, contactPhone, email, pathDataFile);
            }
            else
            {
                // Thêm mới
                var contactName = txtContactName.Text;
                var contactPhone = txtContactPhone.Text;
                var email = txtEmail.Text;
                ContactService.addContact(contactName, contactPhone, email, pathDataFile);
            }
            MessageBox.Show("Đã cập nhật dữ liệu thành công");
            DialogResult = DialogResult.OK; // Đóng Form
        }

        private void TxtContactName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

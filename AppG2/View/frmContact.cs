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
    public partial class frmContact : Form
    {
        string pathDataFile;
        public frmContact()
        {
            InitializeComponent();
            pathDataFile = Application.StartupPath + @"\Data\contact.txt";
            loadContact();
        }
        private void loadContact()
        {
            
            bdsContact.DataSource = null;
            dtgContact.AutoGenerateColumns = false;
            List<Contact> lstContacts  = ContactService.GetContacts(pathDataFile);
            if (lstContacts == null)
                throw new Exception("Chưa có thông tin về danh bạ");
            else
            {
                bdsContact.DataSource = lstContacts;
            }
            dtgContact.DataSource = bdsContact;
        }

        private void deleteContact()
        {

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var contactID = dtgContact.CurrentRow.Cells[4].Value.ToString();
            ContactService.deleteContact(pathDataFile, contactID);
            dtgContact.Rows.RemoveAt(dtgContact.CurrentRow.Index);
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var f = new frmContactChiTiet(null, pathDataFile);
            if (f.ShowDialog() == DialogResult.OK)
            {
                loadContact();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            var contact = bdsContact.Current as Contact;
            if (contact != null)
            {
                var f = new frmContactChiTiet(contact, pathDataFile);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    loadContact();
                }
            }
            
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string value = txtSearch.Text;
            bdsContact.DataSource = null;
            dtgContact.AutoGenerateColumns = false;
            List<Contact> lstSearchContacts = ContactService.SearchContacts(pathDataFile, value);
            if (lstSearchContacts == null)
                throw new Exception("Không tìm thấy");
            else
            {
                bdsContact.DataSource = lstSearchContacts;
            }
            dtgContact.DataSource = bdsContact;
        }
    }
}

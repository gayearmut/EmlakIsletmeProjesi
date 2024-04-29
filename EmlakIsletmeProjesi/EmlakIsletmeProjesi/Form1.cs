using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmlakIsletmeProjesi
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		// Giriş yap butonuna tıklandığında çalışacak olay
		private void btnGirisYap_Click_1(object sender, EventArgs e)
		{
			IsletmeGiris grs = new IsletmeGiris(); // Yeni bir IsletmeGiris formu oluşturulur ve gösterilir
			grs.Show();
			this.Hide();
		}


		// Kaydol butonuna tıklandığında çalışacak olay
		private void btnKaydol_Click_1(object sender, EventArgs e)
		{
			IsletmeKayit kaydol = new IsletmeKayit();
			kaydol.Show();
			this.Hide();
		}

		// Çıkış yapılır
		private void btnCikisYap_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		
	}
}

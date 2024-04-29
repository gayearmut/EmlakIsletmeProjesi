using Npgsql;
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
	public partial class IsletmeKayit : Form
	{
		public IsletmeKayit()
		{
			InitializeComponent();
		}

		NpgsqlConnection connection = new NpgsqlConnection("server = localHost; port=5432;" +
			" Database= dbemlakisletmesi; user Id = postgres; password= 123456");

		// Kaydet butonuna tıklandığında gerçekleşecek olay
		private void btnKaydet_Click(object sender, EventArgs e)
		{
			// Eğer gerekli alanlardan herhangi biri boş mu kontrol edilir
			if (string.IsNullOrWhiteSpace(txtIsletmeAdi.Text) || string.IsNullOrWhiteSpace(txtYetkili.Text) || string.IsNullOrWhiteSpace(txtAdres.Text) ||
				string.IsNullOrWhiteSpace(txtTelefon.Text) || string.IsNullOrWhiteSpace(txtMail.Text) || string.IsNullOrWhiteSpace(txtSifre.Text))
			{
				MessageBox.Show("Kayıt işlemi başarısız. Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				try
				{
					connection.Open();
					NpgsqlCommand command = new NpgsqlCommand("insert into isletme (isletmeadi, yetkili, adres, telefon, mail, sifre)" +
						" values (@p1, @p2, @p3, @p4, @p5, @p6)", connection);

					command.Parameters.AddWithValue("@p1", txtIsletmeAdi.Text);
					command.Parameters.AddWithValue("@p2", txtYetkili.Text);
					command.Parameters.AddWithValue("@p3", txtAdres.Text);
					command.Parameters.AddWithValue("@p4", txtTelefon.Text);
					command.Parameters.AddWithValue("@p5", txtMail.Text);
					command.Parameters.AddWithValue("@p6", txtSifre.Text);

					command.ExecuteNonQuery();
					MessageBox.Show("İşletme başarılı bir şekilde kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

					
					Form1 form1 = new Form1();
					form1.Show();
					this.Hide();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					connection.Close();
				}
			}
		}

		//Giriş ekranına dönüş
		private void btnGeriDonus_Click(object sender, EventArgs e)
		{
			Form1 form1 = new Form1();
			form1.Show();
			this.Hide();
		}

		// Çıkış yap
		private void btnCikisYap_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}

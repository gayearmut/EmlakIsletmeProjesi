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
	public partial class IsletmeGiris : Form
	{
		public IsletmeGiris()
		{
			InitializeComponent();
		}
		// NpgsqlConnection nesnesi oluşturulur ve PostgreSQL veritabanına bağlanma scripti yazılır
		NpgsqlConnection connection = new NpgsqlConnection("server = localHost; port=5432;" +
			" Database= dbemlakisletmesi; user Id = postgres; password= 123456");


		// Giriş yap butonuna tıklandığında çalışacak komut
		private void btnGirisYap_Click(object sender, EventArgs e)
		{
			

			if (txtEposta.Text == "" || txtSifre.Text == "")//E-posta veya şifre alanları boş mu kontrolu yapılır
			{
				MessageBox.Show("Email adresi ve Şifre boş olamaz!", "Hata Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				try
				{
					connection.Open();
									  
					NpgsqlCommand command = new NpgsqlCommand("select * from isletme where mail=@p1 and sifre=@p2", connection);
					command.Parameters.AddWithValue("@p1", txtEposta.Text);
					command.Parameters.AddWithValue("@p2", txtSifre.Text);
					NpgsqlDataReader dr = command.ExecuteReader();

					if (dr.Read())// Sonuçlar okunduysa
					{
						MessageBox.Show("Giriş Başarılı!", "Bilgilendirilme Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);

						IsletmeAnasayfa anasayfa = new IsletmeAnasayfa();
						anasayfa.Show();
						this.Hide();
					}
					else //Hatalı şifre ya da mail adresi girilmesi durumu
					{
						MessageBox.Show("Yanlış Email veya Şifre", "Hata Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				catch (Exception ex)
				{
					
					MessageBox.Show("Error Connecting: " + ex.Message, "Hata Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					connection.Close();
				}
			}

		}
		//Çıkış Yapılır
		private void btnCikisYap_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}

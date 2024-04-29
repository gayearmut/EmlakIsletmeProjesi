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
	public partial class MusteriEkle : Form
	{
		public MusteriEkle()
		{
			InitializeComponent();
		}

		NpgsqlConnection connection = new NpgsqlConnection("server = localHost; port=5432;" +
			" Database= dbemlakisletmesi; user Id = postgres; password= 123456");

		// Müşteri bilgilerini veritabanına kaydetmek için sorgu oluştur
		private void btnKaydet_Click(object sender, EventArgs e)
		{
			// Müşteri bilgilerini veritabanına kaydetmek için sorgu oluştur
			connection.Open();
			NpgsqlCommand command = new NpgsqlCommand("insert into musteri (musteriid, musteriad, musterisoyad, musteritel, musterimail,musterituru) " +
				"values (@p1, @p2, @p3, @p4, @p5, @p6)", connection);
			command.Parameters.AddWithValue("@p1", int.Parse(txtMusteriid.Text));
			command.Parameters.AddWithValue("@p2", txtMusteriAd.Text);
			command.Parameters.AddWithValue("@p3", txtMusteriSoyad.Text);
			command.Parameters.AddWithValue("@p4", txtMusteriTel.Text);
			command.Parameters.AddWithValue("@p5", txtMusteriMail.Text);
			command.Parameters.AddWithValue("@p6", txtMusterituru.Text);

			command.ExecuteNonQuery();
			connection.Close();
			MessageBox.Show("Müşteri başarılı bir şekilde kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

		}


		// Tüm müşteri bilgilerini görüntülemek için sorgu oluştur ve DataGridView'e yükle
		private void btnMusteriGoruntule_Click(object sender, EventArgs e)
		{
			string command = "select * from musteri";
			NpgsqlDataAdapter da = new NpgsqlDataAdapter(command, connection);
			DataSet dt = new DataSet();
			da.Fill(dt);
			dataGridView1.DataSource = dt.Tables[0];
		}

		// Anasayfaya dönüş
		private void btnAnasayfa_Click(object sender, EventArgs e)
		{
			IsletmeAnasayfa anasayfa = new IsletmeAnasayfa();
			anasayfa.Show();
			this.Hide();
		}

		// Çıkış Yap
		private void btnCikisYap_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		////////////////// Formlar arası geçiş ////////////////////////
		private void lblMusteriEkle_Click(object sender, EventArgs e)
		{
			MusteriEkle addmusteri = new MusteriEkle();
			addmusteri.Show();
			this.Hide();
		}

		private void EmlakAra_Click(object sender, EventArgs e)
		{
			EmlakAra emlakAra = new EmlakAra();
			emlakAra.Show();
			this.Hide();
		}

		private void EmlakEkle_Click(object sender, EventArgs e)
		{
			EmlakEkle addEmlak = new EmlakEkle();
			addEmlak.Show();
			this.Hide();
		}

		private void MusteriDuzenle_Click(object sender, EventArgs e)
		{
			MusteriDuzenle musteriDuzenle = new MusteriDuzenle();
			musteriDuzenle.Show();
			this.Hide();
		}

		private void EmlakDuzenle_Click(object sender, EventArgs e)
		{
			EmlakDuzenle emlakDuzenle = new EmlakDuzenle();
			emlakDuzenle.Show();
			this.Hide();
		}

		
	}
}

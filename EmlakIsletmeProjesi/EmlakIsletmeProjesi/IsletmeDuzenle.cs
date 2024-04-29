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
using System.Xml.Linq;

namespace EmlakIsletmeProjesi
{
	public partial class IsletmeDuzenle : Form
	{
		public IsletmeDuzenle()
		{
			InitializeComponent();
		}

		public string mail;

		// Veritabanı bağlantısı

		NpgsqlConnection connection = new NpgsqlConnection("server = localHost; port=5432;" +
			" Database= dbemlakisletmesi; user Id = postgres; password= 123456");

		private void btnKydt_Click(object sender, EventArgs e)
		{

			// Veritabanında işletme bilgilerini güncellemek için sorgu oluştur
			connection.Open();
			NpgsqlCommand command = new NpgsqlCommand("Update isletme set isletmeadi = @p1, yetkili = @p2, adres = @p3, telefon = @p4, mail =@p5, sifre = @p6 ", connection);
			command.Parameters.AddWithValue("@p1", txtIsletmeAdi.Text);
			command.Parameters.AddWithValue("@p2", txtYetkili.Text);
			command.Parameters.AddWithValue("@p3", txtAdres.Text);
			command.Parameters.AddWithValue("@p4", txtTelefon.Text);
			command.Parameters.AddWithValue("@p5", txtMail.Text);
			command.Parameters.AddWithValue("@p6", txtSifre.Text);


			command.ExecuteNonQuery();
			MessageBox.Show("İşletme başarılı bir şekilde güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
			connection.Close();

		}

		// İşletme bilgilerini görüntüle butonu
		private void btnIsletmeGoster_Click(object sender, EventArgs e)
		{
			// Tüm işletme bilgilerini görüntülemek için sorgu oluştur ve DataGridView e yükle
			string command = "select * from isletme";
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

		// DataGridViewdeki bir hücreye tıklandığında ilgili bilgileri ilgili metin kutularına yerleştirir
		private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			txtIsletmeAdi.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
			txtYetkili.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
			txtAdres.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
			txtTelefon.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
			txtMail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
			txtSifre.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
			

		}

		/////////////// Formlar arası geçiş ////////////////////
		private void musteriEkle_Click(object sender, EventArgs e)
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

		// Çıkış yap
		private void btnCikisYap_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		
	}
}

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
	public partial class MusteriDuzenle : Form
	{
		public MusteriDuzenle()
		{
			InitializeComponent();
		}

		NpgsqlConnection connection = new NpgsqlConnection("server = localHost; port=5432;" +
			" Database= dbemlakisletmesi; user Id = postgres; password= 123456");

		
		private void btnGuncelle_Click(object sender, EventArgs e)
		{
			// Müşteri bilgilerini güncellemek için sorgu oluştur
			connection.Open();
			NpgsqlCommand command = new NpgsqlCommand("update musteri set  musteriid = @p1, musteriad = @p2, musterisoyad = @p3," +
				" musteritel = @p4, musterimail = @p5 ,musterituru = @p6 WHERE musteriid = @p1", connection);
			command.Parameters.AddWithValue("@p1", int.Parse(txtMusteriid.Text));
			command.Parameters.AddWithValue("@p2", txtMusteriAd.Text);
			command.Parameters.AddWithValue("@p3", txtMusteriSoyad.Text);
			command.Parameters.AddWithValue("@p4", txtMusteriTel.Text);
			command.Parameters.AddWithValue("@p5", txtMusteriMail.Text);
			command.Parameters.AddWithValue("@p6", txtMusterituru.Text);

			command.ExecuteNonQuery();
			connection.Close();
			MessageBox.Show("Müşteri başarılı bir şekilde güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		// Tüm müşteri bilgilerini görüntülemek için sorgu oluştur ve DataGridViewe yükle
		private void btnMusteriGoruntule_Click(object sender, EventArgs e)
		{
			string command = "select * from musteri";
			NpgsqlDataAdapter da = new NpgsqlDataAdapter(command, connection);
			DataSet dt = new DataSet();
			da.Fill(dt);
			dataGridView1.DataSource = dt.Tables[0];
		}

		//Anasayfaya dön
		private void btnAnasayfa_Click(object sender, EventArgs e)
		{
			IsletmeAnasayfa anasayfa = new IsletmeAnasayfa();
			anasayfa.Show();
			this.Hide();
		}

		// DataGridViewdeki bir hücreye tıklandığında ilgili bilgileri ilgili metin kutularına yerleştir
		private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			txtMusteriid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
			txtMusteriAd.Text= dataGridView1.CurrentRow.Cells[1].Value.ToString();
			txtMusteriSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
			txtMusteriTel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
			txtMusteriMail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
			txtMusterituru.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
		}
		private void btnCikisYap_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		//////////////////// Formlar arası geçiş ////////////////////////
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

		private void lblMusteriDuzenle_Click(object sender, EventArgs e)
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

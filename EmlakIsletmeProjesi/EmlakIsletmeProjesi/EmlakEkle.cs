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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EmlakIsletmeProjesi
{
	public partial class EmlakEkle : Form
	{
		public EmlakEkle()
		{
			InitializeComponent();
		}

		NpgsqlConnection connection = new NpgsqlConnection("server = localHost; port=5432;" +
			" Database= dbemlakisletmesi; user Id = postgres; password= 123456");

		private void EmlakEkle_Load(object sender, EventArgs e)
		{
			// Müşterileri yükle ve comboboxa bağla (müşterileri comboboxtan seçmek için)
			connection.Open();
			NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from musteri", connection);
			DataTable dt = new DataTable();
			da.Fill(dt);
			dt.Columns.Add("AdSoyad", typeof(string), "musteriad + ' ' + musterisoyad");
			comboBoxMusteri.DisplayMember = "AdSoyad";
			comboBoxMusteri.ValueMember = "musteriid";
			comboBoxMusteri.DataSource = dt;
			connection.Close();
		}

		// Yeni bir emlak kaydı oluştur ve veritabanına ekle
		private void btnKaydet_Click(object sender, EventArgs e)
		{
			connection.Open();
			NpgsqlCommand command = new NpgsqlCommand("insert into emlak ( emlaktipi, metrekare, odasayisi, kat,fiyat, il,ilce, mahalle,musteri,emlakdurumu)" +
				" values (@p2,@p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10,@p11)", connection);

			//command.Parameters.AddWithValue("@p1", int.Parse(txtEmlakid.Text));
			command.Parameters.AddWithValue("@p2", txtEmlakTipi.Text);
			command.Parameters.AddWithValue("@p3", int.Parse(txtMetrekare.Text));
			command.Parameters.AddWithValue("@p4", txtOdaSayisi.Text);
			command.Parameters.AddWithValue("@p5", int.Parse(txtKat.Text));
			command.Parameters.AddWithValue("@p6", int.Parse(txtFiyat.Text));
			command.Parameters.AddWithValue("@p7", txtIl.Text);
			command.Parameters.AddWithValue("@p8", txtIlce.Text);
			command.Parameters.AddWithValue("@p9", txtMahalle.Text);
			command.Parameters.AddWithValue("@p10", int.Parse(comboBoxMusteri.SelectedValue.ToString()));
			command.Parameters.AddWithValue("@p11", txtEmlakDurum.Text);

			command.ExecuteNonQuery();
			connection.Close();
			MessageBox.Show("Emlak başarılı bir şekilde kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		
		//Emlakları görüntüle butonu
		private void btnEmlakGoruntule_Click(object sender, EventArgs e)
		{
			string command = "select * from emlak_listesi";
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
		/////////////////// Formlar arası geçiş //////////////////
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

		private void lblEmlakEkle_Click(object sender, EventArgs e)
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

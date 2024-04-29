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
	public partial class EmlakDuzenle : Form
	{
		public EmlakDuzenle()
		{
			InitializeComponent();
		}

		NpgsqlConnection connection = new NpgsqlConnection("server = localHost; port=5432;" +
			" Database= dbemlakisletmesi; user Id = postgres; password= 123456");


		// DataGridView hücresine tıklandığında çalışacak olay
		private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			// Seçilen emlak satırını textbox ve comboboxa yükler
			txtEmlakid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
			txtEmlakTipi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
			txtMetrekare.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
			txtOdaSayisi.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
			txtKat.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
			txtFiyat.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
			txtIl.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
			txtIlce.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
			txtMahalle.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
			comboBoxMusteri.SelectedItem = dataGridView1.CurrentRow.Cells[9].Value.ToString();
			txtEmlakDurum.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
			
		}

		// Emlakları görüntüler
		private void btnEmlakGoruntule_Click(object sender, EventArgs e)
		{
			string command = "select * from emlak_listesi";// Tüm emlakları görüntülemek için sorgu oluşturduk
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

		// Emlak güncelleme butonu
		private void btnGuncelle_Click(object sender, EventArgs e)
		{

			// Seçilen emlak bilgilerini günceller
			connection.Open();
			NpgsqlCommand command = new NpgsqlCommand("update emlak set emlaktipi =@p2, metrekare = @p3, odasayisi= @p4, kat = @p5," +
				"fiyat = @p6, il = @p7,ilce =@p8, mahalle =@p9 ,musteri = @p10, emlakdurumu=@p11 WHERE emlakid = @p1", connection);

			command.Parameters.AddWithValue("@p1", int.Parse(txtEmlakid.Text));
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
			MessageBox.Show("Emlak başarılı bir şekilde güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void EmlakDuzenle_Load(object sender, EventArgs e)
		{
			// Müşterileri getir ve comboboxta göster
			connection.Open();
			NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from musteri", connection);
			DataTable dt = new DataTable();
			da.Fill(dt);
			dt.Columns.Add("AdSoyad", typeof(string), "musteriad + ' ' + musterisoyad");//ad soyadı tek satırda yazdık
			comboBoxMusteri.DisplayMember = "AdSoyad";
			comboBoxMusteri.ValueMember = "musteriid";
			comboBoxMusteri.DataSource = dt;
			connection.Close();

		}
		//////////// Formlar arası geçiş /////////////
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

		private void lblEmlakDuzenle_Click(object sender, EventArgs e)
		{
			EmlakDuzenle emlakDuzenle = new EmlakDuzenle();
			emlakDuzenle.Show();
			this.Hide();
		}
		// Çıkış Yap
		private void btnCikisYap_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}

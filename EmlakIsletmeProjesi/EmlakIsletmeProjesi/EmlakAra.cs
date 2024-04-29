using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace EmlakIsletmeProjesi
{
	public partial class EmlakAra : Form
	{
		public EmlakAra()
		{
			InitializeComponent();
		}


		public int id = 1;

		NpgsqlConnection connection = new NpgsqlConnection("server = localHost; port=5432;" +
			" Database= dbemlakisletmesi; user Id = postgres; password= 123456");

		private void EmlakAra_Load(object sender, EventArgs e)
		{
			VerileriYukle();// Verilerin yükle
		}


		// Verilerin yüklenmesi için metod
		private void VerileriYukle()
		{
			try
			{
				//connection.Open();

				// ComboBox'ları doldur
				FillComboBox("emlaktipi", cbEmlakTipi);
				FillComboBox("metrekare", cbMetrekare);
				FillComboBox("odasayisi", cbOdaSayisi);
				FillComboBox("il", cbIl);
				FillComboBox("ilce", cbIlce);
				FillComboBox("emlakdurumu", cbEmlakDurumu);

				connection.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Hata: " + ex.Message);
			}
		}


		// ComboBox'ları dolduran metot
		private void FillComboBox(string column, ComboBox comboBox)
		{
			// emlak tablosundaki kolonlara ait benzersiz değerleri çektik
			string query = $"select distinct {column} from emlak";
			NpgsqlCommand command = new NpgsqlCommand(query, connection);
			NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);
			DataTable dt = new DataTable();
			da.Fill(dt);
			comboBox.DataSource = dt;
			comboBox.DisplayMember = column;
			comboBox.ValueMember = column;
			comboBox.SelectedIndex = -1; // Başlangıçta herhangi bir seçim olmadığını belirtmek için
		}

		// Arama yapma metodu
		private void AramaYap()
		{
			try
			{
				connection.Open();


				string query = "SELECT * FROM emlak WHERE 1=1";


				// ComboBox'lardan seçilen değerlere göre filtreleme yapılır
				if (cbEmlakTipi.SelectedIndex != -1) //emlaktipinde comboboxtan seçim yaptık diyelim
				{
					query += $" AND emlaktipi = '{cbEmlakTipi.SelectedValue}'";// sorguya ilgili koşulu ekleriz
				}
				if (cbMetrekare.SelectedIndex != -1)//metrekareden yapılan seçim de sorguya eklenir
				{
					query += $" AND metrekare = '{cbMetrekare.SelectedValue}'";
				}
				if (cbOdaSayisi.SelectedIndex != -1)
				{
					query += $" AND odasayisi = '{cbOdaSayisi.SelectedValue}'";
				}
				if (cbIl.SelectedIndex != -1)
				{
					query += $" AND il = '{cbIl.SelectedValue}'";
				}
				if (cbIlce.SelectedIndex != -1)
				{
					query += $" AND ilce = '{cbIlce.SelectedValue}'";
				}
				if (cbEmlakDurumu.SelectedIndex != -1)
				{
					query += $" AND emlakdurumu = '{cbEmlakDurumu.SelectedValue}'";
				}

				NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connection);
				DataTable dt = new DataTable();
				da.Fill(dt);
				dataGridView1.DataSource = dt;

				connection.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Hata: " + ex.Message);
			}
		}

		//private void cb_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	AramaYap();
		//}



		// Arama yap butonuna tıklandığında arama yapma metodu çağırılır
		private void btnAra_Click(object sender, EventArgs e)
		{
			AramaYap();
		}

		private void btnTemizle_Click(object sender, EventArgs e)
		{

			VerileriYukle(); // Verileri yükleme metodu çağrılır
							 // ComboBox'ların seçimleri temizlenir
			cbEmlakTipi.SelectedIndex = -1;
			cbMetrekare.SelectedIndex = -1;
			cbOdaSayisi.SelectedIndex = -1;
			cbIl.SelectedIndex = -1;
			cbIlce.SelectedIndex = -1;
			cbEmlakDurumu.SelectedIndex = -1;

		}

		// Yazdır butonuna tıklandığında çalışacak olay

		private void btnYazdir_Click(object sender, EventArgs e)
		{
			printPreviewDialog1.Document = printDocument1;
			printPreviewDialog1.PrintPreviewControl.Zoom = 1;
			printPreviewDialog1.ShowDialog();

		}

		// Yazdırma işlemi 
		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			Bitmap bmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
			dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
			e.Graphics.DrawImage(bmp, 50, 100);

		}



		////////////Formlar arası geçiş ///////////////
		private void lblMusteriEkle_Click(object sender, EventArgs e)
		{
			MusteriEkle addmusteri = new MusteriEkle();
			addmusteri.Show();
			this.Hide();
		}

		private void labelEmlakAra_Click(object sender, EventArgs e)
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

		private void lblMusteriDuzenle_Click(object sender, EventArgs e)
		{
			MusteriDuzenle musteriDuzenle = new MusteriDuzenle();
			musteriDuzenle.Show();
			this.Hide();
		}

		private void lblEmlakDüzenle_Click(object sender, EventArgs e)
		{
			EmlakDuzenle emlakDuzenle = new EmlakDuzenle();
			emlakDuzenle.Show();
			this.Hide();
		}

		private void btnAnasayfa_Click(object sender, EventArgs e)
		{
			IsletmeAnasayfa anasyf = new IsletmeAnasayfa();
			anasyf.Show();
			this.Hide();
		}


		//çıkış yap
		private void btnCikisYap_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}


	}
}

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
	public partial class IsletmeAnasayfa : Form
	{
		public IsletmeAnasayfa()
		{
			InitializeComponent();
		}


		/////////////// Formlara geçiş kodları ///////////////////////
		private void btnMusteriEkle_Click(object sender, EventArgs e)
		{
			MusteriEkle addmusteri = new MusteriEkle();
			addmusteri.Show();
			this.Hide();

		}

		private void btnEmlakEkle_Click(object sender, EventArgs e)
		{
			EmlakEkle addEmlak = new EmlakEkle();	
			addEmlak.Show();
			this.Hide();
		}

		private void btnIsletmeGuncelle_Click(object sender, EventArgs e)
		{
			IsletmeDuzenle isletmeDuzenle = new IsletmeDuzenle();
			isletmeDuzenle.Show();
			this.Hide();
		}

		private void btnEmlakAra_Click(object sender, EventArgs e)
		{
			EmlakAra emlakAra = new EmlakAra();
			emlakAra.Show();
			this.Hide();
		}

		private void btnEmlakGuncelle_Click(object sender, EventArgs e)
		{
			EmlakDuzenle emlakDuzenle = new EmlakDuzenle();
			emlakDuzenle.Show();
			this.Hide();
		}

		private void btnMusteriDuzenle_Click(object sender, EventArgs e)
		{
			MusteriDuzenle musteriDuzenle = new MusteriDuzenle();
			musteriDuzenle.Show();
			this.Hide();
		}

		//Giriş Ekranına geri dönüş
		private void btnGeriDonus_Click(object sender, EventArgs e)
		{
			IsletmeGiris isletmeGiris = new IsletmeGiris();
			isletmeGiris.Show();
			this.Hide();
		}

		//////////////////////// Formlar arası geçiş ///////////////////////
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

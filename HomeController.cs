using Dapper;
using Fis.mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Globalization;

namespace Fis.mvc.Controllers
{
    public class HomeController : Controller
    {
        public string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=FistenVergiIndirimi; Integrated Security=true;";
        //giriş
        public ActionResult giris()
        {
            return View();
        }
        public ActionResult girisdeneme(string KullaniciAdi,string Sifre)
        {
            string sqlcumle = "SELECT * FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<KullanicilarModel> liste = connection.Query<KullanicilarModel>(sqlcumle, new { KullaniciAdi = KullaniciAdi, Sifre = Sifre }).ToList();

                if (liste.Any()) // Liste boş değilse (eşleşen kullanıcı varsa)
                {
                    // Kullanıcı doğru, Anasayfa'ya yönlendir
                    return RedirectToAction("anasayfa");
                }
                else
                {
                    return RedirectToAction("giris"); // Hatalıysa başka bir sayfaya yönlendirilebilir
                }
            }
        }
        //anasayfa
        public ActionResult anasayfa()
        {
            string sqlcumle = "SELECT * FROM Mesajlar";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<MesajlarModel> liste = connection.Query<MesajlarModel>(sqlcumle).ToList();

                return View(liste);
            }
        }
        //personel
        public ActionResult personel()
        {
            string sqlcumle = "SELECT * FROM Kullanicilar";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<KullanicilarModel> liste = connection.Query<KullanicilarModel>(sqlcumle).ToList();

                return View(liste);
            }
        }
        public ActionResult PersonelAra(int id)
        {
            string sqlcumle = "SELECT * FROM Kullanicilar WHERE id=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<KullanicilarModel> kullanicilar = connection.Query<KullanicilarModel>(sqlcumle, new { id = id }).ToList();
                return View("personel",kullanicilar);
            }
        }
        public ActionResult PersonelSil(int id)
        {
            string sqlcumle = "delete from Kullanicilar where id=@id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlcumle, new { id });
                return RedirectToAction("personel");
            }
            
        }
        //kayit
        public ActionResult kayit()
        {
            return View();
        }
        public ActionResult kayitol(KullanicilarModel item)
        {
            string sqlcumle = @"

INSERT INTO [dbo].[Kullanicilar]
           ([AdSoyad]
           ,[Meslek]
           ,[Mail]
           ,[TelefonNumarasi]
           ,[KullaniciAdi]
           ,[Sifre]
           ,[YetkiTuru]
           )
     VALUES
           (

@AdSoyad, @Meslek, @Mail, @TelefonNumarasi, @KullaniciAdi, @Sifre, @YetkiTuru

            )
";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Query<KullanicilarModel>(sqlcumle, item);
                return RedirectToAction("personel");
            }
            
        }
        //personelduzen
        public ActionResult personelduzen()
        {
            return View();
        }
        public ActionResult personelduzenle(KullanicilarModel item)
        {
            string sqlcumle = @"

UPDATE Kullanicilar SET AdSoyad=@AdSoyad, Meslek=@Meslek, Mail=@Mail, TelefonNumarasi=@TelefonNumarasi,
KullaniciAdi=@KullaniciAdi, Sifre=@Sifre, YetkiTuru=@YetkiTuru
WHERE id=@id

";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Query<KullanicilarModel>(sqlcumle, item);
                return RedirectToAction("personel");
            }
            
        }
        //mesajlar
        public ActionResult mesajlar()
        {
            string sqlcumle = "SELECT * FROM Mesajlar";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<MesajlarModel> liste = connection.Query<MesajlarModel>(sqlcumle).ToList();
                return View(liste);
            }
            
        }
        public ActionResult MesajSil(int id)
        {
            string sqlcumle = "delete from Mesajlar where id=@id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlcumle, new { id });
                return RedirectToAction("Mesajlar");
            }
            

        }
        public ActionResult mesajkayit(MesajlarModel item)
        {
            string sqlcumle = @"
INSERT INTO [dbo].[Mesajlar]
           ([AdSoyad]
           ,[Mesaj]
           )
     VALUES
           (
@AdSoyad, @Mesaj
            )
";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Query<MesajlarModel>(sqlcumle, item);
                return RedirectToAction("Mesajlar");
            }
            
        }
        //mesajduzen
        public ActionResult mesajduzen()
        {
            return View();
        }
        public ActionResult mesajduzenle(MesajlarModel item)
        {
            string sqlcumle = @"

UPDATE Mesajlar SET AdSoyad=@AdSoyad, Mesaj=@Mesaj WHERE id=@id
";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Query<MesajlarModel>(sqlcumle, item);
                return RedirectToAction("Mesajlar");
            }
            
        }
        //FisTablosu
        public ActionResult FisTablosu()
        {
            string sqlcumle = "SELECT * FROM FisTablosu";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<FisTablosuModel> liste = connection.Query<FisTablosuModel>(sqlcumle).ToList();

                return View(liste);
            }
        }
        public ActionResult FisSil(int id)
        {
            string sqlcumle = "delete from FisTablosu where id=@id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlcumle, new { id });
                return RedirectToAction("FisTablosu");
            }
        }
        //fiskayit
        public ActionResult fiskayit()
        {
            return View();
        }
        public ActionResult fiskayitet(FisTablosuModel item)
        {
            string sqlcumle = @"

INSERT INTO [dbo].[FisTablosu]
           ([Tarih]
           ,[FisNo]
           ,[Aciklama]
           ,[Masraf]
           ,[Kdv]
           ,[Toplam]
           ,[ToplamKdv]
           ,[Personelid]
           )
     VALUES
           (

@Tarih, @FisNo, @Aciklama, @Masraf, @Kdv, @Toplam, @ToplamKdv, @Personelid

            )
";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Query<FisTablosuModel>(sqlcumle, item);
                return RedirectToAction("FisTablosu");
            }
        }
        //fisduzen
        public ActionResult fisduzen()
        {
            return View();
        }
        public ActionResult fisduzenle(FisTablosuModel item)
        {
            string sqlcumle = @"

UPDATE FisTablosu SET Tarih=@Tarih, FisNo=@FisNo, Aciklama=@Aciklama, Masraf=@Masraf, Kdv=@Kdv, Toplam=@Toplam,
ToplamKdv=@ToplamKdv, Personelid=@Personelid
WHERE id=@id

";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Query<FisTablosuModel>(sqlcumle, item);
                return RedirectToAction("FisTablosu");
            }
            
        }
        //fisara
        public ActionResult fisara()
        {
            string sqlcumle = "SELECT * FROM FisTablosu";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<FisTablosuModel> liste = connection.Query<FisTablosuModel>(sqlcumle).ToList();

                return View(liste);
            }
        }
        public ActionResult fisarama(int Personelid,DateTime Tarih)
        {
            string sqlcumle = "SELECT * FROM FisTablosu WHERE FORMAT(Tarih, 'yyyy-MM') = @Tarih AND Personelid = @Personelid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<FisTablosuModel> liste = connection.Query<FisTablosuModel>(sqlcumle, new { Personelid = Personelid, Tarih = Convert.ToDateTime(Tarih).ToString("yyyy-MM") }).ToList();

                return View("fisara",liste);

            }
        }
        //kazanc
        public ActionResult kazanc()
        {
            return View();
        }
        public ActionResult kazanchesapla(int Personelid, DateTime Tarih)
        {
            string sqlcumle = "SELECT sum(Toplam)/25 FROM FisTablosu WHERE FORMAT(Tarih, 'yyyy-MM') = @Tarih AND Personelid = @Personelid";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                decimal toplam = connection.QueryFirstOrDefault<decimal>(sqlcumle, new { Personelid = Personelid, Tarih = Convert.ToDateTime(Tarih).ToString("yyyy-MM") });
                ViewBag.ToplamKazanc = toplam;
                return View("kazancsonuc");

            }
        }
        public ActionResult kazancsonuc()
        {
            return View();
        }
    }
}
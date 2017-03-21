using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace mvc_akka.Controllers
{
    public class HomeController : Controller
    {
        public static string _Email;
        public static string _PassWord;
        [HttpPost]
        public ActionResult GoToMyWorks(string Email, string Password)
        {
            _Email = Email;
            _PassWord = Password;
            ViewBag.Email = Email;
            ViewBag.Message = "Your SignIn page.";
            using (var context = new jobsearchEntities())
            {
                if (context.Tb_UserInformation.Find(_Email) != null)
                {
                    var item = context.Tb_UserInformation.Find(_Email);
                    ViewBag.FirstName = item.FirstName;
                    ViewBag.NewPassWord = item.PassWord;
                    if (_Email == item.Email && _PassWord == item.PassWord)
                    {
                        var Schemelist = (from scheme in context.Tb_Berüf where scheme.UsersEmail == Email select scheme).ToList();
                        return View(Schemelist);
                    }
                }
            }
            return View("SignIn");
        }
        public ActionResult GoToMyWorks()
        {

            ViewBag.Email = _Email;
            ViewBag.Message = "Your SignIn page.";
            using (var context = new jobsearchEntities())
            {
                        var Schemelist = (from scheme in context.Tb_Berüf where scheme.UsersEmail == _Email select scheme).ToList();
                foreach (var item in Schemelist)
                {
                    ViewBag.FirstName = item.Name;
                }
                return View(Schemelist);
               
            }
               
         
        }
        public ActionResult AllWorks(string Email, string Password)
        {
            jobsearchEntities context = new jobsearchEntities();
            var Schemelist = (from scheme in context.Tb_Berüf select scheme).ToList();
            return View(Schemelist);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "contact with us";

            return View();
        }

        public ActionResult Regester()
        {
            ViewBag.Message = "Your Regester page.";

            return View();
        }

        public ActionResult SignIn()
        {
            ViewBag.Message = "Your SignIn page.";

            return View();
        }

        public ActionResult Home()
        {
            ViewBag.Message = "Your Home page.";

            return View();
        }




        //public ActionResult SendMessage()
        //{
        //    ViewBag.Message = "Your Message.";

        //    return View();
        //}
        [HttpGet]
        public ActionResult AddNewBeruf(string Name, string Email)
        {
            ViewBag.FirstName =Name;
            ViewBag.UserEmail = Email;
            return View("NewBerüf");
        }
        [HttpPost]
        public ActionResult EditBeruf(int ID,string Name, string Email, string FirstName, string Work, string Company,
            string Descrebtion, string Adress, string Telefon, string WebSite)
        {
            ViewBag.UserEmail = Email;
            ViewBag.FirstName = FirstName;
            ViewBag.Work = Work;
            ViewBag.Company = Company;
            ViewBag.Descrebtion = Descrebtion;
            ViewBag.Adress = Adress;
            ViewBag.Telefon = Telefon;
            ViewBag.Email = WebSite;
            ViewBag.ID = ID;
            try
            {
                using (var context = new jobsearchEntities())
                {
                    if (context.Tb_Berüf.Find(ID)!=null)
                    {
                        var item = context.Tb_Berüf.Find(ID);
                        item.Name = FirstName;
                        item.Email = WebSite;
                        item.Adress = Adress;
                        item.Describe = Descrebtion;
                        item.TelephoneNumber = Telefon;
                        item.Beruf = Work;
                        item.UsersEmail = Email;
                        item.CompanyName = Company;
                        context.Tb_Berüf.AddOrUpdate(item);
                        context.SaveChanges();
                    }
                    ViewBag.EndMessage = "Done";
                    ViewBag.Message = "All Contacts.";
                    ViewBag.Letter = "Done Go To My Page";
                    return View("Letter");
                }
            }
            catch
                (Exception)
            {
                ViewBag.EndMessage = "Wrong Data";
                return View("Berüf");
            }
        }

        [HttpGet]
        public ActionResult SaveNewBeruf(string Email, string FirstName, string Work, string Company,
            string Descrebtion, string Adress, string Telefon, string WebSite)
        {
            ViewBag.UserEmail = Email;
            ViewBag.FirstName = FirstName;
            ViewBag.Work = Work;
            ViewBag.Company = Company;
            ViewBag.Descrebtion = Descrebtion;
            ViewBag.Adress = Adress;
            ViewBag.Telefon = Telefon;
            ViewBag.Email = WebSite;
            try
            {
                using (var context = new jobsearchEntities())
                {
                   
                        var item = new Tb_Berüf();
                        item.Name = FirstName;
                        item.Email = WebSite;
                        item.Adress = Adress;
                        item.Describe = Descrebtion;
                        item.TelephoneNumber = Telefon;
                        item.Beruf = Work;
                        item.UsersEmail = Email;
                        item.CompanyName = Company;
                        context.Tb_Berüf.AddOrUpdate(item);
                        context.SaveChanges();
                    ViewBag.ID = item.ID;
                }
                    ViewBag.EndMessage = "Done";
                    ViewBag.Message = "All Contacts.";
                ViewBag.Letter = "Done Go To My Page";
                    return View("Letter");
                }
           
            catch
                (Exception)
            {
                ViewBag.EndMessage = "Wrong Data";
                return View("NewBerüf");
            }
        }
        //public ActionResult SearsheWorks()
        //{
        //    List<Tb_Berüf> infos = new List<Tb_Berüf>();
        //    ViewBag.Message = "All Contacts.";
        //    using (var context = new TestAkkaEntities1())
        //    {
        //        foreach (var info in context.Tb_Berüf)
        //        {
        //            infos.Add(info);
        //        }
        //    }
        //    ViewBag.infos = infos;

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult GoToMyWorks(string Email, string Password)
        //{
        //    ViewBag.Email = Email;
        //    ViewBag.Message = "Your SignIn page.";
        //    using (var context = new TestAkkaEntities1())
        //    {
        //        if (context.Tb_UserInformation.Find(Email) != null)
        //        {
        //            var item = context.Tb_UserInformation.Find(Email);
        //            ViewBag.FirstName = item.FirstName;
        //            ViewBag.NewPassWord = item.PassWord;
        //            if (Email == item.Email && Password == item.PassWord)
        //            {
        //                var Schemelist = (from scheme in context.Tb_Berüf where scheme.UsersEmail==Email select scheme).ToList();
        //                return View("EditWorks");
        //            }
        //        }
        //    }
        //    return View("SignIn");
        //}

        public static int _confirmationMessage;
        private static int x=0;
        public ActionResult Regesterr(string NewFirstName, string NewLastName, string NewPassWord, string Email,
            string EmailPassWord, string ConfirmationMessage)
        {
            
            Random random = new Random();
            int randomNumber = random.Next(0, 10000);
            if (x == 0)
            {
                _confirmationMessage = randomNumber;
                
            }
            if (ConfirmationMessage != "" && ConfirmationMessage == _confirmationMessage.ToString())
            {

            if (NewFirstName != ""
                && NewLastName != "" && NewPassWord != ""
                && Email != "" && EmailPassWord != "")
            {
                ViewBag.Message = "Your Regester page.";
                ViewBag.FirstName = NewFirstName;
                ViewBag.LastName = NewLastName;
                ViewBag.Email = Email;
                ViewBag.EmailPassWord = EmailPassWord;
                ViewBag.NewPassWord = NewPassWord;

                using (var context = new jobsearchEntities())
                {
                    Tb_UserInformation info = new Tb_UserInformation();
                    info.FirstName = NewFirstName;
                    info.LastName = NewLastName;
                    info.PassWord = NewPassWord;
                    info.Email = Email;
                    info.EmailpassWord = EmailPassWord;
                    context.Tb_UserInformation.Add(info);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        ViewBag.AlertMessage = "You have already registered Go To SignIn ";
                        return View("Regester");
                    }

                }
                x = 0;
                return View("Welcom");
            }
            }


            if (ConfirmationMessage == "" && NewFirstName != ""
                && NewLastName != "" && NewPassWord != ""
                && Email != "" && EmailPassWord != "")
            {
                ViewBag.Message = "Your Regester page.";
                ViewBag.FirstName = NewFirstName;
                ViewBag.LastName = NewLastName;
                ViewBag.Email = Email;
                ViewBag.EmailPassWord = EmailPassWord;
                ViewBag.NewPassWord = NewPassWord;
                ViewBag.emailmessage = "we sent to you an email....Go to your Email Box";
                if (x == 0)
                {
                    SendConfiremMessage(Email, randomNumber);
                    x = 1;
                }
               
            }

            else
            {
                return View("Regester");
            }
          
           return View("Regester");

        }


        //[HttpPost]
        //public ActionResult SendMessagee(string Reciver, string Message)
        //{
        //    ViewBag.Message = "Your Message sendet.";
        //    ViewBag.message2 = Message;
        //    ViewBag.reciver = Reciver;
        //    return View("SendMessage");
        //}


        public void SendConfiremMessage( string Email,
            double randomNumber)


        {
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("ahmedfahed9559@gmail.com", "allah22akbr55ahmed66")
            };


            MailMessage mm = new MailMessage("ahmedfahed9559@gmail.com", Email, "Confirmation message",
                randomNumber.ToString())
            {
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            };

            client.Send(mm);

        }

        public ActionResult Edit(int ID,string Email, string FirstName, string Work, string Company, string Descrebtion,
            string Adress, string Telefon, string WebSite)
        {

            ViewBag.UserEmail = Email;
            ViewBag.FirstName = FirstName;
            ViewBag.Work = Work;
            ViewBag.Company = Company;
            ViewBag.Descrebtion = Descrebtion;
            ViewBag.Adress = Adress;
            ViewBag.Telefon = Telefon;
            ViewBag.Email = WebSite;
            ViewBag.ID = ID;
            return View("Berüf");

        }
        public ActionResult Delete(int ID,string Name,string Email)
        {
            try
            {
                ViewBag.ID = ID;
                using (var context = new jobsearchEntities())
                {
                    var item = context.Tb_Berüf.Find(ID);
                    context.Tb_Berüf.Remove(item);
                    context.SaveChanges();
                    
                }
                ViewBag.Letter = "Done Go To My Page";
                return View("Letter");
            }
            catch (Exception)
            {
                ViewBag.Letter = "Error";
                return View("Letter");
            }
         

        }
    }
}
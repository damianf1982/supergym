using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FoxRoles.Models;

namespace FoxRoles.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       // private IEnumerable list;

        // GET: Profiles
        public ActionResult Index()
        {   
            //Display a list of users who have profiles
            return View(db.profiles.ToList());
        }

        public ActionResult Notes()
        {
            //ApplicationDbContext db = new ApplicationDbContext();

            ////List<Profiles> list = db.profiles.ToList();
            //List<Profiles> list = db.profiles.ToList();

            //ViewBag.EmailsList = new SelectList(list, "Id", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Notes(int ? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("NotFound");
            }
            Profiles profiles = db.profiles.Find(Id);
            if (profiles == null)
            {
                return RedirectToAction("NotFound");
            }

            string xxx = profiles.Notes;
            string plaintext = Decrypt(xxx);

            profiles.Notes = plaintext;
            db.profiles.Add(profiles);
            //db.SaveChanges();

            return View(profiles);
        }

        // GET: Profiles/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Profiles profiles = db.profiles.Find(id);
        //    if (profiles == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    string xxx = profiles.Notes;
        //    string plaintext = Decrypt(xxx);

        //    profiles.Notes = plaintext;
        //    db.profiles.Add(profiles);
        //    //db.SaveChanges();

        //    return View(profiles);
        //}

      

        //method to decrypt the string
        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        /// method to decrypt the string
        //public string DecryptString(string encrString)
        //{
        //    byte[] b;
        //    string decrypted;
        //    try
        //    {
        //        b = Convert.FromBase64String(encrString);
        //        decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
        //    }
        //    catch (FormatException fe)
        //    {
        //        decrypted = "";
        //    }
        //    return decrypted;
        //}

        // GET: Profiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,GymNumber,Notes")] Profiles profiles)
        {
            if (ModelState.IsValid)
            {

                string notz = profiles.Notes;

               string Encryptedstring = encrypt(notz);
                profiles.Notes = Encryptedstring;
                db.profiles.Add(profiles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profiles);
        }

        // method to encrypt
        public string encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        //public string EnryptString(string strEncrypted)
        //{
        //    byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
        //    string encrypted = Convert.ToBase64String(b);
        //    return encrypted;
        //}

        //public string Test(string word)
        //{
        //    string name = word; 
        //    return name;
        //}

        // GET: Profiles/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Profiles profiles = db.profiles.Find(id);
        //    if (profiles == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(profiles);
        //}

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Email,GymNumber,Notes")] Profiles profiles)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(profiles).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(profiles);
        //}

        // GET: Profiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profiles profiles = db.profiles.Find(id);
            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profiles profiles = db.profiles.Find(id);
            db.profiles.Remove(profiles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

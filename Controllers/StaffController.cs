using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NoticeBoard.Models;

namespace NoticeBoard.Controllers
{
    public class StaffController : Controller
    {
        private NoticeBoardEntities db = new NoticeBoardEntities();

        // GET: Staff
        public ActionResult Index()
        {
            var notices = db.Notices.Include(n => n.Department);
            return View(notices.ToList());
        }

        // GET: Staff/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notice notice = db.Notices.Find(id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            return View(notice);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            return View();
        }

      
        // POST: Staff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(Notice noticeModel)
        {
            string filename = Path.GetFileNameWithoutExtension(noticeModel.ImageFile.FileName);
            string extension = Path.GetExtension(noticeModel.ImageFile.FileName);
            filename = filename + extension;
            noticeModel.ImagePath = "~/Photos/" + filename;
            filename = Path.Combine(Server.MapPath("~/Photos/"), filename);
            noticeModel.ImageFile.SaveAs(filename);
            if (ModelState.IsValid)
            {
                db.Notices.Add(noticeModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", noticeModel.DepartmentID);
            return View(noticeModel);
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notice notice = db.Notices.Find(id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", notice.DepartmentID);
            return View(notice);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Title,Headlines,SessionID,DepartmentID,ImagePath")] Notice notice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", notice.DepartmentID);
            return View(notice);
        }

        // GET: Staff/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notice notice = db.Notices.Find(id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            return View(notice);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Notice notice = db.Notices.Find(id);
            db.Notices.Remove(notice);
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

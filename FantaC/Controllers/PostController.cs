using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FantaC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;

namespace FantaC.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Post
        public ActionResult Index()
        {
            var post = db.Post.Include(p => p.User);
            return View(post.ToList());
        }

        // GET: Post/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            // ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "UserName");
            return View();
        }

        //// POST: Post/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "PostId,UserId,UserName,PostName,PostSubject,PostImage,PostContent,ApplicationUserId")] Post post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Post.Add(post);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", post.UserId);
        //    return View(post);
        //}

        // POST: Post/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "PostName, PostSubject, PostImage, PostContent")] Post model)
        {
            // makes the user information accessable
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            try
            {
                if (ModelState.IsValid)
                {
                    // makes a post id that is 472893 plus however many posts 
                    // there are that is 10 digits long with 0s in front to 
                    // fill in the remaining digits
                    string postId = (472893 + (db.Post.Count())).ToString().PadLeft(10, '0');

                    // fills all the information about the post into the post
                    // variable
                    var post = new Post
                    {
                        PostId = postId,
                        UserId = user.Id,
                        UserName = user.UserName,
                        PostName = model.PostName,
                        PostSubject = model.PostSubject,
                        PostImage = model.PostImage,
                        PostContent = model.PostContent
                    };

                    // adds the post variable to the database on the Post table
                    // and saves the changes
                    db.Post.Add(post);
                    db.SaveChanges();

                    // auto generated code that doesn't seem to work...?
                    // ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", post.UserId);
                }

                // sends the user back to /post
                return RedirectToAction("Index");
            }

            catch
            {
                return View();
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            // auto generated code that doesn't seem to work...?
            // ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", post.UserId);
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,UserId,UserName,PostName,PostSubject,PostImage,PostContent,ApplicationUserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // auto generated code that doesn't seem to work...?
            // ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", post.UserId);
            return View(post);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Post post = db.Post.Find(id);
            db.Post.Remove(post);
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

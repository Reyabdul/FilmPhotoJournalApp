using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FilmPhotoJournalApp.Models;
//use to help with debugging **use the output terminal to see repponse fromd debug
using System.Diagnostics;

namespace FilmPhotoJournalApp.Controllers
{
    public class PhotoDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PhotoData/ListPhotos
        [HttpGet]
        public IEnumerable<PhotoDto> ListPhotos()
        {
            List<Photo> Photos = db.Photos.ToList();
            List<PhotoDto> PhotoDtos = new List<PhotoDto>();

            Photos.ForEach(p => PhotoDtos.Add(new PhotoDto()
            {
                PhotoID = p.PhotoID,
                Iso = p.Iso,
                CollectionTitle = p.Collection.CollectionTitle
            }));

            return PhotoDtos;
        }

        // GET: api/FindPhoto/5
        [ResponseType(typeof(Photo))]
        [HttpGet]
        public IHttpActionResult FindPhoto(int id)
        {
            Photo photo = db.Photos.Find(id);
            PhotoDto PhotoDto = new PhotoDto()
            {
                PhotoID = photo.PhotoID,
                Iso = photo.Iso,
                CollectionTitle = photo.Collection.CollectionTitle
            };
            if (photo == null)
            {
                return NotFound();
            }

            return Ok(PhotoDto);
        }

        // POST: api/PhotoData/UpdatePhoto/5
        /*
         HOW TO TEST UPDATING PHOTOS:   
         -update the json file with the data
         -in 'C:\Users\reyabdul\source\repos\FilmPhotoJournalApp\FilmPhotoJournalApp\jsondata', type 'curl -d @photo.json -H "Content-type:application/json" "https://localhost:44350/api/photodata/updatephoto/6"'
         -check by typing 'curl https://localhost:44350/api/photodata/findphotos/5'

         */
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePhoto(int id, Photo photo)
        {
            Debug.WriteLine("I have reached update photo method.");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != photo.PhotoID)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("Get parameter" + id);
                Debug.WriteLine("POST parameter" + photo.PhotoID);
                Debug.WriteLine("POST parameter" + photo.Iso);
                return BadRequest();
            }

            db.Entry(photo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
                {
                    Debug.WriteLine("Photo not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }



        // POST: api/PhotoData/AddPhoto
        /*
         HOW TO TEST ADDING PHOTO:
        -create folder 'json data' with json file (e.g.:photo.json)
        -send post request, open photo.json in open folder in file explorer, cop addressas text, open command line and cd into address (e.g.:C:\Users\reyabdul\source\repos\FilmPhotoJournalApp\FilmPhotoJournalApp\jsondata)
        -command line:
            -type 'dir' and check for photo.json
            -add animal:
                -curl -d @photo.json -H "Content-type:application/json" https://localhost:44350/api/photodata/addphoto
                -check if add by typing 'curl https://localhost:44350/api/photodata/listphotos'       
         */
        [ResponseType(typeof(Photo))]
        [HttpPost]
        public IHttpActionResult AddPhoto(Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Photos.Add(photo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = photo.PhotoID }, photo);
        }

        // POST: api/PhotoData/DeletePhoto/5
            //Can test delete is working by typing in the command line 'curl -d "" https://localhost:44350/api/photodata/deletephoto/5
        [ResponseType(typeof(Photo))]
        [HttpPost]
        public IHttpActionResult DeletePhoto(int id)
        {
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return NotFound();
            }

            db.Photos.Remove(photo);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhotoExists(int id)
        {
            return db.Photos.Count(e => e.PhotoID == id) > 0;
        }
    }
}
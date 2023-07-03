using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//used when using 'HttpClient'
using System.Net.Http;
//used for debugging
using System.Diagnostics;
//import models to get the definition of 'photo' 
using FilmPhotoJournalApp.Models;
using System.Web.Script.Serialization;
//used when using JavascriptSerializer
using System.Web.Script.Serialization;


/*
 ======NOTES==========
-added 'PhotoController' through controller, then selecting 'MVC5 Controller with read/write actions'
    -PhotoController is used to access information through API and Views, whereas 'PhotoDataController' focuses on accessing/retrieving data from database
-make sure to import 'photo' from models so that this page knows the definition of waht a photo is suppposed to be
-HOW to check the debug codes:
    -launch code in browser
    -access the page where code is wrriten (e.g.: https:://https://localhost:44350/api/photodata/list)
    -check console output
 */

namespace FilmPhotoJournalApp.Controllers
{
    public class PhotoController : Controller
    {

        //Creating client variable to minimize code redundancy
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PhotoController()
        {
            client = new HttpClient(); // client variable
            client.BaseAddress = new Uri("https://localhost:44350/api/photodata/"); //base uri variable
        }

        // GET: Photo/List
        public ActionResult List()
        {
            //objective: communicate with our photo data api to retrieve a list of photos
            //curl https://localhost:44350/api/photodata/listphotos


            //'client' is anything that is accessing information from server
            //HttpClient client = new HttpClient() { };  **No longer needed since client varaible was defined in the top

            //establish url communication end point
            //string url = "https://localhost:44350/api/photodata/listphotos"; **no longer need since defined above
            string url = "listphotos"; //new url
            
            //will aniticpate client response
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Handling the api requests (However, make sure to make a View or else page won't show up)
            Debug.WriteLine("The reponse code is ");
            Debug.WriteLine(response.StatusCode);//this will let us know if the request was successfully handled in the terminal output ( the output will say 'ok')

            //if above is ok, parse the content into a 'IEnumerable' for photos
                //we should use 'PhotoDto' vs 'Photo' because we are mainly returning Data Transfer Objects rather than a series of animals
            IEnumerable<PhotoDto> photos = response.Content.ReadAsAsync<IEnumerable<PhotoDto>>().Result; //this code will wait for response, pull content, 'ReadAsAsync' instructs c# to parse data as the follow IEnumerbale<Photo>
            //the debug codes below will test if information was pulled correctly
            Debug.WriteLine("number of photos received: ");
            Debug.WriteLine(photos.Count());


            return View(photos);//if output displays all info, we can put 'photo' as an argument, also, make th adjustments in list view
        }


        // GET: Photo/Details/5
            //can use the code used in 'list' for 'details' section (can copy and paste)
        public ActionResult Details(int id)
        {
            //objective: communicate with our photo data api to retrieve one photo
            //curl https://localhost:44350/api/photodata/findphoto/{id}

            //'client' is anything that is accessing information from server
            //HttpClient client = new HttpClient() { };

            //establish url communication end point
            //string url = "https://localhost:44350/api/photodata/findphoto/"+id;  ***no longer needed since uri is definied above***
            string url = "findphoto/" + id;

            //will aniticpate client response
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Handling the api requests (However, make sure to make a View or else page won't show up)
            //Debug.WriteLine("The reponse code is ");
            //Debug.WriteLine(response.StatusCode);//this will let us know if the request was successfully handled in the terminal output ( the output will say 'ok')


            PhotoDto selectedphoto = response.Content.ReadAsAsync<PhotoDto>().Result; //IEnumberable is no longer needed because we are not looking for list of animals
            //the debug codes below will test if information was pulled correctly
            //Debug.WriteLine("animal received : ");
           // Debug.WriteLine(selectedphoto.PhotoID);

            return View(selectedphoto);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Photo/New
            //named 'New' rather than Create to show a page for inputing new photos
        public ActionResult New()
        {
            return View();
        }

        // POST: Photo/Create
        [HttpPost]
        public ActionResult Create(Photo photo)
        {
            //test to see if one can add photo
            // Debug.WriteLine("the input ISO is: ");
            // Debug.WriteLine(photo.Iso);

            Debug.WriteLine("the json payload is :");

            //objective:add new photo into our system using the API
            //curl -H "Content-Type:application/json" -d @photo.json https://localhost:{port}/api/photodata/addphoto
            string url = "addphoto";

            //convert C# object into json object to send to API
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(photo);

            Debug.WriteLine(jsonpayload); //checks if javascript object is created

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            //check if response is successful
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

            //client.PostAsync(url, content); 

        }

        // GET: Photo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Photo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Photo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Photo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

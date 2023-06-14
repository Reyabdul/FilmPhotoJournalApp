using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

/*
 ----------------------NOTES-------------
-the film class will be used as a bridging table (bridging between film-collection)
-don't forget to add migration and update database
    
*/

namespace FilmPhotoJournalApp.Models
{
    public class Film
    {
        [Key]
        public int FilmID { get; set; }
        public string FilmName { get; set; }

       //----------------BRIDGINg TABLE------------
       //A film will be part of a collection
       public ICollection<Collection> Collections { get; set; }
    }
}
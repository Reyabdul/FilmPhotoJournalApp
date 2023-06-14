using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 ----------------------NOTES-------------
-after creating a class, create DbSet in IdentityModels.cs to push data into the database
-after creating the DbSets, migration, and pushing into the database, can update properties futher

 */

namespace FilmPhotoJournalApp.Models
{
    public class Photo
    {
        //Properties
        public int PhotoID { get; set; }
        public int Iso { get; set; }

        //Todo: av, ss, notes, data, datetaken,image
    }
}
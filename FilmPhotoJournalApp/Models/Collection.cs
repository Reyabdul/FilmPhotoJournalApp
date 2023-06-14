using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

/*
 ----------------------NOTES-------------
-after creating a class, create DbSet in IdentityModels.cs to push data into the database
-after creating the DbSets, migration, and pushing into the database, can update properties futher
-can create other Models or foreign keys
*/

namespace FilmPhotoJournalApp.Models
{
    public class Collection
    {
        public int CollectionID { get; set; }
        public string CollectionTitle { get; set; }

    }
}
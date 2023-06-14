﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//must declare when using [Key]
using System.ComponentModel.DataAnnotations;

/*
 ----------------------NOTES-------------
-after creating a class, create DbSet in IdentityModels.cs to push data into the database
-after creating the DbSets, migration, and pushing into the database, can update properties futher
-can create other Models or foreign keys
*/

namespace FilmPhotoJournalApp.Models
{
    public class Photo
    {
        //Properties
        [Key]   //annotates the column below as the 'primary key'
        public int PhotoID { get; set; }
        public int Iso { get; set; }

        //Todo: av, ss, notes, data, datetaken,image
    }
}
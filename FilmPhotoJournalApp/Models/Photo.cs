using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//must declare when using [Key]
using System.ComponentModel.DataAnnotations;
//must declare when using [ForenignKey]
using System.ComponentModel.DataAnnotations.Schema;


/*
 ----------------------NOTES-------------
-after creating a class, create DbSet in IdentityModels.cs to push data into the database
-after creating the DbSets, migration, and pushing into the database, can update properties futher
-can create other Models or foreign keys
-when promted with red squiggly line, pressing 'alt+enter' wil display a recommended solution
-Error may occur
    <Message>An error has occurred.</Message>
    <ExceptionMessage>The 'ObjectContent`1' type failed to serialize the response body for content type 'application/xml; charset=utf-8'.</ExceptionMessage>    
    -since photo model contain many information that also connects to foreign keys, we can create a simpler version of that class or 'data transfer object' which can be seen below in 'PhotoDto'
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


        //--------Foreign Keys---------------  

        //A photo belongs to one collection
        //A collection can ahve many photos
        [ForeignKey("Collection")] //can be explicit and declare where foreign key is referencing from by add ("").
        public int CollectionID { get; set; }

        //if error occurs when updating database, it might be the cause of having existing values in the table, clear them and it should resolve the issue
        public virtual Collection Collection { get; set; } //error may occur when creating migration that connects photo and collections (e.g.:photo-collection). This is sometimes do to Entity requiring to be more explicit when describing a naviagation proprty(should be okay in other databases)

    }

    public class PhotoDto
    {
        public int PhotoID { get; set; }
        public int Iso { get; set; }
        public string CollectionTitle { get; set; }
    }
}
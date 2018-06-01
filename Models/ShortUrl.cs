using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Humanizer;

namespace Piccolo.Models
{
    public class ShortUrl
    {
        public int Id { get; set; }

        [Required]
        public string LongUrl { get; set; }

        public DateTime CreateDate { get; set; }

        public int? LookupCount { get; set; }

        public DateTime? LastLookupDate { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public string CreateDateHumanized => CreateDate.Humanize();

        [NotMapped]
        public string LastLookupDateHumanized => LastLookupDate.Humanize();

    }
}